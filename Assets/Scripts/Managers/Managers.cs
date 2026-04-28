using UnityEngine;

public class Managers : MonoBehaviour
{
    static Managers _instance;
    static Managers Instance { get { Init(); return _instance; } }

    #region Contents
    GameManager _gameManager = new();
    public static GameManager Game { get { return Instance._gameManager; } }
    #endregion

    #region  Core
    DataManager _dataManager = new();
    InputManager _inputManager = new();
    PoolManager _poolManager = new();
    ResourceManager _resourceManager = new();
    SceneManagerEx _sceneManager = new();
    SoundManager _soundManager = new();
    UIManager _uiManager = new();
    public static DataManager Data { get { return Instance._dataManager; } }
    public static InputManager Input { get { return Instance._inputManager; } }
    public static PoolManager Pool { get { return Instance._poolManager; } }
    public static ResourceManager Resource { get { return Instance._resourceManager; } }
    public static SceneManagerEx Scene { get { return Instance._sceneManager; } }
    public static SoundManager Sound { get { return Instance._soundManager; } }
    public static UIManager UI { get { return Instance._uiManager; } }
    #endregion

    void Start()
    {
        Init();
    }

    void Update()
    {
        _inputManager.OnUpdate();
    }

    static void Init()
    {
        if (_instance == null)
        {
            GameObject go = GameObject.Find("@Manager");
            if (go == null)
            {
                go = new GameObject { name = "@Manager" };
                go.AddComponent<Managers>();
            }
            DontDestroyOnLoad(go);
            _instance = go.GetComponent<Managers>();

            _instance._dataManager.Init();
            _instance._poolManager.Init();
            _instance._soundManager.Init();
        }
    }

    public static void Clear()
    {
        Input.Clear();
        Sound.Clear();
        Scene.Clear();
        UI.Clear();

        Pool.Clear();
    }
}
