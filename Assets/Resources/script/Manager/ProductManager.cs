using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements.Experimental;

public class ProductManager : MonoBehaviour
{
    private static ProductManager instance;
    public static ProductManager Instance {  get { return instance; } }

    public string[] ProductNames;
    public int[] ProductLevels;
    public Sprite[] ProductSprites;
    public int[] ProductCost;
    public int[] ProductMaxLevel;

    public GameObject UI_Warning;
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void UpGrade(int productNum)
    {
        if (ProductCost[productNum] > GameManager.Instance.CurrentMoney)
        {
            Instantiate(UI_Warning).GetComponent<UI_Warning>().Init("돈이 부족합니다.");
            return;
        }
        if (ProductLevels[productNum] >= ProductMaxLevel[productNum])
            return;
        GameManager.Instance.CurrentMoney -= ProductCost[productNum];
        ProductLevels[productNum]++;
        switch (productNum)
        {
            case 0:
                ProductCost[productNum] += 300;
                GameManager.Instance.MaxPlayerO2 += 50;
                break;
            case 1:
                ProductCost[productNum] += 400;
                GameManager.Instance.MaxStorableTreasureCount += 2;
                GameManager.Instance.MaxStroableTreasureWeight += 50;
                break;
            case 2:
                ProductCost[productNum] += 500;
                GameManager.Instance.PlayerAttack++;
                break;
        }

    }
}
