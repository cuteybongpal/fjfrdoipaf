using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Guidance : Item
{
    public GameObject Arrow;
    PlayerController player;
    public override void UseItem()
    {
        player = FindFirstObjectByType<PlayerController>();
        StartCoroutine(RotateArrow());
        base.UseItem();
    }
    IEnumerator RotateArrow()
    {
        Treasure[] treasures = FindObjectsByType<Treasure>(FindObjectsSortMode.None);
        GameObject arrow = Instantiate(Arrow);
        Vector3 TargetPos = Vector3.zero;
        if (GameManager.Instance[0])
        {
            foreach (Treasure t in treasures)
            {
                if (!GameManager.Instance[t.TreasureNum])
                {
                    TargetPos = t.transform.position;
                    break;
                }
            }
        }
        else
        {
            foreach (Treasure t in treasures)
            {
                if (t.TreasureNum == 0)
                {
                    TargetPos = t.transform.position;
                }
            }
        }

        float elapsedTime = 0f;
        float duration = 5f;
        while (elapsedTime < duration)
        {
            Vector3 pos = TargetPos - player.transform.position;
            pos = new Vector3(pos.x, 0, pos.z).normalized;
            arrow.transform.position = player.transform.position + pos;
            arrow.transform.rotation = Quaternion.LookRotation(pos);
            arrow.transform.Rotate(90, 0, 0);
            Debug.Log(transform.position);
            yield return null;
            elapsedTime += Time.deltaTime;
        }
        Destroy(gameObject);
        Destroy(arrow);
    }
    public override object Clone()
    {
        GameObject go = new GameObject() { name = "Item" };
        Guidance item = go.AddComponent<Guidance>();
        item.Weight = Weight;
        item.Sprite = Sprite;
        item.Arrow = Arrow;
        item.player = player;

        return item;
    }
}
