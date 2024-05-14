using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ScriptManager : MonoBehaviour
{
    public static ScriptManager instance;

    [SerializeField] string csvName;
    [SerializeField] Dictionary<int, Script> scriptDic = new Dictionary<int, Script>();

    public GameObject nextButton;
    public GameObject scriptPanel;
    public TextMeshProUGUI scriptText;
    public ScriptEvent script;

    public Script[] scripts; // 전체 스크립트 목록을 받아 올 배열
    public Script currentScript; // 현재 스크립트를 받아 옴

    private ScriptManager scriptManager;

    public bool isPlaying = false;       // 스크립트 재생 중일 경우 true
    public bool isFinished = false;      // 스크립트 재생이 끝나면 true
    public bool isTyping = false;        // 타이핑 중이면 true
    public int currentLine = 0;
    Coroutine myCoroutine;

    private void Awake()
    {
        instance = this;

        ScriptParser scriptParser = GetComponent<ScriptParser>();
        Script[] scripts = scriptParser.Parse(csvName);

        for (int i = 0; i < scripts.Length; i++)
            scriptDic.Add(i + 1, scripts[i]);
    }

    private void Start()
    {
        scriptManager = GetComponent<ScriptManager>();
        scriptManager.LoadScript(scriptManager.GetScript((int)script.line.x, (int)script.line.y));
    }

    private void Update()
    {
        ReturnScript();
    }

    private void LoadScript(Script[] scripts)
    {
        this.scripts = scripts;
    }

    public void ShowScript()
    {
        isPlaying = true;
        currentLine = 0;

        StartCoroutine(Write());
    }

    private Script[] GetScript(int start, int end)
    {
        List<Script> scriptList = new List<Script>();

        for (int i = 0; i <= end - start; i++)
            scriptList.Add(scriptDic[start + i]);

        script.scripts = scriptList.ToArray();

        return script.scripts;
    }

    private void ReturnScript()
    {
        if (isPlaying)                            // 스크립트 재생 중
            if (Input.GetKeyDown(KeyCode.Return)) // 엔터를 눌렀을 때
            {
                if (isTyping)                     // 타이핑 중인 상태
                {
                    StopCoroutine(myCoroutine);
                    scriptText.maxVisibleCharacters = scriptText.text.Length;
                    nextButton.SetActive(false);
                    isTyping = false;
                } else
                {                                 // 타이핑이 끝난 상태
                    nextButton.SetActive(false);
                    if (++currentLine < currentScript.sentences.Length)
                        StartCoroutine(Write());  // 문장 남았으면 다음 코루틴 시작
                    else
                    {
                        ShowScriptUI(false);      // 문장 끝났으면 UI 비활성화
                        Finished();
                        isFinished = true;
                        isPlaying = false;
                    }
                }
            }
    }
   
    private void Finished()
    {
        string s_name = currentScript.scriptName;

        // 방금 끝난 스크립트가 스테이지 클리어 스크립트였다면
        if (s_name == "STAGE_1_CLEAR_2" ||
            s_name == "STAGE_2_CLEAR")
        {
            MissionUI.instance.StartCoroutine("FadeOutPanel", 0.6f);
            MissionUI.instance.ShowMissionClearPanel();
        }
        else if (s_name == "STAGE_3_CLEAR_1")
        {
            MissionUI.instance.StartCoroutine("FadeOutPanel", 1f);
            FindScript("STAGE_3_CLEAR_2");
        }
        else if (s_name == "STAGE_3_CLEAR_2")
            MissionUI.instance.ShowMissionClearPanel();
        // 스테이지 시작 스크립트였다면
        else if (s_name == "STAGE_3_START")
        {
            MissionUI.instance.StartMission();
            GameObject.Find("Canvas").transform.Find("Timer Panel").gameObject.SetActive(true);
            GameObject.Find("Player").transform.Find("Black Cat").gameObject.SetActive(true);
        }

    }

    private IEnumerator Write()
    {
        isFinished = false;
        isTyping = true;

        scriptText.text = "";

        yield return new WaitForSeconds(0.5f);

        string replaceText = currentScript.sentences[currentLine];
        replaceText = replaceText.Replace("'", ",");
        replaceText = replaceText.Replace("\\n", "\n");
        scriptText.text = replaceText;

        ShowScriptUI(true);

        myCoroutine = StartCoroutine(TypingEffect(scriptText.text, 0.05f));
    }

    private IEnumerator TypingEffect(string completeText, float timePerCharacter)
    {
        scriptText.maxVisibleCharacters = 0; // 보여지고 있는 글자의 수
        int totalCharacters = completeText.Length; // 문장의 전체 길이

        for (int i = 0; i < totalCharacters; i++)
        {
            scriptText.maxVisibleCharacters = i; // 1씩 증가시키며 타이핑 효과
            yield return new WaitForSeconds(timePerCharacter); // maxVisibleCharacters가 증가할 때마다 몇 초 대기할 건지
        }

        if (isTyping) isTyping = false;
        nextButton.SetActive(true);
    }

    private void ShowScriptUI(bool flag)
    {
        scriptPanel.SetActive(flag);
    }

    public void FindScript(string scriptName)
    {
        for (int i = 0; i < script.scripts.Length; i++)
            if (script.scripts[i].scriptName == scriptName)
            {
                currentScript = script.scripts[i];
                break;
            }
        currentLine = 0;
        ShowScript();
    }
}
