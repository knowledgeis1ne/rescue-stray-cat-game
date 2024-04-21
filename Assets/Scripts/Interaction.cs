using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interact : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.CompareTag("Player"))
        {
            if (Input.GetKeyDown(KeyCode.C))
            {
                // 키가 4개 이상이라면 일단 Key Panel 열기
                if (FindKey.instance.getKeyList.Count > 3) FindKey.instance.ShowKeyPanel();
                // 그렇지 않다면 스크립트 출력
                else
                {
                    ScriptManager.instance.FindScript("STAGE_1_FAIL_3");
                    ScriptManager.instance.ShowScript();
                }
            }
        }
    }
}
