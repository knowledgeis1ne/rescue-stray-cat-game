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
                // Ű�� 4�� �̻��̶�� �ϴ� Key Panel ����
                if (FindKey.instance.getKeyList.Count > 3) FindKey.instance.ShowKeyPanel();
                // �׷��� �ʴٸ� ��ũ��Ʈ ���
                else
                {
                    ScriptManager.instance.FindScript("STAGE_1_FAIL_3");
                    ScriptManager.instance.ShowScript();
                }
            }
        }
    }
}
