using System.Collections;
using TMPro;
using System;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MissionUI : MonoBehaviour
{
    public static MissionUI instance;
    public Transform fadeOutPanel;      // ���̵�ƿ� �г�
    public Transform missionClearPanel; // �̼� Ŭ���� �г�
    public Transform missionPanel;      // �̼� �ȳ� �г�
    public Transform targetPosition;    // �г��� �̵��� Ÿ�� ��ġ
    public GameObject miniPanel;
    public TextMeshProUGUI miniPanelText;
    public float smoothTime = 1.0f;     // SmoothDamp �Ű�����
    Vector3 originalPosition;           // �г��� ���� ��ġ ����
    string sceneName;                   // �������� ������ ���� �� �̸� ����
    delegate void startMission();

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        StartCoroutine(FadeInPanel());
        sceneName = SceneManager.GetActiveScene().name;
        // �������� 1, �������� 2�� �ٷ� �̼� ����
        if (sceneName == "Stage1" || sceneName == "Stage2")
            StartMission();
        // �������� 3�� ��ũ��Ʈ ���� ����
        else if (sceneName == "Stage3")
            ScriptManager.instance.FindScript("STAGE_3_START");
        else if (sceneName == "Stage4")
            ScriptManager.instance.FindScript("STAGE_4_START");
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

    public void SetText() // ������������ �̼� �г� �ؽ�Ʈ ���Ӱ� ����
    {
        switch(sceneName)
        {
            case "Stage1":
                miniPanelText.text = "����̰� ���� �������� ���� ���� ���� ������ ���踦 ��� �ּ���";
                break;
            case "Stage2":
                miniPanelText.text = "����̸� �������� �Ǵ���� ��� óġ�� �ּ��� (" 
                    + AttackEnemy.instance.attackCount + "/" + AttackEnemy.instance.enemyCount + ")";
                break;
            case "Stage3":
                miniPanelText.text = "��ģ ����̸� �ð� �ȿ� ������ ������ �ּ���";
                break;
            case "Stage4":
                miniPanelText.text = "�������� ���� ��� �ö󰡼���";
                break;
        }
    }

    private IEnumerator MovePanel(Vector3 target, float time)
    {
        targetPosition.localPosition = new Vector3(0, 0);

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

    public IEnumerator FadeOutPanel(float transparent, Action onComplete = null)
    {
        yield return new WaitForSeconds(1f);

        WaitForSeconds wait = new WaitForSeconds(0.01f);

        if (!fadeOutPanel.gameObject.activeSelf)
            fadeOutPanel.gameObject.SetActive(true);

        Color c = fadeOutPanel.GetComponent<Image>().color;

        for (float f = 0f; f <= transparent; f += 0.025f)
        {
            c.a = f;
            fadeOutPanel.GetComponent<Image>().color = c;
            yield return wait;
        }

        onComplete?.Invoke(); // �ݹ� ȣ��
    }

    public IEnumerator FadeInPanel()
    {
        WaitForSeconds wait = new WaitForSeconds(0.01f);

        if (!fadeOutPanel.gameObject.activeSelf)
            fadeOutPanel.gameObject.SetActive(true);

        Color c = fadeOutPanel.GetComponent<Image>().color;

        for (float f = 1f; f >= 0f; f -= 0.05f)
        {
            c.a = f;
            fadeOutPanel.GetComponent<Image>().color = c;
            yield return wait;
        }

        fadeOutPanel.gameObject.SetActive(false);
    }

    public void ShowMissionClearPanel()
    {
        if(!missionClearPanel.gameObject.activeSelf)
            missionClearPanel.gameObject.SetActive(true);
    }
}
