using System.Collections;
using System.Collections.Generic;
using UnityEditor.SceneManagement;
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

    // ���� �������� ���� Ȯ�� ��ư
    public void OnClickOKButton()
    {
        string sceneName = SceneManager.GetActiveScene().name;
        switch (sceneName) 
        {
            case "Stage1":
                SceneManager.LoadScene("Stage2");
                break;
            case "Stage2":
                SceneManager.LoadScene("Stage3");
                break;
            case "Stage3":
                SceneManager.LoadScene("Stage4");
                break;
        }

    }
}
