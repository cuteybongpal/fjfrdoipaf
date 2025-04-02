using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;
using UnityEngine.UI;

public class UI_Ad : UI_Popup
{
    public GameObject UI_TreasureInventory;
    public Transform Content;
    public Text Total;
    public Button Close;
    void Start()
    {
        Init();
        Close.onClick.AddListener(() =>
        {
            Hide();
        });
        Close.gameObject.SetActive(false);
        StartCoroutine(ad());
    }

    IEnumerator ad()
    {
        int totalPrice = 0;
        foreach (IStorable i in GameManager.Instance.Inventory)
        {
            Treasure t = i as Treasure;
            if (t == null)
            {
                continue;
            }
            yield return new WaitForSeconds(.1f);
            UI_InventoryTreasure ui = Instantiate(UI_TreasureInventory).GetComponent<UI_InventoryTreasure>();
            ui.Init(t);
            ui.gameObject.transform.SetParent(Content.transform);
            GameManager.Instance.Score += 10;
            totalPrice += t.Worth;
            Total.text = $"{totalPrice}";
        }
        GameManager.Instance.CurrentMoney += totalPrice;
        GameManager.Instance.InventoryClear();
        Close.gameObject.SetActive(true);
    }
}
