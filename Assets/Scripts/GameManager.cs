using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    /*
    // �÷��̾� ������Ʈ
    GameObject player;
    // �÷��̾� �ʱ� ������ �� �����̼� ����
    Vector3 initialPosition;
    Quaternion initialRotation;

    private void Start()
    {
        SceneManager.sceneLoaded += LoadedsceneEvent;
        player = FindObjectOfType<PlayerController>().gameObject;
        initialPosition = player.transform.position;
        initialRotation = player.transform.rotation;
    }

    // ���� �ε�� ������ ȣ��
    private void LoadedsceneEvent(Scene scene, LoadSceneMode mode)
    {
        //player.transform.position = initialPosition;
        //player.transform.rotation = initialRotation;
        //PlayerController.instance.isMovable = true;
        //PlayerController.instance.dyingMark.SetActive(false);
        //CameraController.instance.isMovable = true;
    }
    */

    public void Restart()
    {
        string sceneName = SceneManager.GetActiveScene().name;
        SceneManager.LoadScene(sceneName);
    }
}
