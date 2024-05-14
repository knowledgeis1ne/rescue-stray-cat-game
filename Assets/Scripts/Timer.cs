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
        //0�� �̻�
        if(remainingTime > 0)
        {
            remainingTime -= Time.deltaTime; //ī��Ʈ�ٿ�
        }
        else if(remainingTime < 0) //0�� ����
        {
            remainingTime = 0; //0���� ����
            timerText.color = Color.red; //0�� �Ǹ� ���� ���������� �ٲ�
            PlayerController.instance.Die();
        }
        int minutes = Mathf.FloorToInt(remainingTime / 60); //��
        int seconds = Mathf.FloorToInt(remainingTime % 60); //��
        timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds); //���
    }
}
