using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_Start : UI_Base
{
    public Button S;
    public Button Quit;
    public Button Ranking;


    public GameObject UI_Ranking;
    void Start()
    {
        DataManager.Instance.Init();
        Init();
        S.onClick.AddListener(() =>
        {
            GameManager.Instance.CurrentScene = Define.Scenes.Lobby;
        });
        Quit.onClick.AddListener(() =>
        {
            Application.Quit();
        });
        Ranking.onClick.AddListener(() =>
        {
            Instantiate(UI_Ranking);
        });
    }
}
