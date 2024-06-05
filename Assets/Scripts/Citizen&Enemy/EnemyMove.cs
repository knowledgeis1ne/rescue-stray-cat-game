using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMove : MonoBehaviour
{
    Rigidbody2D rigid;
    Animator anim;
    SpriteRenderer spriteRenderer;
    public int nextMove; //행동지표
    CapsuleCollider2D capsuleCollider;

    // Start is called before the first frame update
    void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        capsuleCollider = GetComponent<CapsuleCollider2D>();

        Invoke("Think", 5); //행동지표를 바꿔줄 함수
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        rigid.velocity = new Vector2(nextMove, rigid.velocity.y); //이동

        Vector2 frontVec = new Vector2(rigid.position.x + nextMove * 0.2f, rigid.position.y);
        Debug.DrawRay(frontVec, Vector3.down, new Color(0, 1, 0)); //플랫폼 체크

        RaycastHit2D rayHit = Physics2D.Raycast(frontVec, Vector3.down, 1, LayerMask.GetMask("Tilemap")); //타일맵

        if (rayHit.collider == null)
        {
            Turn(); //방향 전환
        }
    }

    void Think() //재귀 함수
    {
        nextMove = Random.Range(-1, 2); //다음 행동
        anim.SetInteger("WalkSpeed", nextMove); //애니메이션

        if (nextMove != 0)
        {
            spriteRenderer.flipX = (nextMove == 1); //이미지 전환
        }

        float nextThinkTime = Random.Range(2f, 5f); //재귀
        Invoke("Think", nextThinkTime);
    }

    void Turn() //전환
    {
        nextMove = nextMove * (-1); ///양수, 음수로 전환
        spriteRenderer.flipX = (nextMove == 1);

        CancelInvoke();
        Invoke("Think", 2);
    }

    public void OnDamaged() //플레이어에게 밟혔을 때
    {
        spriteRenderer.color = new Color(1, 1, 1, 0.4f);//색상 전환
        spriteRenderer.flipY = true; //위아래 전환
        capsuleCollider.enabled = false; //콜라이더 해제
        rigid.AddForce(Vector2.up * 5, ForceMode2D.Impulse); //반동
        Invoke("DeActive", 5);
    }

    void DeActive()
    {
        gameObject.SetActive(false);
    }
}
