using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeedBoost : Item
{
    public float AddSpeed;
    public float duration;

    public override void UseItem()
    {
        StartCoroutine(Boost());
        base.UseItem();
    }
    IEnumerator Boost()
    {
        GameManager.Instance.PlayerSpeed += AddSpeed;
        yield return new WaitForSeconds(duration);
        GameManager.Instance.PlayerSpeed -= AddSpeed;

        Destroy(gameObject);
    }
    public override object Clone()
    {
        GameObject go = new GameObject() { name = "Item" };
        SpeedBoost speed = go.AddComponent<SpeedBoost>();
        speed.AddSpeed = AddSpeed;
        speed.duration = duration;
        speed.Sprite = Sprite;
        speed.Weight = Weight;
        return speed;
    }
}
