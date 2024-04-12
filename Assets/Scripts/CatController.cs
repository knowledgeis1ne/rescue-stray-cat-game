using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CatController : MonoBehaviour
{
    private Animator anim;
    public string[] animNames; // �ִϸ��̼� Ŭ���� �̸��� ����
    private int currentIndex = 0;
    WaitForSeconds ws; // �ڷ�ƾ�� ���� ����

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
            // (Sit �ִϸ��̼��� �ƴ� ���) 3�� ���
            yield return ws;
            // ���� �ִϸ��̼� Ŭ�� ��Ȱ��ȭ
            anim.SetBool(animNames[currentIndex], false);
            // ���� �ִϸ��̼� Ŭ������ �̵�
            currentIndex = (currentIndex + 1) % animNames.Length; // �迭 �ε��� �ʰ� ����
            // ���� �ִϸ��̼� Ŭ�� Ȱ��ȭ
            anim.SetBool(animNames[currentIndex], true);
        }
    }
}
