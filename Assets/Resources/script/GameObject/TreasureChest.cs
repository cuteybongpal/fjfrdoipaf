using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using UnityEngine;

public class TreasureChest : MonoBehaviour
{
    Treasure treasure;
    public GameObject UI_TreasureInfo;
    UI_TreasureInfo ui;
    Animator anim;
    void Start()
    {
        treasure = GetComponent<Treasure>();
        anim = GetComponent<Animator>();
        if (GameManager.Instance[treasure.TreasureNum])
        {
            treasure = null;
            Open();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player"))
            return;
        if (ui != null)
            return;
        if (treasure == null)
            return;
        ui = Instantiate(UI_TreasureInfo).GetComponent<UI_TreasureInfo>();
        ui.Init(treasure);
        Open();
    }
    private void OnTriggerExit(Collider other)
    {
        if (!other.CompareTag("Player"))
            return;
        if (ui == null)
            return;
        if (treasure == null)
            return;

        ui.Hide();
        ui = null;
        Close();
    }
    void Open()
    {
        anim.Play("Open");
    }
    void Close()
    {
        anim.Play("Close");
    }
}
