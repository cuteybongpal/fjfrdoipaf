using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_GameOver : MonoBehaviour
{
    public Button ToLobby;
    public void Init()
    {
        ToLobby.onClick.AddListener(() =>
        {
            GameManager.Instance.CurrentScene = Define.Scenes.Lobby;
            Debug.Log("ddf");
        });
    }
}
