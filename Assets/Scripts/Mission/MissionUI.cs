using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MissionUI : MonoBehaviour
{
    public static MissionUI instance;
    public Transform fadeOutPanel;      // 페이드아웃 패널
    public Transform missionClearPanel; // 미션 클리어 패널
    public Transform missionPanel;      // 미션 안내 패널
    public Transform targetPosition;    // 패널이 이동할 타겟 위치
    public GameObject miniPanel;
    public TextMeshProUGUI miniPanelText;
    public float smoothTime = 1.0f;     // SmoothDamp 매개변수
    Vector3 originalPosition;           // 패널의 원래 위치 저장
    string sceneName;                   // 스테이지 구분을 위해 씬 이름 저장
    delegate void startMission();

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        sceneName = SceneManager.GetActiveScene().name;
        // 스테이지 1, 스테이지 2는 바로 미션 시작
        if (sceneName == "Stage1" || sceneName == "Stage2")
            StartMission();
        // 스테이지 3은 스크립트 먼저 실행
        else if (sceneName == "Stage3")
            ScriptManager.instance.FindScript("STAGE_3_START");
        else
        {

        }
    }

    public void StartMission()
    {
        ShowPanel();
        SetText();
        StartCoroutine(MovePanel(targetPosition.position, smoothTime));
    }

    private void ShowPanel()
    {
        missionPanel.gameObject.SetActive(true);
        originalPosition = missionPanel.position;
    }

    public void SetText() // 스테이지마다 미션 패널 텍스트 새롭게 설정
    {
        switch(sceneName)
        {
            case "Stage1":
                miniPanelText.text = "고양이가 갇힌 케이지를 열기 위해 여러 색깔의 열쇠를 모아 주세요";
                break;
            case "Stage2":
                miniPanelText.text = "고양이를 괴롭히는 악당들을 모두 처치해 주세요 (" 
                    + AttackEnemy.instance.attackCount + "/" + AttackEnemy.instance.enemyCount + ")";
                break;
            case "Stage3":
                miniPanelText.text = "다친 고양이를 시간 안에 병원에 데려가 주세요";
                break;
            case "Stage4":
                break;
        }
    }

    private IEnumerator MovePanel(Vector3 target, float time)
    {
        targetPosition.localPosition = new Vector3(0, 0);

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

    public IEnumerator FadeOutPanel()
    {
        yield return new WaitForSeconds(1f);

        WaitForSeconds wait = new WaitForSeconds(0.01f);

        if (!fadeOutPanel.gameObject.activeSelf)
            fadeOutPanel.gameObject.SetActive(true);

        Color c = fadeOutPanel.GetComponent<Image>().color;

        for (float f = 0f; f <= 0.6f; f += 0.025f)
        {
            c.a = f;
            fadeOutPanel.GetComponent<Image>().color = c;
            yield return wait;
        }

        ShowMissionClearPanel();
    }

    public void ShowMissionClearPanel()
    {
        if(!missionClearPanel.gameObject.activeSelf)
            missionClearPanel.gameObject.SetActive(true);
    }
}
