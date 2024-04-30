using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    /*
    // 플레이어 오브젝트
    GameObject player;
    // 플레이어 초기 포지션 및 로테이션 저장
    Vector3 initialPosition;
    Quaternion initialRotation;

    private void Start()
    {
        SceneManager.sceneLoaded += LoadedsceneEvent;
        player = FindObjectOfType<PlayerController>().gameObject;
        initialPosition = player.transform.position;
        initialRotation = player.transform.rotation;
    }

    // 씬이 로드될 때마다 호출
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
