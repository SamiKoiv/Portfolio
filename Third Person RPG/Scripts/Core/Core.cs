using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Core : MonoBehaviour
{
    public static Core instance = null;

    public Camera MainCamera;
    public Camera_Controller CameraController;
    public UI_Events UIEvents;
    public QuestEvents QuestEvents = new QuestEvents();

    public UI_LoadingScreen loadingScreen;

    Transform player;

    SceneManager _sceneManager;
    QuestManager _questManager;
    bool _cursorVisible;

    public SceneManager SceneManager
    {
        get
        {
            return _sceneManager;
        }
    }
    public QuestManager QuestManager
    {
        get
        {
            return _questManager;
        }
    }
    public bool CursorVisible
    {
        get
        {
            return _cursorVisible;
        }
        set
        {
            _cursorVisible = value;
            Cursor.visible = _cursorVisible;

            if (_cursorVisible)
            {
                Cursor.lockState = CursorLockMode.Confined;
            }
            else
            {
                Cursor.lockState = CursorLockMode.Locked;
            }
        }
    }

    void Awake()
    {
        EstablishSingleton();
        _sceneManager = new SceneManager();
        _questManager = new QuestManager(UIEvents);
    }

    void Start()
    {
        CursorVisible = false;

        ManagedInit();
    }

    void Update()
    {
        ManagedUpdate();
    }

    void EstablishSingleton()
    {
        if (instance == null)
            instance = this;

        else if (instance != this)
            Destroy(gameObject);

        DontDestroyOnLoad(gameObject);
    }

    #region ManagedObjects

    void ManagedInit()
    {
        loadingScreen.ManagedInit();
    }

    void ManagedUpdate()
    {
        loadingScreen.ManagedUpdate();
    }

    #endregion

    #region Player

    public void SetPlayer(Transform player)
    {
        this.player = player;
    }

    public Transform GetPlayer()
    {
        return player;
    }

    #endregion

}


