using System.Collections;
using System.Collections.Generic;
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
}
