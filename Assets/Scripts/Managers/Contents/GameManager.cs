using System;
using System.Collections.Generic;
using UnityEngine;

public class GameManager
{
    GameObject _player;
    public GameObject GetPlayer() { return _player; }
    // Dictionary<int, GameObject> _players = new();
    readonly HashSet<GameObject> _monsters = new();

    public Action<int> OnSpawnEvent;

    public GameObject Spawn(Define.WorldObject type, string path, Transform parent = null)
    {
        GameObject go = Managers.Resource.Instantiate(path, parent);

        switch (type)
        {
            case Define.WorldObject.Monster:
                _monsters.Add(go);
                OnSpawnEvent?.Invoke(1);
                break;
            case Define.WorldObject.Player:
                _player = go;
                break;
        }

        return go;
    }

    public Define.WorldObject GetWorldObjectType(GameObject go)
    {
        BaseController bc = go.GetComponent<BaseController>();

        if (bc == null)
            return Define.WorldObject.Unknown;

        return bc.WorldObjectType;
    }

    public void Despawn(GameObject go)
    {
        Define.WorldObject type = GetWorldObjectType(go);

        switch (type)
        {
            case Define.WorldObject.Player:
                if (_player == go)
                    _player = null;
                break;
            case Define.WorldObject.Monster:
                if (_monsters.Contains(go))
                {
                    _monsters.Remove(go);
                    OnSpawnEvent?.Invoke(-1);
                }
                break;
            case Define.WorldObject.Unknown:
                break;
        }

        Managers.Resource.Destory(go);
    }
}
