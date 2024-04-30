using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Script
{
    [Tooltip("스크립트 번호")]
    public string scriptNum;
    [Tooltip("스크립트 이름")]
    public string scriptName;
    [Tooltip("스크립트 내용")]
    public string[] sentences;
}

[System.Serializable]
public class ScriptEvent
{
    public Vector2 line; // 몇 번째 대사를 추출할지 나타내는 변수
    public Script[] scripts;
}
