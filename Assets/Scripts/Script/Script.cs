using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Script
{
    [Tooltip("��ũ��Ʈ ��ȣ")]
    public string scriptNum;
    [Tooltip("��ũ��Ʈ �̸�")]
    public string scriptName;
    [Tooltip("��ũ��Ʈ ����")]
    public string[] sentences;
    [Tooltip("��ũ��Ʈ ȿ��")]
    public string effect;
}

[System.Serializable]
public class ScriptEvent
{
    public Vector2 line; // �� ��° ��縦 �������� ��Ÿ���� ����
    public Script[] scripts;
}
