using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CatController : MonoBehaviour
{
    private Animator anim;
    public string[] animNames; // 애니메이션 클립의 이름을 저장
    private int currentIndex = 0;
    WaitForSeconds ws; // 코루틴에 쓰일 변수

    private void Start()
    {
        ws = new WaitForSeconds(3f);
        anim = GetComponent<Animator>();
        anim.SetBool(animNames[currentIndex], true);
        StartCoroutine(ChangeAnimation());
    }

    IEnumerator ChangeAnimation()
    {
        while (true)
        {
            // (Sit 애니메이션이 아닐 경우) 3초 대기
            yield return ws;
            // 현재 애니메이션 클립 비활성화
            anim.SetBool(animNames[currentIndex], false);
            // 다음 애니메이션 클립으로 이동
            currentIndex = (currentIndex + 1) % animNames.Length; // 배열 인덱스 초과 방지
            // 다음 애니메이션 클립 활성화
            anim.SetBool(animNames[currentIndex], true);
        }
    }
}
