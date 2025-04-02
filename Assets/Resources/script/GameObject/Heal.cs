using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Heal : Item
{
    public override void UseItem()
    {
        PlayerController player = FindAnyObjectByType<PlayerController>();
        player.CurrentHp++;
        base.UseItem();
        Destroy(gameObject);
    }

    public override object Clone()
    {
        GameObject go = new GameObject() { name = "Item" };
        Heal heal = go.AddComponent<Heal>();
        heal.Sprite = Sprite;
        heal.Weight = Weight;

        return heal;
    }
}
