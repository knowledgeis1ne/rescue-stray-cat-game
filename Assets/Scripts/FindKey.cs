using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics.Tracing;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class FindKey : MonoBehaviour
{
    // �������� 1 : ���� ã��

    public static FindKey instance;
    public MissionUI ui;
    public List<string> getKeyList; // ���� Ű ����Ʈ
    public GameObject keyUI;
    public GameObject keyButtons; // ���� ��ư���� �θ� ������Ʈ
    public GameObject p_keyHole; // Ŭ���� ���谡 �� �гε��� �θ� ������Ʈ
    public List<GameObject> clickKeyList; // Ŭ���� ���� ������Ʈ ����Ʈ
    public List<Transform> keyHoleList; // Ŭ���� ���谡 �� �г� ����Ʈ
    public List<string> answerList;
    public bool isCompleted = false;

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        getKeyList = new List<string>();
        clickKeyList = new List<GameObject>();
        foreach(Transform trans in p_keyHole.transform) // p_keyhole�� first depth���� ��ȸ��
            keyHoleList.Add(trans);                     // => keyHole���� �ڽ��� ���Ե��� ����
        answerList = new List<string> { "PINK", "ORANGE", "YELLOW", "GREEN" };
        /*
        keys = new List<Transform>(); // ���� ������Ʈ���� ���� ����Ʈ
        GameObject parentKey = GameObject.Find("Keys"); // �θ� ������Ʈ ã��
        keys = parentKey.GetComponentsInChildren<Transform>().ToList(); // �ڽ� ������Ʈ��(����) ����Ʈ�� �߰�
        keys.RemoveAt(0); // ����Ʈ �� �θ� ������Ʈ ����
        */
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // ���� �浹 ����
        if (collision.gameObject.tag == "Key")
        {
            collision.gameObject.SetActive(false);
            getKeyList.Add(collision.gameObject.name);
        }
    }

    public void ShowKeyPanel()
    {
        keyUI.SetActive(true);

        for (int i = 0; i < keyButtons.GetComponentsInChildren<Button>().Length; i++)
            for (int j = 0; j < getKeyList.Count; j++)
                if (keyButtons.GetComponentsInChildren<Button>()[i].name == getKeyList[j])
                    keyButtons.GetComponentsInChildren<Button>()[i].interactable = true;
    }

    public void OnKeyButtonClick()
    {
        // ��� Ŭ���� ��ư ����
        GameObject clickObject = EventSystem.current.currentSelectedGameObject;
        clickObject.GetComponent<Button>().interactable = false;
        // clickObject�� �̸� clickKeyList�� ����
        clickKeyList.Add(clickObject);
        // ��������Ʈ �ش� ����� ����
        keyHoleList[clickKeyList.Count - 1].GetChild(0).GetComponent<Image>().sprite 
            = Resources.Load<Sprite>(clickKeyList[clickKeyList.Count - 1].name);
        // ��������Ʈ ���� 255�� ����
        Color color = keyHoleList[clickKeyList.Count - 1].GetChild(0).GetComponent<Image>().color;
        color.a = 100f;
        keyHoleList[clickKeyList.Count - 1].GetChild(0).GetComponent<Image>().color = color;

        // ���� ���� ����Ʈ�� �� á�ٸ�
        if (clickKeyList.Count == 4)
        {
            bool isAnswer = true;
            for (int i = 0; i < clickKeyList.Count; i++)
            {
                if (clickKeyList[i].name != answerList[i])
                {
                    isAnswer = false;
                    break; // �ϳ��� Ʋ�� ��� �ݺ��� ����
                }
            }
            // ������ ���
            if (isAnswer)
            {
                OnExitButtonClick(); // UI�� �ݰ� clickKeyList �ʱ�ȭ
                ScriptManager.instance.FindScript("STAGE_1_CLEAR_1");
                ScriptManager.instance.ShowScript();
            }
            // ������ ���
            else
            {
                OnExitButtonClick(); // UI�� �ݰ� clickKeyList �ʱ�ȭ
                ScriptManager.instance.FindScript("STAGE_1_FAIL_2");
                ScriptManager.instance.ShowScript();
            }
        }
    }

    public void OnExitButtonClick()
    {
        // ����Ʈ �ʱ�ȭ
        clickKeyList.Clear();
        // ��������Ʈ ���� 0���� ����
        for (int i = 0; i < keyHoleList.Count; i++)
        {
            Color color = keyHoleList[i].GetChild(0).GetComponent<Image>().color;
            color.a = 0f;
            keyHoleList[i].GetChild(0).GetComponent<Image>().color = color;
        }
        // ��ư interactable false�� ���� ����
        for (int i = 0; i < keyButtons.GetComponentsInChildren<Button>().Length; i++)
            keyButtons.GetComponentsInChildren<Button>()[i].interactable = false;

        keyUI.SetActive(false);
    }
}
