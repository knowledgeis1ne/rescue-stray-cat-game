using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MissionUI : MonoBehaviour
{
    public Transform missionPanel;      // 미션 안내 패널
    public Transform targetPosition;    // 패널이 이동할 타겟 위치
    public GameObject miniPanel;
    public TextMeshProUGUI miniPanelText;
    public float smoothTime = 1.0f;     // SmoothDamp 매개변수
    Vector3 originalPosition;           // 패널의 원래 위치 저장
    string sceneName;                   // 스테이지 구분을 위해 씬 이름 저장

    private void Start()
    {
        missionPanel.gameObject.SetActive(true);
        originalPosition = missionPanel.position;
        sceneName = SceneManager.GetActiveScene().name;
        SetText();
        StartCoroutine(MovePanel(targetPosition.position, smoothTime));
    }

    public void SetText() // 스테이지마다 미션 패널 텍스트 새롭게 설정
    {
        switch(sceneName)
        {
            case "Stage1":
                miniPanelText.text = "고양이가 갇힌 케이지를 열기 위해 여러 색깔의 열쇠를 모아 주세요";
                break;
            case "Stage2":
                miniPanelText.text = "고양이를 괴롭히는 악당들을 모두 처치해 주세요";
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
        float offset = 0.5f; // SmoothDamp에서 정확한 위치를 못 찾을 경우를 대비하여 오프셋 보정

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
