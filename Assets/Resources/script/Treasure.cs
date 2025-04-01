using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Treasure : MonoBehaviour, IStorable
{
    public string Name;
    public int Worth;
    public Sprite Sprite;
    public int Weight;
    public string Description;
    public int TreasureNum;
    public object Clone()
    {
        GameObject go = new GameObject() { name = "treasure" };
        Treasure t = go.AddComponent<Treasure>();
        t.Name = Name;
        t.Worth = Worth;
        t.Sprite = Sprite;
        t.Weight = Weight;
        return t;
    }

    public void Store()
    {
        GameManager.Instance.AddToInventory(this);
        GameManager.Instance[TreasureNum] = true;
        Destroy(this);
    }
}

