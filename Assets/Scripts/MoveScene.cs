using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MoveScene : MonoBehaviour
{
    // 씬 이동 버튼들을 관리하는 스크립트

    Button btn;

    private void Start()
    {
        btn = transform.GetComponent<Button>();

        if (btn != null)
            btn.onClick.AddListener(() =>
                Invoke(btn.gameObject.name, 0f));
    }

    private void Restart()
    {
        string sceneName = SceneManager.GetActiveScene().name;
        SceneManager.LoadScene(sceneName);
    }

    public void NextStage()
    {
        string sceneName = SceneManager.GetActiveScene().name;
        switch (sceneName)
        {
            case "Intro Scene":
                SceneManager.LoadScene("Stage1");
                break;
            case "Stage1":
                SceneManager.LoadScene("Stage2");
                break;
            case "Stage2":
                SceneManager.LoadScene("Stage3");
                break;
            case "Stage3":
                SceneManager.LoadScene("Stage4");
                break;
            case "Stage4":
                SceneManager.LoadScene("Ending Scene");
                break;
        }
    }

    private void MainScene()
    {
        SceneManager.LoadScene("Start Scene");
    }

    private void StartGame()
    {
        SceneManager.LoadScene("Intro Scene");
    }

    private void ExitGame()
    {
        #if UNITY_EDITOR // 유니티 에디터 실행의 경우
                UnityEditor.EditorApplication.isPlaying = false;
        #else
                Application.Quit();
        #endif
    }
}
