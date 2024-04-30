using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Die : MonoBehaviour
{
    public GameObject gameOverPanel;
    public GameObject player;

    private void OnTriggerEnter2D(Collider2D other)
    {
        // Die Area에 Player가 닿았을 경우
        if (other.CompareTag("Player"))
        {
            // 카메라 고정
            CameraController.instance.isMovable = false;
            // 키 입력 방지
            PlayerController.instance.isMovable = false;
            // 게임 오버 UI 표시
            StartCoroutine("ShowGameOver");
            // 씬 다시 로드
            // string sceneName = SceneManager.GetActiveScene().name;
            // SceneManager.LoadScene(sceneName);
        }
    }

    private IEnumerator ShowGameOver()
    {
        Time.timeScale = 0;
        yield return new WaitForSecondsRealtime(1f);
        Time.timeScale = 1;
        gameOverPanel.SetActive(true);
        yield return new WaitForSeconds(2f);
    }
}
