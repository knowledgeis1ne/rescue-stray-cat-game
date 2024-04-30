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
        // Die Area�� Player�� ����� ���
        if (other.CompareTag("Player"))
        {
            // ī�޶� ����
            CameraController.instance.isMovable = false;
            // Ű �Է� ����
            PlayerController.instance.isMovable = false;
            // ���� ���� UI ǥ��
            StartCoroutine("ShowGameOver");
            // �� �ٽ� �ε�
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
