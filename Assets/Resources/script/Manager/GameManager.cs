using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    private static GameManager instance;
    public static GameManager Instance { get { return instance; } }

    public int MaxPlayerO2 = 100;
    public int PlayerCurrentHp;
    public int PlayerCurrentO2;
    public float PlayerSpeed;
    float OriginalPlayerSpeed;
    public int PlayerAttack;
    public int Score = 1000;

    public Vector3[] ReSpawnPos = new Vector3[5];

    int currentMoney = 10000;
    public int CurrentMoney
    {
        get { return currentMoney; }
        set
        {
            currentMoney = value;
            UI_Lobby ui = UIManager.Instance.GetMainUI<UI_Lobby>();
            if (ui == null)
                return;
            ui.SetMoney(currentMoney);
        }
    }

    public List<IStorable> Inventory = new List<IStorable>();
    public int MaxStorableTreasureCount = 4;
    public int MaxStroableTreasureWeight = 200;
    public Define.Scenes currentScene = Define.Scenes.Lobby;
    public List<bool>[] IsStageTreasureFind;
    public bool Clear = false;
    public GameObject UI_GameOver;
    public Func<PlayerController> GetPlayer;
    public bool[] isPuzzleCleared = new bool[5];

    public GameObject UI_Warning;
    public Define.Scenes CurrentScene
    {
        get { return currentScene; }
        set
        {
            Time.timeScale = 1f;
            if (value == Define.Scenes.Stage1 && currentScene == Define.Scenes.Lobby)
            {
                ReSpawnPos[0] = Vector3.zero;
                GameStart();
            }
            else if (value == Define.Scenes.Lobby && CurrentScene == Define.Scenes.Stage1)
                GameClear();
            else if ((int)value >= (int)Define.Scenes.Stage1 && (int)currentScene < (int)value && !Clear)
            {
                StageClear();
            }
            if ((int)value < (int)CurrentScene && (int)currentScene >= (int)Define.Scenes.Stage1)
            {
                ReSpawnPos[CurrentStage] = Vector3.zero;
            }

            currentScene = value;
            Debug.Log("daa");
            SceneManager.LoadSceneAsync((int)currentScene);
        }
    }
    public int CurrentStage { get { return (int)currentScene - 2; } }
    public bool this[int index]
    {
        get { return IsStageTreasureFind[CurrentStage][index]; }
        set { IsStageTreasureFind[CurrentStage][index] = value; }
    }

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
            OriginalPlayerSpeed = PlayerSpeed;
        }
        else
        {
            Destroy(gameObject);
        }
    }
    public bool AddToInventory(IStorable storable)
    {
        if (Inventory.Count >= MaxStorableTreasureCount)
        {
            Instantiate(UI_Warning).GetComponent<UI_Warning>().Init("인벤토리 칸이 부족합니다.");
            return false;
        }
        int weight = 0;
        if (storable is Item)
        {
            Item it = storable as Item;
            weight += it.Weight;
        }
        else if (storable is Treasure)
        {
            Treasure t = storable as Treasure;
            weight += t.Weight;
        }
        foreach (IStorable st in Inventory)
        {
            if (st is Item)
            {
                Item it = st as Item;
                weight += it.Weight;
            }
            else if (st is Treasure)
            {
                Treasure t = st as Treasure;
                weight += t.Weight;
            }
        }
        if (weight > MaxStroableTreasureWeight)
        {
            PlayerSpeed /= 1.5f;
        }
        else
        {
            PlayerSpeed = OriginalPlayerSpeed;
        }
        IStorable stt = storable.Clone() as IStorable;
        if (stt is Item)
        {
            Item i = stt as Item;
            DontDestroyOnLoad(i.gameObject);
            Inventory.Add(i);
        }
        else
        {
            Treasure t =  stt as Treasure;
            DontDestroyOnLoad (t.gameObject);
            Inventory.Add(t);
        }
        var ui = UIManager.Instance.GetMainUI<UI_Player>();
        if (ui == null)
            return true;
        ui.Refresh();
        return true;
    }
    public void RemoveItemFromInventory(IStorable storable)
    {
        Inventory.Remove(storable);
        UI_Player ui = UIManager.Instance.GetMainUI<UI_Player>();
        if (ui == null) return;
        ui.Refresh();
    }
    public void GameOver()
    {
        PlayerCurrentHp = 0;
        PlayerCurrentO2 = 0;
        Instantiate(UI_GameOver).GetComponent<UI_GameOver>().Init();
        for (int i = Inventory.Count - 1; i > 0; i-- )
        {
            IStorable st = Inventory[i];
            if (st is Item)
                Destroy(st as Item);
            else
                Destroy(st as Treasure);
        }
        IsStageTreasureFind = new List<bool>[5];
        int index = 0;
        foreach (List<bool> list in DataManager.Instance.IsStageTreasureFind)
        {
            IsStageTreasureFind[index] = new List<bool>();
            foreach (bool item in list)
            {
                IsStageTreasureFind[index].Add(item);
            }
            index++;
        }
        Score -= 100;
        Inventory = new List<IStorable>();
        Time.timeScale = 0f;
    }
    public void GameStart()
    {
        IsStageTreasureFind = new List<bool>[5];
        int index = 0;
        foreach(List<bool> list in DataManager.Instance.IsStageTreasureFind)
        {
            IsStageTreasureFind[index] = new List<bool>();
            foreach (bool item in list)
            {
                IsStageTreasureFind[index].Add(item);
            }
            index++;
        }
    }
    public void GameClear()
    {
        PlayerCurrentHp = 0;
        PlayerCurrentO2 = 0;
        DataManager.Instance.IsStageTreasureFind = new List<bool>[5];
        for (int i = 0; i < IsStageTreasureFind.Length; i++)
        {
            DataManager.Instance.IsStageTreasureFind[i] = new List<bool>();
            for (int j = 0; j < IsStageTreasureFind[i].Count; j++)
            {
                DataManager.Instance.IsStageTreasureFind[i].Add(IsStageTreasureFind[i][j]);
            }
        }
        bool isAllFind = true;

        for (int i = 0; i < DataManager.Instance.IsStageTreasureFind.Length; i++)
        {
            isAllFind &= DataManager.Instance.IsStageTreasureFind[i][0];
        }
        if (isAllFind)
        {
            Clear = true;
            DataManager.Instance.Add(Score);
        }
        PlayerSpeed = OriginalPlayerSpeed;
    }
    public void StageClear()
    {
        PlayerController player = GetPlayer.Invoke();
        PlayerCurrentHp = player.CurrentHp;
        PlayerCurrentO2 = player.CurrentO2;
    }
    public void InventoryClear()
    {
        for (int i = Inventory.Count - 1; i > 0 ;i--)
        {
            IStorable st = Inventory[i];
            if (st is Treasure)
            {
                Treasure t = st as Treasure;
                Destroy(t.gameObject);
            }
            else
            {
                Item it = st as Item;
                Destroy(it.gameObject);
            }
        }
        Inventory = new List<IStorable>();
    }

    public void ResetManager()
    {
        GameObject go = new GameObject() { name = "GameManager" };
        GameManager manager = go.AddComponent<GameManager>();
        instance = null;
        Destroy(gameObject);
    }
}
