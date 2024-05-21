using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using UnityEngine;

public class ScriptParser : MonoBehaviour
{
    public Script[] Parse(string csv)
    {
        List<Script> scriptList = new List<Script>(); // 스크립트 리스트 생성
        TextAsset csvData = Resources.Load<TextAsset>(csv); // csv 파일 가져오기

        string[] data = csvData.text.Split(new char[] { '\n' }, System.StringSplitOptions.RemoveEmptyEntries); // 엔터 단위로 쪼개기

        for (int i = 1; i < data.Length;)
        {
            string[] row = data[i].Split(new char[] { ',' }, System.StringSplitOptions.None); // 각 열에서 콤마 단위로 쪼개기

            if (row.Length < 4)
            {
                Debug.LogError("CSV 파일의 데이터 형식이 올바르지 않습니다: " + data[i]);
                i++;
                continue;
            }

            Script script = new Script();

            string scriptNum = row[0].Trim();
            string scriptName = row[1].Trim();
            string effect = row[3].Trim();
            List<string> sentences = new List<string>();

            do
            {
                sentences.Add(row[2].Trim());
                if (++i < data.Length)
                {
                    row = data[i].Split(new char[] { ',' });
                }
                else break;
            }
            while (string.IsNullOrWhiteSpace(row[0]));

            script.scriptNum = scriptNum;
            script.scriptName = scriptName;
            script.effect = effect;
            script.sentences = sentences.ToArray();

            scriptList.Add(script);
        }

        return scriptList.ToArray();
    }
}
