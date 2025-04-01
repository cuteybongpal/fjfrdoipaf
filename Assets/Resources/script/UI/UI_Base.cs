using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_Base : MonoBehaviour
{
    protected virtual void Init()
    {
        UIManager.Instance.CurrentMainUI = this;
    }
}
