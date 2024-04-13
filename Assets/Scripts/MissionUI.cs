using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissionUI : MonoBehaviour
{
    public Transform missionPanel;      // 미션 안내 패널
    public Transform targetPosition;    // 패널이 이동할 타겟 위치
    public float smoothTime = 1.0f;     // SmoothDamp 매개변수
    Vector3 originalPosition;           // 패널의 원래 위치 저장

    private void Start()
    {
        missionPanel.gameObject.SetActive(true);
        originalPosition = missionPanel.position;

        StartCoroutine(MovePanel(targetPosition.position, smoothTime));
    }

    private IEnumerator MovePanel(Vector3 target, float time)
    {
        Vector3 velocity = Vector3.zero;
        float offset = 0.1f; // SmoothDamp에서 정확한 위치를 못 찾을 경우를 대비하여 오프셋 보정

        while (target.y + offset <= missionPanel.position.y)
        {
            missionPanel.position = Vector3.SmoothDamp(missionPanel.position, target, ref velocity, time);
            yield return null;
        }
        missionPanel.position = target;

        yield return new WaitForSeconds(1f);

        while (originalPosition.y - offset >= missionPanel.position.y)
        {
            missionPanel.position = Vector3.SmoothDamp(missionPanel.position, originalPosition,
                ref velocity, time);
            yield return null;
        }
        missionPanel.position = originalPosition;

        yield return null;
    }
}
