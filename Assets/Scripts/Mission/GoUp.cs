using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoUp : MonoBehaviour
{
    public void OnClickLetterButton()
    {
        ScriptManager.instance.FindScript("STAGE_4_CLEAR_2");
        gameObject.SetActive(false);
    }
}
