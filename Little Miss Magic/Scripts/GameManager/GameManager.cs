using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    static GameManager instance;
    public static GameManager Instance
    {
        get
        {
            if (instance == null)
                Debug.Log("GameManager not found in the scene.");

            return instance;
        }
    }

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else if (instance != this)
        {
            Debug.Log("Destroying GameManager duplicates from the scene.");
            Destroy(gameObject);
        }
    }

    [SerializeField] String_Public _playerName;
    public string PlayerName { get { return _playerName.Value; } }
    
    public void SetPlayerName(string playerName)
    {
        _playerName.Value = playerName;
    }
}
