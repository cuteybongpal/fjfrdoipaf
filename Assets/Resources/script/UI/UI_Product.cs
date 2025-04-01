using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_Product : MonoBehaviour
{
    public Text productName;
    public Text productLv;

    public Image productImage;
    public Button UpGrade;

    public int productNum;

    void Start()
    {
        productName.text = $"{ProductManager.Instance.ProductNames[productNum]}";
        productImage.sprite = ProductManager.Instance.ProductSprites[productNum];

        productLv.text = $"Lv. {ProductManager.Instance.ProductLevels[productNum]}";
        UpGrade.GetComponentInChildren<Text>().text = $"{ProductManager.Instance.ProductCost[productNum]}";
        UpGrade.onClick.AddListener(() =>
        {
            ProductManager.Instance.UpGrade(productNum);
            Refresh();
        });
        if (ProductManager.Instance.ProductLevels[productNum] >= ProductManager.Instance.ProductMaxLevel[productNum])
        {
            UpGrade.GetComponentInChildren<Text>().text = "업그레이드 불가";
            UpGrade.GetComponent<Image>().color = Color.white;
        }
    }
    void Refresh()
    {
        productName.text = $"{ProductManager.Instance.ProductNames[productNum]}";
        productImage.sprite = ProductManager.Instance.ProductSprites[productNum];

        productLv.text = $"Lv. {ProductManager.Instance.ProductLevels[productNum]}";
        UpGrade.GetComponentInChildren<Text>().text = $"{ProductManager.Instance.ProductCost[productNum]}";
        if (ProductManager.Instance.ProductLevels[productNum] >= ProductManager.Instance.ProductMaxLevel[productNum])
        {
            UpGrade.GetComponentInChildren<Text>().text = "업그레이드 불가";
            UpGrade.GetComponent<Image>().color = Color.white;
        }
    }
}
