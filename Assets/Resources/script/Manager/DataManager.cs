using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Xml;
using System.Xml.Serialization;
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
            false,
            false,
            false,
        },
        new List<bool>
        {
            false,
            false,
        },
        new List<bool>
        {
            false,
            false,
        },
        new List<bool>
        {
            false,
        },
    };
    public List<int> Ranking = new List<int>();

    public void Add(int score)
    {
        Ranking.Add(score);

        XmlSerializer serializer = new XmlSerializer(typeof(List<int>));
#if UNITY_EDITOR
        using (StreamWriter streamWriter = new StreamWriter("Assets/Resources/Data/ranking.xml"))
        {
            serializer.Serialize(streamWriter, Ranking);
        }
        return;
#endif
        using (StreamWriter streamWriter = new StreamWriter(Application.streamingAssetsPath + "Data/ranking.xml"))
        {
            serializer.Serialize(streamWriter, Ranking);
        }
    }
    bool isInitialized = false;
    public void Init()
    {
        if (isInitialized) return;
        isInitialized = true;
        XmlSerializer serializer = new XmlSerializer(typeof(List<int>));
        #if UNITY_EDITOR
            try
            {
                using (StreamReader reader = new StreamReader("Assets/Resources/Data/ranking.xml"))
                {
                    Ranking = (List<int>)serializer.Deserialize(reader);
                }
            }
            catch(Exception e)
            {
                Ranking = new List<int>();
            }
            return;
        #endif
        Debug.Log(Application.streamingAssetsPath);
        try
        {
            using (StreamReader reader = new StreamReader(Application.streamingAssetsPath + "Data/ranking.xml"))
            {
                Ranking = (List<int>)serializer.Deserialize(reader);
            }
        }
        catch (Exception e)
        {
            Ranking = new List<int>();
        }
    }
    private DataManager()
    {

    }
}
