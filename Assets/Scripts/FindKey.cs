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
    // 스테이지 1 : 열쇠 찾기

    public static FindKey instance;
    public MissionUI ui;
    public List<string> getKeyList; // 얻은 키 리스트
    public GameObject keyUI;
    public GameObject keyButtons; // 열쇠 버튼들의 부모 오브젝트
    public GameObject p_keyHole; // 클릭한 열쇠가 들어갈 패널들의 부모 오브젝트
    public List<GameObject> clickKeyList; // 클릭한 열쇠 오브젝트 리스트
    public List<Transform> keyHoleList; // 클릭한 열쇠가 들어갈 패널 리스트
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
        foreach(Transform trans in p_keyHole.transform) // p_keyhole의 first depth만을 순회함
            keyHoleList.Add(trans);                     // => keyHole들의 자식은 포함되지 않음
        answerList = new List<string> { "PINK", "ORANGE", "YELLOW", "GREEN" };
        /*
        keys = new List<Transform>(); // 열쇠 오브젝트들을 담을 리스트
        GameObject parentKey = GameObject.Find("Keys"); // 부모 오브젝트 찾기
        keys = parentKey.GetComponentsInChildren<Transform>().ToList(); // 자식 오브젝트들(열쇠) 리스트에 추가
        keys.RemoveAt(0); // 리스트 중 부모 오브젝트 제거
        */
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // 열쇠 충돌 감지
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
        // 방금 클릭한 버튼 저장
        GameObject clickObject = EventSystem.current.currentSelectedGameObject;
        clickObject.GetComponent<Button>().interactable = false;
        // clickObject의 이름 clickKeyList에 저장
        clickKeyList.Add(clickObject);
        // 스프라이트 해당 열쇠로 변경
        keyHoleList[clickKeyList.Count - 1].GetChild(0).GetComponent<Image>().sprite 
            = Resources.Load<Sprite>(clickKeyList[clickKeyList.Count - 1].name);
        // 스프라이트 투명도 255로 설정
        Color color = keyHoleList[clickKeyList.Count - 1].GetChild(0).GetComponent<Image>().color;
        color.a = 100f;
        keyHoleList[clickKeyList.Count - 1].GetChild(0).GetComponent<Image>().color = color;

        // 만약 열쇠 리스트가 다 찼다면
        if (clickKeyList.Count == 4)
        {
            bool isAnswer = true;
            for (int i = 0; i < clickKeyList.Count; i++)
            {
                if (clickKeyList[i].name != answerList[i])
                {
                    isAnswer = false;
                    break; // 하나라도 틀린 경우 반복문 종료
                }
            }
            // 정답일 경우
            if (isAnswer)
            {
                OnExitButtonClick(); // UI를 닫고 clickKeyList 초기화
                ScriptManager.instance.FindScript("STAGE_1_CLEAR_1");
                ScriptManager.instance.ShowScript();
            }
            // 오답일 경우
            else
            {
                OnExitButtonClick(); // UI를 닫고 clickKeyList 초기화
                ScriptManager.instance.FindScript("STAGE_1_FAIL_2");
                ScriptManager.instance.ShowScript();
            }
        }
    }

    public void OnExitButtonClick()
    {
        // 리스트 초기화
        clickKeyList.Clear();
        // 스프라이트 투명도 0으로 설정
        for (int i = 0; i < keyHoleList.Count; i++)
        {
            Color color = keyHoleList[i].GetChild(0).GetComponent<Image>().color;
            color.a = 0f;
            keyHoleList[i].GetChild(0).GetComponent<Image>().color = color;
        }
        // 버튼 interactable false로 돌려 놓기
        for (int i = 0; i < keyButtons.GetComponentsInChildren<Button>().Length; i++)
            keyButtons.GetComponentsInChildren<Button>()[i].interactable = false;

        keyUI.SetActive(false);
    }
}
