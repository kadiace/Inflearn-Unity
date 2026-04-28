using UnityEngine;
using UnityEngine.SceneManagement;

public class LoginScene : BaseScene
{
    protected override void Init()
    {
        base.Init();

        SceneType = Define.Scene.Login;

        // Temp
        GameObject[] gameObjects = new GameObject[10];
        for (int i = 0; i < 2; i++)
        {
            gameObjects[i] = Managers.Resource.Instantiate("UnityChan");
        }


        // foreach (GameObject go in gameObjects)
        // {
        //     Managers.Resource.Destory(go);
        // }
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
            Managers.Scene.LoadScene(Define.Scene.Game);
    }

    public override void Clear()
    {
        Debug.Log("Login Scene Clear");
    }
}
