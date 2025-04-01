using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrapSpear : MonoBehaviour
{
    public float DestY;
    public float CoolDown;
    void Start()
    {
        StartCoroutine(Commad());
    }
    IEnumerator Commad()
    {
        while(true)
        {
            yield return Up();
            yield return new WaitForSeconds(CoolDown);
            yield return Down();
            yield return new WaitForSeconds(CoolDown);
        }
    }
    IEnumerator Up()
    {
        float elapsedTime = 0;
        float duration = 1f;

        Vector3 origin = transform.position;
        Vector3 destPos = transform.position + transform.up * DestY;
        while(elapsedTime < duration)
        {
            Vector3 pos = Vector3.Lerp(origin, destPos, elapsedTime / duration);
            transform.position = pos;
            yield return null;
            elapsedTime += Time.deltaTime;
        }
    }
    IEnumerator Down()
    {
        float elapsedTime = 0;
        float duration = 1f;

        Vector3 origin = transform.position;
        Vector3 destPos = transform.position - transform.up * DestY;
        while (elapsedTime < duration)
        {
            Vector3 pos = Vector3.Lerp(origin, destPos, elapsedTime / duration);
            transform.position = pos;
            yield return null;
            elapsedTime += Time.deltaTime;
        }
    }
}
