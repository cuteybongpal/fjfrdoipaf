using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_Warning : MonoBehaviour
{
    public void Init(string message)
    {
        Text text = GetComponentInChildren<Text>();
        text.text = message;
        StartCoroutine(DestroySelf());
    }
    IEnumerator DestroySelf()
    {
        yield return new WaitForSeconds(1);
        Destroy(gameObject);
    }
}
