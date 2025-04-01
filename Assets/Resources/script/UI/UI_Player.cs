using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_Player : UI_Base
{
    public Transform Inventory;
    public GameObject[] HPs;
    public Slider O2Slider;
    public Text O2Text;
    public Text WeightText;

    public GameObject InventoryItem;
    UI_InventoryItem[] inventoryItems;
    void Start()
    {
        Init();
        O2Slider.maxValue = GameManager.Instance.MaxPlayerO2;
        inventoryItems = new UI_InventoryItem[GameManager.Instance.MaxStorableTreasureCount];
        for (int i = 0; i < GameManager.Instance.MaxStorableTreasureCount; i++)
        {
            GameObject go = Instantiate(InventoryItem);
            go.transform.parent = Inventory;
            inventoryItems[i] = go.GetComponent<UI_InventoryItem>();
        }
        Refresh();
    }

    public void SetO2(int value)
    {
        O2Slider.value = value;
        O2Text.text = $"남은 산소량 : {value}";
    }
    public void Refresh()
    {

        foreach (UI_InventoryItem item in inventoryItems)
        {
            item.Init(null);
        }
        int weight = 0;
        for (int i = 0; i < GameManager.Instance.Inventory.Count; i++)
        {
            IStorable st = GameManager.Instance.Inventory[i];
            if (st is Treasure)
            {
                Treasure t = (Treasure)st;
                inventoryItems[i].Init(t);
                weight += t.Weight;
            }
            else
            {
                Item it = (Item)st;
                inventoryItems[i].Init(it);
                weight += it.Weight;
            }
        }
        WeightText.text = $"{weight} / {GameManager.Instance.MaxStroableTreasureWeight} kg";
    }
    public void SetHp(int value)
    {
        foreach (var go in HPs)
        {
            go.SetActive(true);
        }
        for(int i = value; i < HPs.Length; i++)
        {
            HPs[i].SetActive(false);
        }
    }
}
