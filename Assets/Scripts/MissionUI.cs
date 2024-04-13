using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissionUI : MonoBehaviour
{
    public Transform missionPanel;      // �̼� �ȳ� �г�
    public Transform targetPosition;    // �г��� �̵��� Ÿ�� ��ġ
    public float smoothTime = 1.0f;     // SmoothDamp �Ű�����
    Vector3 originalPosition;           // �г��� ���� ��ġ ����

    private void Start()
    {
        missionPanel.gameObject.SetActive(true);
        originalPosition = missionPanel.position;

        StartCoroutine(MovePanel(targetPosition.position, smoothTime));
    }

    private IEnumerator MovePanel(Vector3 target, float time)
    {
        Vector3 velocity = Vector3.zero;
        float offset = 0.1f; // SmoothDamp���� ��Ȯ�� ��ġ�� �� ã�� ��츦 ����Ͽ� ������ ����

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
