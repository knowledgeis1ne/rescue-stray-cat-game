using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    Animator anim;
    Rigidbody2D rigid;
    public bool isJumping = false;
    public bool isRunning = false;
    public float jumpPower;
    public float maxSpeed;

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
            if (!isJumping) anim.SetBool("isRun", true);
            anim.SetBool("isIdle", false);
        }
        else if (Input.GetButtonUp("Horizontal") && isRunning)
        {
            rigid.velocity = new Vector2(rigid.velocity.normalized.x * 0.5f, rigid.velocity.y); // 감속
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

        // 점프 애니메이션 체크
        /*
        if (anim.GetCurrentAnimatorStateInfo(0).IsName("Jump") == true)
        {
            float animTime = anim.GetCurrentAnimatorStateInfo(0).normalizedTime; // 애니메이션 실행 시간
            if (animTime >= 1.0f) // 애니메이션이 끝났다면
            {
                isJumping = false;
                if (isRunning) anim.SetBool("isRun", true);
                else anim.SetBool("isIdle", true);
            }
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

        RaycastHit2D rayHit = Physics2D.Raycast(rigid.position, Vector3.down, 1, LayerMask.GetMask("Tilemap"));
        if (rayHit.collider != null)
        {
            if (rayHit.collider.name == "Tilemap" && rayHit.distance < 0.5f) // 지면에 닿아 있다면
            {
                Debug.Log(rayHit.collider.tag);
                isJumping = false;
                if (Mathf.Abs(rigid.velocity.x) < 0.1f)
                {
                    isRunning = false;
                    anim.SetBool("isIdle", true);
                }
            }
        }
    }

    private IEnumerator DelayIdleAnimation()
    {
        yield return new WaitForSeconds(0.1f);
        if (Mathf.Abs(rigid.velocity.x) < 0.1f)
        {
            isRunning = false;
            anim.SetBool("isRun", false);
            anim.SetBool("isIdle", true);
        }
    }
}
