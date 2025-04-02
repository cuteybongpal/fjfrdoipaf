using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class UI_Maze : UI_Popup
{
    public Button Close;
    public Button Move;
    public Text ClearText;

    UI_MazeCell[,] MazeMap = new UI_MazeCell[10, 10];

    public Transform Cell;

    List<UI_MazeCell> paths = new List<UI_MazeCell>();
    Cage cage;
    public void Initialize(Cage cage)
    {
        Init();
        this.cage = cage;
        Move.onClick.AddListener(() =>
        {
            StartCoroutine(MoveStart());
        });
        Close.onClick.AddListener(() =>
        {
            Hide();
        });
        for (int y = 0; y < 10; y++)
        {
            for (int x = 0; x < 10; x++)
            {
                MazeMap[y, x] = Cell.GetChild(y * 10 + x).GetComponent<UI_MazeCell>();
            }
        }
        ClearText.gameObject.SetActive(false);
    }
    public void AddPath(UI_MazeCell cell)
    {
        int cellPosX = 0;
        int cellPosY = 0;
        Vector2Int[] dirs = { Vector2Int.up, Vector2Int.down, Vector2Int.left, Vector2Int.right };
        for (int y = 0; y < 10; y++)
        {
            bool isFind = false;
            for (int x = 0; x < 10; x++)
            {
                if (MazeMap[y, x] == cell)
                {
                    cellPosX = x;
                    cellPosY = y;
                    isFind = true;
                    break;
                }
                if (isFind)
                    break;
            }
        }

        for (int i = 0; i < dirs.Length; i++)
        {
            if (cellPosX + dirs[i].x < 0 || cellPosX + dirs[i].x > 9)
                continue;
            if (cellPosY + dirs[i].y < 0 || cellPosY + dirs[i].y > 9)
                continue;

            UI_MazeCell ce = MazeMap[cellPosY + dirs[i].y, cellPosX + dirs[i].x];
            if (paths.Count == 0)
            {
                if (ce.Type == UI_MazeCell.CellType.Player)
                {
                    paths.Add(cell);
                    cell.Type = UI_MazeCell.CellType.Path;
                    cell.SetColor(UI_MazeCell.CellType.Path);
                }
            }
            else
            {
                if (ce == paths[^1])
                {
                    paths.Add(cell);
                    if (cell.Type != UI_MazeCell.CellType.Goal && cell.Type != UI_MazeCell.CellType.Player)
                    {
                        cell.Type = UI_MazeCell.CellType.Path;
                        cell.SetColor(UI_MazeCell.CellType.Path);
                    }
                }
            }
        }
    }

    public void RemovePath(UI_MazeCell cell)
    {
        int index = paths.IndexOf(cell);

        if (index == -1)
            return;
        for (int i = paths.Count - 1; i >= index; i--)
        {
            UI_MazeCell ui = paths[i];
            paths.RemoveAt(i);
            ui.Type = UI_MazeCell.CellType.None;
            ui.SetColor(UI_MazeCell.CellType.None);
        }

    }

    IEnumerator MoveStart()
    {
        MazeMap[1, 1].Type = UI_MazeCell.CellType.None;
        MazeMap[1, 1].SetColor(UI_MazeCell.CellType.None);
        UI_MazeCell prevCell = null;
        foreach (UI_MazeCell cell in paths)
        {
            cell.SetColor(UI_MazeCell.CellType.Player);
            if (prevCell != null)
            {
                MazeMap[1, 1].Type = UI_MazeCell.CellType.None;
                prevCell.SetColor(UI_MazeCell.CellType.None);
            }
            if (cell.Type == UI_MazeCell.CellType.Goal)
            {
                ClearText.gameObject.SetActive(true);
                Clear();
                yield return new WaitForSeconds(1);
                Hide();
            }
            prevCell = cell;
            yield return new WaitForSeconds(.1f);
        }
        Hide();
    }
    void Clear()
    {
        cage.Clear();
    }
}

