using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MissionUI : MonoBehaviour
{
    public Transform missionPanel;      // �̼� �ȳ� �г�
    public Transform targetPosition;    // �г��� �̵��� Ÿ�� ��ġ
    public GameObject miniPanel;
    public TextMeshProUGUI miniPanelText;
    public float smoothTime = 1.0f;     // SmoothDamp �Ű�����
    Vector3 originalPosition;           // �г��� ���� ��ġ ����
    string sceneName;                   // �������� ������ ���� �� �̸� ����

    private void Start()
    {
        missionPanel.gameObject.SetActive(true);
        originalPosition = missionPanel.position;
        sceneName = SceneManager.GetActiveScene().name;
        SetText();
        StartCoroutine(MovePanel(targetPosition.position, smoothTime));
    }

    public void SetText() // ������������ �̼� �г� �ؽ�Ʈ ���Ӱ� ����
    {
        switch(sceneName)
        {
            case "Stage1":
                miniPanelText.text = "����̰� ���� �������� ���� ���� ���� ������ ���踦 ��� �ּ���";
                break;
            case "Stage2":
                miniPanelText.text = "����̸� �������� �Ǵ���� ��� óġ�� �ּ���";
                break;
            case "Stage3":
                break;
            case "Stage4":
                break;
        }
    }

    private IEnumerator MovePanel(Vector3 target, float time)
    {
        Vector3 velocity = Vector3.zero;
        float offset = 0.5f; // SmoothDamp���� ��Ȯ�� ��ġ�� �� ã�� ��츦 ����Ͽ� ������ ����

        while (target.y + offset <= missionPanel.position.y)
        {
            missionPanel.position = Vector3.SmoothDamp(missionPanel.position, target, ref velocity, time);
            yield return null;
        }
        missionPanel.position = target;

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
