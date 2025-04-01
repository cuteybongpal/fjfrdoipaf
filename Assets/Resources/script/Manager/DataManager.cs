using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataManager
{
    private static DataManager instance = new DataManager();
    public static DataManager Instance {  get { return instance; } }

    public List<bool>[] IsStageTreasureFind = new List<bool>[5]
    {
        new List<bool>
        {
            false,
            false,
            false,
            false,
        },
        new List<bool>
        {
            false,
        },
        new List<bool>
        {
            false,
        },
        new List<bool>
        {
            false,
        },
        new List<bool>
        {
            false,
        },
    }; 
}
