using System;
using System.Collections.Generic;
using UnityEngine;

public class DataManager
{
    public Dictionary<int, Data.Stat> StatDict { get; protected set; } = new();
    public void Init()
    {
        StatDict = LoadJson<Data.StatData, int, Data.Stat>("StatData").MakeDict();
    }

    Loader LoadJson<Loader, Key, Value>(string path) where Loader : ILoader<Key, Value>
    {
        TextAsset textAsset = Managers.Resource.Load<TextAsset>($"Datas/{path}");
        return JsonUtility.FromJson<Loader>(textAsset.text);
    }

    public void Clear() { }
}