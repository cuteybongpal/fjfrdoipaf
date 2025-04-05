using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_Lobby : UI_Base
{
    public Text CoinText;
    public Button Shop;
    public Button ToMenu;

    public GameObject ShopUI;
    public GameObject ConfirmSceneChange;
    public GameObject UI_Ad;
    void Start()
    {
        Init();
        if (GameManager.Instance.Inventory.Count > 0)
        {
            Instantiate(UI_Ad);
        }
        SetMoney(GameManager.Instance.CurrentMoney);
        Shop.onClick.AddListener(() =>
        {
            Instantiate(ShopUI);
        });
        ToMenu.onClick.AddListener(() =>
        {
            Instantiate(ConfirmSceneChange).GetComponent<UI_ConfirmSceneChange>().Init("시작화면으로 돌아가시겠습니까?", Define.Scenes.StartScene);
        });
    }
    public void SetMoney(int money)
    {
        CoinText.text = $"{money} 원";
    }
}
