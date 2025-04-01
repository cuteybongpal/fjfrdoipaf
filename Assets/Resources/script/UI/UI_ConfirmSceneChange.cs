using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_ConfirmSceneChange : UI_Popup
{
    public Button Close;
    public Button Move;
    public Text text;
    

    public void Init(string moveText, Define.Scenes sceneNum)
    {
        Init();
        text.text = moveText;
        Close.onClick.AddListener(() =>
        {
            Hide();
        });
        Move.onClick.AddListener(() =>
        { 
            GameManager.Instance.CurrentScene = sceneNum;
        });
    }
}
