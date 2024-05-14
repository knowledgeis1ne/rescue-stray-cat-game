using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CatController : MonoBehaviour
{
    Animator anim;
    Rigidbody2D rigid;
    WaitForSeconds ws; // �ڷ�ƾ�� ���� ����
    public string[] animNames; // �ִϸ��̼� Ŭ���� �̸��� ����
    int currentIndex = 0;

    private void Start()
    {
        ws = new WaitForSeconds(3f);
        anim = GetComponent<Animator>();
        rigid = GetComponent<Rigidbody2D>();
        anim.SetBool(animNames[currentIndex], true);
        StartCoroutine(ChangeAnimation());
    }

    IEnumerator ChangeAnimation()
    {
        while (true)
        {
            // 3�� ���
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
