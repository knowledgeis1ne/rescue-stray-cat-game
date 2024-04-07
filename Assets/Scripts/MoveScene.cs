using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MoveScene : MonoBehaviour
{
    // ��ŸƮ �� ���� ��ư
    public void OnClickStartButton()
    {
        SceneManager.LoadScene("Stage1"); // 
    }

    // ��ŸƮ �� ���� ��ư
    public void OnClickExitButton()
    {
        #if UNITY_EDITOR // ����Ƽ ������ ������ ���
                UnityEditor.EditorApplication.isPlaying = false;
        #else
                Application.Quit();
        #endif
    }
}
