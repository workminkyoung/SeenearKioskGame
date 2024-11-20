using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System;
using System.Linq;

public class DataLoader : MonoBehaviour
{
    private void Awake()
    {
        LoadMcData();
        LoadMcTutoData();
    }

    void LoadMcData()
    {
        string mcPath = Path.Combine(Application.streamingAssetsPath, "McDataList.json");
        if (!File.Exists(mcPath))
            return;

        string mcData = File.ReadAllText(mcPath);
        McdonaldProperties._McMenu = JsonUtility.FromJson<McMenu>(mcData);
        for (int i = 0; i < McdonaldProperties._McMenu.items.Length; i++)
        {
            McdonaldProperties._McListTitle.Add(McdonaldProperties._McMenu.items[i].title);
            //Debug.Log(McdonaldProperties._McMenu.items[i].title);
        }
    }
    void LoadMcTutoData()
    {
        string mcPath = Path.Combine(Application.streamingAssetsPath, "McTutoData.json");
        if (!File.Exists(mcPath))
            return;

        string mcData = File.ReadAllText(mcPath);
        McdonaldProperties._McTuto = JsonUtility.FromJson<McTutoList>(mcData);
        for (int i = 0; i < McdonaldProperties._McTuto.items.Length; i++)
        {
            McdonaldProperties._McTuto.items[i].posVec = StringToVector2(McdonaldProperties._McTuto.items[i].pos);
            McdonaldProperties._McTuto.items[i].sizeVec = StringToVector2(McdonaldProperties._McTuto.items[i].size);
            //Debug.Log(McdonaldProperties._McMenu.items[i].title);
        }
    }
    public static McTutoList LoadAndGetMcTutoData(string json)
    {
        string mcPath = Path.Combine(Application.streamingAssetsPath, json+".json");
        if (!File.Exists(mcPath))
            return null;

        string mcData = File.ReadAllText(mcPath);
        McTutoList tuto = JsonUtility.FromJson<McTutoList>(mcData);
        for (int i = 0; i < tuto.items.Length; i++)
        {
            tuto.items[i].posVec = StringToVector2(tuto.items[i].pos);
            tuto.items[i].sizeVec = StringToVector2(tuto.items[i].size);
            tuto.items[i].lposVec = StringToVector2(tuto.items[i].lpos);
        }

        return tuto;
    }

    public static Vector2 StringToVector2(string text)
    {
        if (text == string.Empty || text.Length <= 0)
            return new Vector2(0, 0);
        
        char splitRef = ',';
        string[] splitted = text.Split(splitRef);
        float[] vecList = splitted.Select(n => float.Parse(n.Trim())).ToArray();
        return new Vector2(vecList[0], vecList[1]);
    }
}
