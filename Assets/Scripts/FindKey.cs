using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class FindKey : MonoBehaviour
{
    // �������� 1 : ���� ã��

    public List<Transform> keys;

    public int wholeCount;
    public int getCount = 0;

    private void Start()
    {
        keys = new List<Transform>(); // ���� ������Ʈ���� ���� ����Ʈ
        GameObject parentKey = GameObject.Find("Keys"); // �θ� ������Ʈ ã��
        keys = parentKey.GetComponentsInChildren<Transform>().ToList(); // �ڽ� ������Ʈ��(����) ����Ʈ�� �߰�
        keys.RemoveAt(0); // ����Ʈ �� �θ� ������Ʈ ����
        wholeCount = keys.Count;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // ���� �浹 ����
        if (collision.gameObject.tag == "Key")
        {
            collision.gameObject.SetActive(false);
            getCount++;
        }
    }
}
