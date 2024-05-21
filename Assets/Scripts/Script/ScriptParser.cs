using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using UnityEngine;

public class ScriptParser : MonoBehaviour
{
    public Script[] Parse(string csv)
    {
        List<Script> scriptList = new List<Script>(); // ��ũ��Ʈ ����Ʈ ����
        TextAsset csvData = Resources.Load<TextAsset>(csv); // csv ���� ��������

        string[] data = csvData.text.Split(new char[] { '\n' }, System.StringSplitOptions.RemoveEmptyEntries); // ���� ������ �ɰ���

        for (int i = 1; i < data.Length;)
        {
            string[] row = data[i].Split(new char[] { ',' }, System.StringSplitOptions.None); // �� ������ �޸� ������ �ɰ���

            if (row.Length < 4)
            {
                Debug.LogError("CSV ������ ������ ������ �ùٸ��� �ʽ��ϴ�: " + data[i]);
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
