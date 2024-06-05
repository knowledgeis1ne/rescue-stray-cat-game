using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDialogue : MonoBehaviour
{
    private Transform player;
    private SpriteRenderer speedchBubbleRenderer;

    // Start is called before the first frame update
    void Start()
    {
        speedchBubbleRenderer = GetComponent<SpriteRenderer>();
        speedchBubbleRenderer.enabled = false; //말풍선 비활성화
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Player") //플레이어가 범위 내에 들어왔을 때
        {
            speedchBubbleRenderer.enabled = true; //말풍선 활성화
            player = collision.gameObject.GetComponent<Transform>();
            if(player.position.x > transform.position.x && transform.parent.localScale.x < 0) //왼쪽, 오른쪽 방향 전환
            {
                Flip ();
            }
            else if(player.position.x < transform.position.x && transform.parent.localScale.x > 0)
            {
                Flip();
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision) //플레이어가 범위 밖에 벗어났을 때
    {
        if(collision.gameObject.tag == "Player")
        {
            speedchBubbleRenderer.enabled = false; //말풍선 비활성화
        }
    }

    private void Flip() //방향 전환
    {
        Vector3 currentScale = transform.parent.localScale;
        currentScale.x *= -1; //양수, 음수로 전환
        transform.parent.localScale = currentScale;
    }
}
