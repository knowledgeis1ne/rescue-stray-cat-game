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
    public bool isJumping = false; // 점프 중인가?
    public bool isRunning = false; // 이동 중인가?
    public bool isIdling = true;   // 기본 상태인가?
    public bool isMovable = true;  // 키 입력이 가능한 상태인가?
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
            // 애니메이션 전환
            if (Input.GetButton("Horizontal") && !isRunning)
            {
                isRunning = true; isIdling = false;
                if (!isJumping) anim.SetBool("isRun", isRunning);
                anim.SetBool("isIdle", isIdling);
            }
            else if (Input.GetButtonUp("Horizontal") && isRunning)
            {
                rigid.velocity = new Vector2(rigid.velocity.normalized.x * 0.5f, rigid.velocity.y); // 감속
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
        if (isRunning) // Update()와 FixedUpdate() 동기화 -> 입력 상태 확인
        {
            float h = Input.GetAxisRaw("Horizontal"); // -1, 0, 1 중 반환
            rigid.AddForce(Vector2.right * h, ForceMode2D.Impulse);

            if (rigid.velocity.x > maxSpeed) // 우측 최대 스피드 설정
                rigid.velocity = new Vector2(maxSpeed, rigid.velocity.y);
            else if (rigid.velocity.x < -maxSpeed) // 좌측 최대 스피드 설정
                rigid.velocity = new Vector2(-maxSpeed, rigid.velocity.y);

            // 방향에 따라 플레이어 캐릭터 좌우 반전
            if (h == -1) this.transform.localScale = new Vector3(-1, 1, 1);
            else if (h == 1) this.transform.localScale = new Vector3(1, 1, 1);
        }

        RaycastHit2D rayHit = Physics2D.Raycast(rigid.position, Vector3.down, 1, LayerMask.GetMask("Tilemap"));
        if (rayHit.collider != null)
        {
            string colTag = rayHit.collider.tag;
            if ((colTag == "Tilemap" || colTag == "Box")
                && rayHit.distance < 0.5f) // 지면에 닿아 있다면
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

        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, interactionDistance, interactableLayer); // 상호작용 가능한 레이어의 콜라이더 검출
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
                // 문제를 풀었는지 여부 확인
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
                // 키가 4개 이상이라면 일단 Key Panel 열기
                if (FindKey.instance.getKeyList.Count > 3) FindKey.instance.ShowKeyPanel();
                // 그렇지 않다면 스크립트 출력
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
        // Die Area에 Player가 닿았을 경우
        if (other.CompareTag("Die"))
            Die();
        else if (other.CompareTag("Goal"))
            ScriptManager.instance.FindScript("STAGE_3_CLEAR_1");
    }

    public void Die()
    {
        // 카메라 고정
        CameraController.instance.isMovable = false;
        // 키 입력 방지
        isMovable = false;
        // 게임 오버 UI 표시
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

    //적과의 충돌
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

    //적을 밟아 죽이는 것
    void OnAttack(Transform enemy)
    {
        rigid.AddForce(Vector2.up * 10, ForceMode2D.Impulse);
        EnemyMove enemyMove = enemy.GetComponent<EnemyMove>();
        enemyMove.OnDamaged();
        AttackEnemy.instance.attackCount++;
        if (AttackEnemy.instance.attackCount == AttackEnemy.instance.enemyCount)
        {
            MissionUI.instance.miniPanelText.text = "고양이를 구출하러 가세요";
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