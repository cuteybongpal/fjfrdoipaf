using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_Credit : UI_Base
{
    public GameObject credit;
    public Text ScoreText;
    void Start()
    {
        ScoreText.text += GameManager.Instance.Score;
        StartCoroutine(CreditDown());
    }
    IEnumerator CreditDown()
    {
        float elapsedTime = 0;
        float duration = 5;
        Vector3 origin = credit.transform.position;
        Vector3 target = credit.transform.position + Vector3.up * 1000;
        while (elapsedTime < duration)
        {
            Vector3 pos = Vector3.Lerp(origin, target, elapsedTime / duration);
            credit.transform.position = pos;
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        while (!Input.GetMouseButtonDown(0))
        {
            yield return null;
        }
        GameManager.Instance.CurrentScene = Define.Scenes.StartScene;
    }
}
