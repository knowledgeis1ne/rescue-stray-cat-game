using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ScriptParser : MonoBehaviour
{
    public Script[] Parse(string csv)
    {
        List<Script> scriptList = new List<Script>(); // ��ũ��Ʈ ��ųʸ� ����
        TextAsset csvData = Resources.Load<TextAsset>(csv); // csv ���� ��������

        string[] data = csvData.text.Split(new char[] { '\n' }); // ���� ������ �ɰ���

        for (int i = 1; i < data.Length;)
        {
            string[] row = data[i].Split(new char[] { ',' }); // �� ������ �޸� ������ �ɰ���

            Script script = new Script();

            string scriptNum;
            string scriptName;
            List<string> sentences = new List<string>();

            scriptNum = row[0];
            scriptName = row[1];

            do
            {
                sentences.Add(row[2]);
                if (++i < data.Length)
                {
                    row = data[i].Split(new char[] { ',' });
                }
                else break;
            }
            while (row[0].ToString() == "");

            script.scriptName = string.Concat(scriptName.Where(x => !char.IsWhiteSpace(x)));
            script.sentences = sentences.ToArray();

            scriptList.Add(script);
        }

        return scriptList.ToArray();
    }
}
