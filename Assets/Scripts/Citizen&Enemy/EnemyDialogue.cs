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
        speedchBubbleRenderer.enabled = false; //��ǳ�� ��Ȱ��ȭ
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Player") //�÷��̾ ���� ���� ������ ��
        {
            speedchBubbleRenderer.enabled = true; //��ǳ�� Ȱ��ȭ
            player = collision.gameObject.GetComponent<Transform>();
            if(player.position.x > transform.position.x && transform.parent.localScale.x < 0) //����, ������ ���� ��ȯ
            {
                Flip ();
            }
            else if(player.position.x < transform.position.x && transform.parent.localScale.x > 0)
            {
                Flip();
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision) //�÷��̾ ���� �ۿ� ����� ��
    {
        if(collision.gameObject.tag == "Player")
        {
            speedchBubbleRenderer.enabled = false; //��ǳ�� ��Ȱ��ȭ
        }
    }

    private void Flip() //���� ��ȯ
    {
        Vector3 currentScale = transform.parent.localScale;
        currentScale.x *= -1; //���, ������ ��ȯ
        transform.parent.localScale = currentScale;
    }
}
