using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CitizenDialogue : MonoBehaviour
{
    private Transform player;
    private SpriteRenderer speedchBubbleRenderer;

    // Start is called before the first frame update
    void Start()
    {
        speedchBubbleRenderer = GetComponent<SpriteRenderer>();
        speedchBubbleRenderer.enabled = false;
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            speedchBubbleRenderer.enabled = true;
            player = collision.gameObject.GetComponent<Transform>();
            if(player.position.x > transform.position.x && transform.parent.localScale.x < 0)
            {
                Flip ();
            }
            else if(player.position.x < transform.position.x && transform.parent.localScale.x > 0)
            {
                Flip();
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            speedchBubbleRenderer.enabled = false;
        }
    }

    private void Flip()
    {
        Vector3 currentScale = transform.parent.localScale;
        currentScale.x *= -1;
        transform.parent.localScale = currentScale;
    }
}
