using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using UnityEngine.SceneManagement;

public class Intro : MonoBehaviour
{
    public static Intro instance;

    public Transform fadeOutPanel;      // 페이드아웃 패널

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        StartCoroutine(FadeInPanel());

        string sceneName = SceneManager.GetActiveScene().name;
        switch (sceneName)
        {
            case "Intro Scene":
                ScriptManager.instance.FindScript("INTRO");
                break;
            case "Ending Scene":
                ScriptManager.instance.FindScript("ENDING");
                break;
        }
    }

    public IEnumerator FadeOutPanel(float transparent, Action onComplete = null)
    {
        yield return new WaitForSeconds(1f);

        WaitForSeconds wait = new WaitForSeconds(0.01f);

        if (!fadeOutPanel.gameObject.activeSelf)
            fadeOutPanel.gameObject.SetActive(true);

        Color c = fadeOutPanel.GetComponent<Image>().color;

        for (float f = 0f; f <= transparent; f += 0.025f)
        {
            c.a = f;
            fadeOutPanel.GetComponent<Image>().color = c;
            yield return wait;
        }

        onComplete?.Invoke(); // 콜백 호출
    }

    public IEnumerator FadeInPanel()
    {
        WaitForSeconds wait = new WaitForSeconds(0.01f);

        if (!fadeOutPanel.gameObject.activeSelf)
            fadeOutPanel.gameObject.SetActive(true);

        Color c = fadeOutPanel.GetComponent<Image>().color;

        for (float f = 1f; f >= 0f; f -= 0.05f)
        {
            c.a = f;
            fadeOutPanel.GetComponent<Image>().color = c;
            yield return wait;
        }

        fadeOutPanel.gameObject.SetActive(false);
    }
}
