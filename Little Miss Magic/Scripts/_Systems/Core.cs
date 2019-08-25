using UnityEngine;
using UnityEngine.SceneManagement;

public class Core : MonoBehaviour
{
    #region Singleton
    private static Core _instance;
    public static Core Instance
    {
        get
        {
            if (_instance == null)
                _instance = new GameObject("Core").AddComponent<Core>();

            return _instance;
        }
    }

    void MakeSingleton()
    {
        if (_instance == null)
        {
            _instance = this;
        }
        else if (_instance != this)
        {
            Debug.Log("Multiple Cores in scene. Extras destroyed.");
            Destroy(gameObject);
        }

        DontDestroyOnLoad(gameObject);
    }
    #endregion

    [Header("Systems")]
    [SerializeField] Database database;
    public Database DB { get { return database; } }

    [Header("References")]
    public Transform m_MainCamera;

    [Header("Load Scenes on Start")]
    public bool LoadScenesOnStart;
    public string[] ScenesLoadedOnStart;

    // Internal Systems

    void Awake()
    {
        MakeSingleton();

        if (LoadScenesOnStart)
        {
            foreach (string scene in ScenesLoadedOnStart)
            {
                SceneManager.LoadSceneAsync(scene, LoadSceneMode.Additive);
            }
        }
    }

    void Start()
    {
        DB.BuildDatabase();

        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.Confined;
    }

    void Update()
    {

    }

    void FixedUpdate()
    {

    }
}
