using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cheat : MonoBehaviour
{
    bool isPause = false;
    static bool isExist = false;
    private void Start()
    {
        if (!isExist)
        {
            DontDestroyOnLoad(gameObject);
            isExist = true;
        }
        else
            Destroy(gameObject);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.F1))
        {
            PlayerController p = FindAnyObjectByType<PlayerController>();
            p.CurrentHp = p.MaxHp;
            p.CurrentO2 = GameManager.Instance.MaxPlayerO2;
        }

        if (Input.GetKeyDown(KeyCode.F2))
        {
            for (int i= 0; i < ProductManager.Instance.ProductCost.Length; i++)
            {
                ProductManager.Instance.ProductCost[i] = 0;
            }
            UI_Shop ui = UIManager.Instance.GetCurrentPopup<UI_Shop>();
            if (ui == null)
                return;
            ui.Refresh();
        }

        if (Input.GetKeyDown(KeyCode.F3))
        {
            GameManager.Instance.GameOver();
        }

        if (Input.GetKeyDown(KeyCode.F4))
        {
            GameManager.Instance.CurrentScene = (Define.Scenes)((GameManager.Instance.CurrentStage + 1) % 5 + 2);
        }

        if (Input.GetKeyDown(KeyCode.F5))
        {
            if (isPause)
            {
                Time.timeScale = 1;
            }
            else
            {
                Time.timeScale = 0;
            }
            isPause = !isPause;

        }
    }
}
