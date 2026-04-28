using TMPro;
using UnityEngine;

public class UI_Inventory_Item : UI_Base
{
    string _name;
    enum GameObjects
    {
        ItemIcon,
        ItemName,
    }

    public override void Init()
    {
        Bind<GameObject>(typeof(GameObjects));
        GetObject((int)GameObjects.ItemName).GetComponent<TextMeshProUGUI>().text = _name;

        GetObject((int)GameObjects.ItemIcon).BindEvent((eventData) => { Debug.Log($"Click Inventory Item Icon {_name}"); }, Define.UIEvent.Click);
    }

    public void SetInfo(string name)
    {
        _name = name;
    }
}
