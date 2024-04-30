using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class CameraController : MonoBehaviour
{
    public static CameraController instance;
    public float cameraSpeed;
    public Transform player;
    public Vector2 offset;
    public float minX, minY, maxX, maxY;
    private float cameraHalfWidth, cameraHalfHeight;
    public bool isMovable = true;

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        // ī�޶� �̵� ���� ������ ���� ī�޶� X��, Y�� ���� ���ϱ�
        // aspect : �ػ� ���� / orthographicSize : ī�޶� ������
        cameraHalfWidth = Camera.main.aspect * Camera.main.orthographicSize;
        cameraHalfHeight = Camera.main.orthographicSize;
    }

    private void LateUpdate()
    {
        if (isMovable)
        {
            Vector3 desiredPosition = new Vector3(
                        Mathf.Clamp(player.position.x + offset.x, minX + cameraHalfWidth, maxX - cameraHalfWidth),   // X
                        Mathf.Clamp(player.position.y + offset.y, minY + cameraHalfHeight, maxY - cameraHalfHeight), // Y
                        -10);                                                                                        // Z
            transform.position = Vector3.Lerp(transform.position, desiredPosition, Time.deltaTime * cameraSpeed); // �ε巴�� �̵�
        }
        
    }
}
