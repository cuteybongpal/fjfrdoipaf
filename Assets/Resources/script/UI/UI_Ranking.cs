using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_Ranking : UI_Popup
{
    public GameObject Ranker;
    public Transform rankTrasnform;
    public Button Close;
    void Start()
    {
        Init();
        DataManager.Instance.Ranking.Sort();
        DataManager.Instance.Ranking.Reverse();
        for (int i = 0; i < DataManager.Instance.Ranking.Count; i++)
        {
            UI_Ranker r = Instantiate(Ranker).GetComponent<UI_Ranker>();
            r.transform.parent = rankTrasnform;
            r.Init(i +1, DataManager.Instance.Ranking[i]);
        }
        Close.onClick.AddListener(() =>
        {
            Hide();
        });
    }
}
