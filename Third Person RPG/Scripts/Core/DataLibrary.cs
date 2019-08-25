using UnityEngine;

public class DataLibrary : MonoBehaviour
{

    public static DataLibrary instance = null;

    private void Awake()
    {
        EstablishSingleton();
    }

    void EstablishSingleton()
    {
        if (instance == null)
            instance = this;

        else if (instance != this)
            Destroy(gameObject);
    }


    public InputLibrary inputLibrary;
    public Color_Library colorLibrary;
    public Proto_AssetLibrary protoAssetLibrary;

}
