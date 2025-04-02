using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OxygenPill : Item
{
    public int Oxygen;

    public override void UseItem()
    {
        PlayerController player = FindAnyObjectByType<PlayerController>();
        player.CurrentO2 += Oxygen;
        base.UseItem();
        Destroy(gameObject);
    }

    public override object Clone()
    {
        GameObject go = new GameObject() { name = "Item" };
        OxygenPill op = go.AddComponent<OxygenPill>();
        op.Oxygen = Oxygen;
        op.Sprite = Sprite;
        op.Weight = Weight;

        return op;
    }
}
