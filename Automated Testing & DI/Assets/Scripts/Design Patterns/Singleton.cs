using UnityEngine;

public class Singleton<T> : MonoBehaviour where T : Component
{
    static T m_instance;
    public static bool m_isQuitting;

    public static T Instance
    {
        get
        {
            if (m_instance == null)
            {
                m_instance = FindObjectOfType<T>();

                if (m_instance == null)
                {
                    GameObject obj = new GameObject(typeof(T).Name);
                    m_instance = obj.AddComponent<T>();
                }
            }

            return m_instance;
        }
    }

    public virtual void Awake()
    {
        if (m_instance == null)
        {
            m_instance = this as T;
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Debug.Log("Destroying duplicates of " + typeof(T).Name + " from the scene.");
            Destroy(gameObject);
        }
    }
}
