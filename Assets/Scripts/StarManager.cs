using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StarManager : MonoBehaviour
{
    float MSpeed = 2.0f; 
    bool beUp = false;

    // Update is called once per frame
    void Update()
    {
        if (beUp == false) 
        {
            transform.Translate(Vector2.up * MSpeed * Time.deltaTime); 
        }
        else 
        {
            transform.Translate(Vector2.down * MSpeed * Time.deltaTime);
        }
    }

    // ��ü ���� �浹 �˻�
    private void OnTriggerEnter2D(Collider2D collision) 
    {
        if (collision.name.Contains("boundary"))
        {
            if (beUp == false) 
            {
                beUp = true; 
                transform.localScale = new Vector3(1, -1, 1); 
            }
            else
            {
                beUp = false;
                transform.localScale = new Vector3(1, 1, 1);
            }
        }
    }
}
