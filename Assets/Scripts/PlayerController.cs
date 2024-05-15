using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    public static PlayerController instance;

    public Rigidbody2D rigid;
    Animator anim;
    WaitForSeconds ws;
    ScriptManager scriptManager;
    public bool isJumping = false; // ���� ���ΰ�?
    public bool isRunning = false; // �̵� ���ΰ�?
    public bool isIdling = true;   // �⺻ �����ΰ�?
    public bool isMovable = true;  // Ű �Է��� ������ �����ΰ�?
    public float jumpPower;
    public float maxSpeed;

    public GameObject gameOverPanel;
    public GameObject dyingMark;

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        anim = GetComponent<Animator>();
        rigid = GetComponent<Rigidbody2D>();
        ws = new WaitForSeconds(0.1f);
        scriptManager = ScriptManager.instance;
    }

    private void Update()
    {
        if (isMovable)
        {
            // �ִϸ��̼� ��ȯ
            if (Input.GetButton("Horizontal") && !isRunning)
            {
                isRunning = true; isIdling = false;
                if (!isJumping) anim.SetBool("isRun", isRunning);
                anim.SetBool("isIdle", isIdling);
            }
            else if (Input.GetButtonUp("Horizontal") && isRunning)
            {
                rigid.velocity = new Vector2(rigid.velocity.normalized.x * 0.5f, rigid.velocity.y); // ����
                StartCoroutine(DelayIdleAnimation());
            }

            if (Input.GetButtonDown("Jump") && !isJumping)
            {
                isJumping = true; isRunning = false; isIdling = false;
                rigid.AddForce(Vector2.up * jumpPower, ForceMode2D.Impulse);
                anim.SetTrigger("isJump");
                anim.SetBool("isRun", isRunning);
                anim.SetBool("isIdle", isIdling);
            }
        }

        if (Input.GetKeyDown(KeyCode.C))
        {
            Collider2D nearestCollider = GetNearestInteractableCollider();
            if (nearestCollider != null) InteractWith(nearestCollider.gameObject);
        }
    }

    private void FixedUpdate()
    {
        //DetectObject();
        if (isRunning) // Update()�� FixedUpdate() ����ȭ -> �Է� ���� Ȯ��
        {
            float h = Input.GetAxisRaw("Horizontal"); // -1, 0, 1 �� ��ȯ
            rigid.AddForce(Vector2.right * h, ForceMode2D.Impulse);

            if (rigid.velocity.x > maxSpeed) // ���� �ִ� ���ǵ� ����
                rigid.velocity = new Vector2(maxSpeed, rigid.velocity.y);
            else if (rigid.velocity.x < -maxSpeed) // ���� �ִ� ���ǵ� ����
                rigid.velocity = new Vector2(-maxSpeed, rigid.velocity.y);

            // ���⿡ ���� �÷��̾� ĳ���� �¿� ����
            if (h == -1) this.transform.localScale = new Vector3(-1, 1, 1);
            else if (h == 1) this.transform.localScale = new Vector3(1, 1, 1);
        }

        RaycastHit2D rayHit = Physics2D.Raycast(rigid.position, Vector3.down, 1, LayerMask.GetMask("Tilemap"));
        if (rayHit.collider != null)
        {
            string colTag = rayHit.collider.tag;
            if ((colTag == "Tilemap" || colTag == "Box")
                && rayHit.distance < 0.5f) // ���鿡 ��� �ִٸ�
            {
                isJumping = false;
                if (Mathf.Abs(rigid.velocity.x) < 0.1f)
                {
                    isRunning = false; isIdling = true;
                    anim.SetBool("isRun", isRunning);
                    anim.SetBool("isIdle", isIdling);
                }
            }
        }
    }


    private Collider2D GetNearestInteractableCollider()
    {
        float interactionDistance = 3f;
        LayerMask interactableLayer = LayerMask.GetMask("Interactable");

        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, interactionDistance, interactableLayer); // ��ȣ�ۿ� ������ ���̾��� �ݶ��̴� ����
        Collider2D nearestCollider = null;
        float nearestDistance = float.MaxValue;

        foreach (Collider2D col in colliders)
        {
            float distance = Vector2.Distance(transform.position, col.transform.position);
            if (distance < nearestDistance)
            {
                nearestDistance = distance;
                nearestCollider = col;
            }
        }

        return nearestCollider;
    }
    
    private void InteractWith(GameObject gameObject)
    {
        switch (gameObject.name)
        {
            case "Orange Cat":
                // ������ Ǯ������ ���� Ȯ��
                if (FindKey.instance.isCompleted)
                    scriptManager.FindScript("STAGE_1_CLEAR_2");
                else
                    scriptManager.FindScript("STAGE_1_FAIL_1");
                break;
            case "White Cat":
                if (AttackEnemy.instance.isCompleted)
                    scriptManager.FindScript("STAGE_2_CLEAR");
                else
                    scriptManager.FindScript("STAGE_2_FAIL");
                break;
            case "Box":
                // Ű�� 4�� �̻��̶�� �ϴ� Key Panel ����
                if (FindKey.instance.getKeyList.Count > 3) FindKey.instance.ShowKeyPanel();
                // �׷��� �ʴٸ� ��ũ��Ʈ ���
                else
                    scriptManager.FindScript("STAGE_1_FAIL_3");
                break;
        }
    }

    private IEnumerator DelayIdleAnimation()
    {
        yield return ws;
        if (Mathf.Abs(rigid.velocity.x) < 0.1f)
        {
            isRunning = false; isIdling = true;
            anim.SetBool("isRun", isRunning);
            anim.SetBool("isIdle", isIdling);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        // Die Area�� Player�� ����� ���
        if (other.CompareTag("Die"))
            Die();
        else if (other.CompareTag("Goal"))
            ScriptManager.instance.FindScript("STAGE_3_CLEAR_1");
    }

    public void Die()
    {
        // ī�޶� ����
        CameraController.instance.isMovable = false;
        // Ű �Է� ����
        isMovable = false;
        // ���� ���� UI ǥ��
        StartCoroutine("Delay");
        Invoke("GameOver", 0.8f);
    }

    private IEnumerator Delay()
    {
        dyingMark.SetActive(true);
        Time.timeScale = 0;
        yield return new WaitForSecondsRealtime(1f);
        Time.timeScale = 1;
        yield return null;
    }

    private void GameOver()
    {
        MissionUI.instance.StartCoroutine("FadeOutPanel", 1f);
        gameOverPanel.SetActive(true);
    }

    //������ �浹
    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            if (rigid.velocity.y < 0 && transform.position.y > collision.transform.position.y)
            {
                OnAttack(collision.transform);
            }
        }

        if(collision.gameObject.tag == "Star")
        {
            OnDamaged(collision.transform.position);
        }
    }

    //���� ��� ���̴� ��
    void OnAttack(Transform enemy)
    {
        rigid.AddForce(Vector2.up * 10, ForceMode2D.Impulse);
        EnemyMove enemyMove = enemy.GetComponent<EnemyMove>();
        enemyMove.OnDamaged();
        AttackEnemy.instance.attackCount++;
        if (AttackEnemy.instance.attackCount == AttackEnemy.instance.enemyCount)
        {
            MissionUI.instance.miniPanelText.text = "����̸� �����Ϸ� ������";
            AttackEnemy.instance.isCompleted = true;
        }
        else
            MissionUI.instance.SetText();
    }

    void OnDamaged(Vector2 targetPos)
    {
        int dirc = transform.position.x - targetPos.x > 0 ? 1 : -1;
        rigid.AddForce(new Vector2(dirc, 1) * 200);
        anim.SetTrigger("doDamaged");
    }
}