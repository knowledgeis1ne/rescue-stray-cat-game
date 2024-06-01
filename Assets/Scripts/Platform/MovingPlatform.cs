using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    public Transform platform; 
    public Transform startPoint; //시작점
    public Transform endPoint; //끝점
    public float speed = 1.5f; //속도

    int direction = 1; //방향

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    private void Update()
    {
        Vector2 target = currentMovementTarget();
        platform.position = Vector2.Lerp(platform.position, target, speed * Time.deltaTime); //이동

        float distance = (target - (Vector2)platform.position).magnitude; //방향 전환

        if(distance <= 0.1f) //거리가 0.1f보다 작으면
        {
            direction *= -1; //전환
        }
    }

    Vector2 currentMovementTarget()
    {
        if(direction == 1)
        {
            return startPoint.position; //시작점 목표 위치 반환
        }
        else
        {
            return endPoint.position; //끝점 목표 위치 반환
        }
    }

    private void OnDrawGizmos()
    {
        if(platform != null && startPoint != null && endPoint != null) //시작점과 끝점 시각화
        {
            Gizmos.DrawLine(platform.transform.position, startPoint.position);
            Gizmos.DrawLine(platform.transform.position, endPoint.position);
        }
    }
}
