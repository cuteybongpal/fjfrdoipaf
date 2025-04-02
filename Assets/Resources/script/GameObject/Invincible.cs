using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Invincible : Item
{
    public float duration;
    PlayerController player;
    public override void UseItem()
    {
        player = FindAnyObjectByType<PlayerController>();
        StartCoroutine(Invin());
        base.UseItem();
    }
    IEnumerator Invin()
    {
        player.Invincible = true;
        yield return new WaitForSeconds(duration);
        player.Invincible = false;
        Destroy(gameObject);
    }
    public override object Clone()
    {
        GameObject go = new GameObject() { name = "Item" };
        Invincible i = go.AddComponent<Invincible>();
        i.duration = duration;
        i.Sprite = Sprite;
        i.Weight = Weight;

        return i;
    }

}
