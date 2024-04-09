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
        // 애니메이션 전환
        if (Input.GetButton("Horizontal") && !isRunning)
        {
            isRunning = true;
            anim.SetBool("isRun", true);
            anim.SetBool("isIdle", false);
        }
        else if (Input.GetButtonUp("Horizontal") && isRunning)
        {
            isRunning = false;
            rigid.velocity = new Vector2(rigid.velocity.normalized.x * 0.5f, rigid.velocity.y); // 감속
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
        
        if (isJumping) {

        }
        
    }
}
