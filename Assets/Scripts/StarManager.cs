using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StarManager : MonoBehaviour
{
    float MSpeed = 2.0f; //�ӵ�
    bool beUp = false; //��ġ ���� ����(���� �ִ� ��쿣 true, �Ʒ��� ���� ��� false)

    // Update is called once per frame
    void Update()
    {
        if (beUp == false) //�Ʒ��� ���� ��
        {
            transform.Translate(Vector2.up * MSpeed * Time.deltaTime); //���� ������
        }
        else //���� ���� ��
        {
            transform.Translate(Vector2.down * MSpeed * Time.deltaTime); //�Ʒ��� ������
        }
    }

    private void OnTriggerEnter2D(Collider2D collision) //�浹
    {
        if (collision.name.Contains("boundary")) //�̵����⿡ ���� �̹��� ����
        {
            if (beUp == false) 
            {
                beUp = true;
                transform.localScale = new Vector3(1, -1, 1); //�̹��� ����
            }
            else
            {
                beUp = false;
                transform.localScale = new Vector3(1, 1, 1); //�̹��� ����
            }
        }
    }
}
