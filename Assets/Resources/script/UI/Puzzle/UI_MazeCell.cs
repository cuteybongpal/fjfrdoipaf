using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_MazeCell : MonoBehaviour
{
    Button choose;
    Image image;
    public enum CellType
    {
        None,
        Blocked,
        Goal,
        Path,
        Player
    }
    public CellType Type;
    public Color[] colors = new Color[5]
    {
        new Color(1, 1, 1),
        new Color(.3f,.3f,.3f),
        new Color(1f,.5f,0),
        new Color(.5f,.5f, 1),
        new Color(0, 0, 1)
    };
    void Start()
    {
        choose = GetComponent<Button>();
        image = GetComponent<Image>();
        choose.onClick.AddListener(() =>
        {
            if (Type == CellType.Blocked)
                return;
            if (Type == CellType.None || Type == CellType.Goal)
                UIManager.Instance.GetCurrentPopup<UI_Maze>().AddPath(this);
            else if (Type == CellType.Path)
                UIManager.Instance.GetCurrentPopup<UI_Maze>().RemovePath(this);

        });
    }
    public void SetColor(CellType type)
    {
        image.color = colors[(int)type];
    }

}
