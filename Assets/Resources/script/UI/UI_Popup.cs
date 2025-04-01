using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_Popup : UI_Base
{
    protected override void Init()
    {
        Show();
    }
    public void Show()
    {
        UIManager.Instance.Push(this);
    }
    public void Hide()
    {
        UIManager.Instance.Pop();
    }
}
