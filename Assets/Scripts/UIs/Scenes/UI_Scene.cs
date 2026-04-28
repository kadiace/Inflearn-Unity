using UnityEngine;

public class UI_Scene : UI_Base
{
    public override void Init()
    {
        Managers.UI.ShowCanvas(gameObject, false);
    }
}
