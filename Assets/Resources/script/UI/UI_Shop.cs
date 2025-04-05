using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_Shop : UI_Popup
{
    public GameObject Product;
    public Transform Content;
    public Button Close;
    GameObject[] products = new GameObject[3];
    void Start()
    {
        Init();

        for (int i = 0; i < ProductManager.Instance.ProductNames.Length; i++)
        {
            UI_Product product = Instantiate(Product).GetComponent<UI_Product>();
            product.productNum = i;
            product.transform.SetParent(Content.transform);
            products[i] = product.gameObject;
        }

        Close.onClick.AddListener(() =>
        {
            Hide();
        });
    }
    public void Refresh()
    {
        for (int i = 0; i < products.Length; i++)
        {
            Destroy(products[i]);
            products[i] = null;
        }

        for (int i = 0; i < ProductManager.Instance.ProductNames.Length; i++)
        {
            UI_Product product = Instantiate(Product).GetComponent<UI_Product>();
            product.productNum = i;
            product.transform.SetParent(Content.transform);
            products[i] = product.gameObject;
        }
    }
}
