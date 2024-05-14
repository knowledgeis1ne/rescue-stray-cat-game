 using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Timer : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI timerText;
    [SerializeField] float remainingTime;

    // Update is called once per frame
    void Update()
    {
        //0초 이상
        if(remainingTime > 0)
        {
            remainingTime -= Time.deltaTime; //카운트다운
        }
        else if(remainingTime < 0) //0초 이하
        {
            remainingTime = 0; //0으로 설정
            timerText.color = Color.red; //0이 되면 색깔 빨간색으로 바뀜
            PlayerController.instance.Die();
        }
        int minutes = Mathf.FloorToInt(remainingTime / 60); //분
        int seconds = Mathf.FloorToInt(remainingTime % 60); //초
        timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds); //출력
    }
}
