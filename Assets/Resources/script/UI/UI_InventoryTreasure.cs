using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_InventoryTreasure : MonoBehaviour
{
    public Image TreasureImage;
    public Text Worth;
    public void Init(Treasure treasure)
    {
        TreasureImage.sprite = treasure.Sprite;
        Worth.text = $"+{treasure.Worth}";
    }
}
