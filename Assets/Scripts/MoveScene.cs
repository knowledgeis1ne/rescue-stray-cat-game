using System.Collections;
using System.Collections.Generic;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MoveScene : MonoBehaviour
{
    // 스타트 씬 시작 버튼
    public void OnClickStartButton()
    {
        SceneManager.LoadScene("Stage1"); // 
    }

    // 스타트 씬 종료 버튼
    public void OnClickExitButton()
    {
        #if UNITY_EDITOR // 유니티 에디터 실행의 경우
                UnityEditor.EditorApplication.isPlaying = false;
        #else
                Application.Quit();
        #endif
    }

    // 다음 스테이지 시작 확인 버튼
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
