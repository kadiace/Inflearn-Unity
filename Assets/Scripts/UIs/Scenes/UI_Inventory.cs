using Unity.VisualScripting;
using UnityEngine;

public class UI_Inventory : UI_Scene
{
    enum GameObjects
    {
        GridPanel,
    }

    public override void Init()
    {
        base.Init();

        Bind<GameObject>(typeof(GameObjects));

        GameObject gridPanel = GetObject((int)GameObjects.GridPanel);
        foreach (Transform child in gridPanel.transform)
            Managers.Resource.Destory(child.gameObject);

        // Get Item Info, and Set
        for (int i = 0; i < 10; i++)
        {
            GameObject item = Managers.UI.CreateSubItem<UI_Inventory_Item>(parent: gridPanel.transform).gameObject;
            UI_Inventory_Item invenItem = item.GetorAddComponent<UI_Inventory_Item>();
            invenItem.SetInfo($"Master Sword {i}");
        }
    }
}
