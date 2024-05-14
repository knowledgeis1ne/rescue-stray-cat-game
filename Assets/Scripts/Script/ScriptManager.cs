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

    public Script[] scripts; // ��ü ��ũ��Ʈ ����� �޾� �� �迭
    public Script currentScript; // ���� ��ũ��Ʈ�� �޾� ��

    private ScriptManager scriptManager;

    public bool isPlaying = false;       // ��ũ��Ʈ ��� ���� ��� true
    public bool isFinished = false;      // ��ũ��Ʈ ����� ������ true
    public bool isTyping = false;        // Ÿ���� ���̸� true
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
        if (isPlaying)                            // ��ũ��Ʈ ��� ��
            if (Input.GetKeyDown(KeyCode.Return)) // ���͸� ������ ��
            {
                if (isTyping)                     // Ÿ���� ���� ����
                {
                    StopCoroutine(myCoroutine);
                    scriptText.maxVisibleCharacters = scriptText.text.Length;
                    nextButton.SetActive(false);
                    isTyping = false;
                } else
                {                                 // Ÿ������ ���� ����
                    nextButton.SetActive(false);
                    if (++currentLine < currentScript.sentences.Length)
                        StartCoroutine(Write());  // ���� �������� ���� �ڷ�ƾ ����
                    else
                    {
                        ShowScriptUI(false);      // ���� �������� UI ��Ȱ��ȭ
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

        // ��� ���� ��ũ��Ʈ�� �������� Ŭ���� ��ũ��Ʈ���ٸ�
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
        // �������� ���� ��ũ��Ʈ���ٸ�
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
        scriptText.maxVisibleCharacters = 0; // �������� �ִ� ������ ��
        int totalCharacters = completeText.Length; // ������ ��ü ����

        for (int i = 0; i < totalCharacters; i++)
        {
            scriptText.maxVisibleCharacters = i; // 1�� ������Ű�� Ÿ���� ȿ��
            yield return new WaitForSeconds(timePerCharacter); // maxVisibleCharacters�� ������ ������ �� �� ����� ����
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
