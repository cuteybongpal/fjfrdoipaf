using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_InventoryItem : MonoBehaviour
{
    public Image InventoryItemImage;
    Button button;
    public void Init(IStorable storable)
    {
        if (storable == null)
        {
            InventoryItemImage.sprite = null;
            return;
        }
        button = GetComponentInChildren<Button>();
        button.onClick.RemoveAllListeners();
        if (storable is Treasure)
        {
            Treasure t = (Treasure)storable;
            InventoryItemImage.sprite = t.Sprite;
        }
        else
        {
            Item t = (Item)storable;
            InventoryItemImage.sprite = t.Sprite;
            button = GetComponentInChildren<Button>();
            button.onClick.AddListener(() =>
            {
                t.UseItem();
            });
        }
    }
}
