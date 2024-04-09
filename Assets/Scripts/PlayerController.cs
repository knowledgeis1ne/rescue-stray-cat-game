using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    Animator anim;
    Rigidbody2D rigid;
    bool isRunning = false;
    bool isJumping = false;
    public float maxSpeed = 5.0f;

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
            anim.SetBool("isRun", true);
            anim.SetBool("isIdle", false);
        }
        else if (Input.GetButtonUp("Horizontal") && isRunning)
        {
            isRunning = false;
            rigid.velocity = new Vector2(rigid.velocity.normalized.x * 0.5f, rigid.velocity.y); // ����
            anim.SetBool("isRun", false);
            anim.SetBool("isIdle", true);
        }

        /*
        if (Input.GetKey(KeyCode.Space) && !isJumping)
        {
            isJumping = true;
            anim.SetBool("isJump", true);
        }
        */
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
        
        if (isJumping) {

        }
        
    }
}
