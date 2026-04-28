using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Diagnostics;

public class UIManager
{
    int _order = 0;

    Stack<UI_Popup> _popupStack = new();
    UI_Scene _scene = null;

    public GameObject _root
    {
        get
        {
            GameObject root = GameObject.Find("@UI_Root");
            if (root == null)
            {
                root = new GameObject { name = "@UI_Root" };
                root.AddComponent<Managers>();
            }
            return root;
        }
    }

    public void ShowCanvas(GameObject go, bool sort = true)
    {
        Canvas canvas = go.GetorAddComponent<Canvas>();

        canvas.renderMode = RenderMode.ScreenSpaceOverlay;
        canvas.overrideSorting = true;

        if (sort)
        {
            canvas.sortingOrder = _order;
            _order++;
        }
        else
            canvas.sortingOrder = 0;
    }

    public T CreateWorldSpaceUI<T>(Transform parent = null, string name = null) where T : UI_Base
    {
        if (string.IsNullOrEmpty(name))
            name = typeof(T).Name;
        GameObject go = Managers.Resource.Instantiate($"UIs/WorldSpace/{name}");

        if (parent != null)
            go.transform.SetParent(parent);

        Canvas canvas = go.GetComponent<Canvas>();
        canvas.renderMode = RenderMode.WorldSpace;
        canvas.worldCamera = Camera.main;

        return go.GetOrAddComponent<T>();
    }

    public T CreateSubItem<T>(Transform parent = null, string name = null) where T : UI_Base
    {
        if (string.IsNullOrEmpty(name))
            name = typeof(T).Name;
        GameObject go = Managers.Resource.Instantiate($"UIs/SubItems/{name}");

        if (parent != null)
            go.transform.SetParent(parent);

        return go.GetOrAddComponent<T>();
    }

    public T ShowSceneUI<T>(string name = null) where T : UI_Scene
    {
        if (string.IsNullOrEmpty(name))
            name = typeof(T).Name;

        GameObject go = Managers.Resource.Instantiate($"UIs/Scenes/{name}");
        T scene = go.GetorAddComponent<T>();

        scene.transform.SetParent(_root.transform);

        _scene = scene;
        return scene;
    }

    public T ShowPopUpUI<T>(string name = null) where T : UI_Popup
    {
        if (string.IsNullOrEmpty(name))
            name = typeof(T).Name;

        GameObject go = Managers.Resource.Instantiate($"UIs/Popups/{name}");
        T popup = go.GetorAddComponent<T>();

        popup.transform.SetParent(_root.transform);

        _popupStack.Push(popup);
        return popup;
    }

    public void ClosePopUpUI(UI_Popup popup)
    {
        if (_popupStack.Count == 0)
            return;

        if (_popupStack.Peek() != popup)
        {
            Debug.Log($"Close Popup Failed: name unmatched - {popup.name}");
            return;
        }

        ClosePopUpUI();
    }

    public void ClosePopUpUI()
    {
        if (_popupStack.Count == 0)
            return;

        UI_Popup popup = _popupStack.Pop();
        Managers.Resource.Destory(popup.gameObject);
        popup = null;
        _order--;
    }

    public void CloseAllPopUpUI()
    {
        while (_popupStack.Count > 0)
            ClosePopUpUI();
    }

    public void Clear()
    {
        CloseAllPopUpUI();
        _scene = null;
    }
}
