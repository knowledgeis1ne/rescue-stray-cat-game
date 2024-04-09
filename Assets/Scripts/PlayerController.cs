using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    Animator anim;
    Rigidbody2D rigid;
    bool isJumping = false;
    bool isRunning = false;
    public float jumpPower;
    public float maxSpeed;

    private void Start()
    {
        anim = GetComponent<Animator>();
        rigid = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        // �ִϸ��̼� ��ȯ
        if (Input.GetButton("Horizontal") && !isRunning)
        {
            isRunning = true;
            if (!isJumping) anim.SetBool("isRun", true);
            anim.SetBool("isIdle", false);
        }
        else if (Input.GetButtonUp("Horizontal") && isRunning)
        {
            rigid.velocity = new Vector2(rigid.velocity.normalized.x * 0.5f, rigid.velocity.y); // ����
            StartCoroutine(DelayIdleAnimation());
        }

        if (Input.GetButtonDown("Jump") && !isJumping)
        {
            isJumping = true;
            rigid.AddForce(Vector2.up * jumpPower, ForceMode2D.Impulse);
            anim.SetTrigger("isJump");
            anim.SetBool("isRun", false);
            anim.SetBool("isIdle", false);
        }

        // ���� �ִϸ��̼� üũ
        if (anim.GetCurrentAnimatorStateInfo(0).IsName("Jump") == true)
        {
            float animTime = anim.GetCurrentAnimatorStateInfo(0).normalizedTime; // �ִϸ��̼� ���� �ð�
            if (animTime >= 1.0f) // �ִϸ��̼��� �����ٸ�
            {
                isJumping = false;
                if (isRunning) anim.SetBool("isRun", true);
                else anim.SetBool("isIdle", true);
            }
        }
    }

    private void FixedUpdate()
    {
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
    }

    private IEnumerator DelayIdleAnimation()
    {
        yield return new WaitForSeconds(0.1f);
        if (!Input.GetButton("Horizontal"))
        {
            isRunning = false;
            anim.SetBool("isRun", false);
            anim.SetBool("isIdle", true);
        }
    }
}
