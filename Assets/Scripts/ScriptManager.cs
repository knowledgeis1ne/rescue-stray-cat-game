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
        if (instance == null)
        {
            instance = this;

            ScriptParser scriptParser = GetComponent<ScriptParser>();
            Script[] scripts = scriptParser.Parse(csvName);

            for (int i = 0; i < scripts.Length; i++)
                scriptDic.Add(i + 1, scripts[i]);
        }
    }

    private void Start()
    {
        scriptManager = GetComponent<ScriptManager>();
        scriptManager.LoadScript(scriptManager.GetScript((int)script.line.x, (int)script.line.y));

        FindScript("TEST");
        ShowScript();
    }

    private void Update()
    {
        ReturnScript();
    }

    private void LoadScript(Script[] scripts)
    {
        this.scripts = scripts;
    }

    private void ShowScript()
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
                        isFinished = true;
                        isPlaying = false;
                    }
                }
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

    private void FindScript(string scriptName)
    {
        for (int i = 0; i < script.scripts.Length; i++)
            if (script.scripts[i].scriptName == scriptName)
            {
                currentScript = script.scripts[i];
                break;
            }
        currentLine = 0;
    }
}
