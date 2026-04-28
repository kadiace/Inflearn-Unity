using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class GameScene : BaseScene
{
    // Coroutine co;
    protected override void Init()
    {
        base.Init();

        SceneType = Define.Scene.Game;
        Managers.UI.ShowSceneUI<UI_Inventory>();
        gameObject.GetOrAddComponent<CursorController>();

        // Temp
        GameObject player = Managers.Game.Spawn(Define.WorldObject.Player, "UnityChan");
        Camera.main.GetOrAddComponent<CameraController>().SetPlayer(player);

        // Managers.Game.Spawn(Define.WorldObject.Monster, "Knight");
        GameObject go = new() { name = "SpawningPool" };
        SpawningPool pool = go.GetorAddComponent<SpawningPool>();
        pool.SetKeepMonsterCount(5);
        // for (int i = 0; i < 5; i++)
        // {
        //     Managers.Resource.Instantiate("UnityChan");
        // }

        // co = StartCoroutine(nameof(ExplodeAfterSeconds), 4.0f);
        // StartCoroutine(nameof(CoStopExplode), 2.0f);

        // Dictionary<int, Stat> statDict = Managers.Data.StatDict;
    }

    // IEnumerator CoStopExplode(float seconds)
    // {
    //     Debug.Log("Stop Explode Start");
    //     yield return new WaitForSeconds(seconds);
    //     Debug.Log("Stop Explode");
    //     if (co != null)
    //     {
    //         StopCoroutine(co);
    //         co = null;
    //     }
    // }

    // IEnumerator ExplodeAfterSeconds(float seconds)
    // {
    //     Debug.Log("Explode Count Start");
    //     yield return new WaitForSeconds(seconds);
    //     Debug.Log("Explode!");
    //     co = null;
    // }

    public override void Clear()
    {

    }
}
