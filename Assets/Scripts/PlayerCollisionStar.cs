using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCollisionStar : MonoBehaviour
{
    Rigidbody2D playerRigid2D;
    float attackedForce = 120.0f;

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.name.Contains("star"))
        {
            playerRigid2D.AddForce(new Vector2(0, attackedForce));
            Debug.Log("Ãæµ¹µÊ");
        }
    }
}
