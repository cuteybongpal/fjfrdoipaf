using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class UI_TreasureInfo : UI_Popup
{
    public Image TreasureImage;
    public Text TreasureName;
    public Text TreasureDescription;
    public Text TreasureWorth;
    public Text TreasureWeight;

    public Button Take;
    public Button Close;
    
    public void Init(Treasure treasure)
    {
        Init();
        TreasureImage.sprite = treasure.Sprite;
        TreasureDescription.text = treasure.Description;
        TreasureName.text = treasure.Name;
        TreasureWorth.text = $"{treasure.Worth}";
        TreasureWeight.text = $"{treasure.Weight}";

        Take.onClick.AddListener(() =>
        {
            treasure.Store();
            Hide();
        });
        Close.onClick.AddListener(() =>
        {
            Hide();
        });
    }
    
}

