using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    private static UIManager instance;
    public static UIManager Instance {  get { return instance; } }

    public UI_Base currentMainUI;
    Stack<UI_Popup> ui_Popups = new Stack<UI_Popup>();
    public UI_Base CurrentMainUI { set { currentMainUI = value; } }
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
    public void Push(UI_Popup popup)
    {
        ui_Popups.Push(popup);
    }
    public void Pop()
    {
        UI_Popup popup = ui_Popups.Pop();
        Destroy(popup.gameObject);
    }
    public T GetMainUI<T>() where T : UI_Base
    {
        return currentMainUI as T;
    }
}
