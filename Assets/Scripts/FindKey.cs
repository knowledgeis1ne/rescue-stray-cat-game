using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class FindKey : MonoBehaviour
{
    // 스테이지 1 : 열쇠 찾기

    public List<Transform> keys;

    public int wholeCount;
    public int getCount = 0;

    private void Start()
    {
        keys = new List<Transform>(); // 열쇠 오브젝트들을 담을 리스트
        GameObject parentKey = GameObject.Find("Keys"); // 부모 오브젝트 찾기
        keys = parentKey.GetComponentsInChildren<Transform>().ToList(); // 자식 오브젝트들(열쇠) 리스트에 추가
        keys.RemoveAt(0); // 리스트 중 부모 오브젝트 제거
        wholeCount = keys.Count;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // 열쇠 충돌 감지
        if (collision.gameObject.tag == "Key")
        {
            collision.gameObject.SetActive(false);
            getCount++;
        }
    }
}
