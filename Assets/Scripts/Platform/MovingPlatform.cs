using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    public Transform platform; 
    public Transform startPoint; //������
    public Transform endPoint; //����
    public float speed = 1.5f; //�ӵ�

    int direction = 1; //����

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    private void Update()
    {
        Vector2 target = currentMovementTarget();
        platform.position = Vector2.Lerp(platform.position, target, speed * Time.deltaTime); //�̵�

        float distance = (target - (Vector2)platform.position).magnitude; //���� ��ȯ

        if(distance <= 0.1f) //�Ÿ��� 0.1f���� ������
        {
            direction *= -1; //��ȯ
        }
    }

    Vector2 currentMovementTarget()
    {
        if(direction == 1)
        {
            return startPoint.position; //������ ��ǥ ��ġ ��ȯ
        }
        else
        {
            return endPoint.position; //���� ��ǥ ��ġ ��ȯ
        }
    }

    private void OnDrawGizmos()
    {
        if(platform != null && startPoint != null && endPoint != null) //�������� ���� �ð�ȭ
        {
            Gizmos.DrawLine(platform.transform.position, startPoint.position);
            Gizmos.DrawLine(platform.transform.position, endPoint.position);
        }
    }
}
