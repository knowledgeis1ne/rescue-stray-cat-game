using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StarManager : MonoBehaviour
{
    float MSpeed = 2.0f; //속도
    bool beUp = false; //위치 구별 변수(위에 있는 경우엔 true, 아래에 있을 경우 false)

    // Update is called once per frame
    void Update()
    {
        if (beUp == false) //아래에 있을 때
        {
            transform.Translate(Vector2.up * MSpeed * Time.deltaTime); //위로 움직임
        }
        else //위에 있을 때
        {
            transform.Translate(Vector2.down * MSpeed * Time.deltaTime); //아래로 움직임
        }
    }

    private void OnTriggerEnter2D(Collider2D collision) //충돌
    {
        if (collision.name.Contains("boundary")) //이동방향에 따라 이미지 반전
        {
            if (beUp == false) 
            {
                beUp = true;
                transform.localScale = new Vector3(1, -1, 1); //이미지 반전
            }
            else
            {
                beUp = false;
                transform.localScale = new Vector3(1, 1, 1); //이미지 반전
            }
        }
    }
}
