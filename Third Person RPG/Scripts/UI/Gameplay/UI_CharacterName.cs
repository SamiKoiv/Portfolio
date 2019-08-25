using UnityEngine;
using UnityEngine.UI;

public class UI_CharacterName : MonoBehaviour
{
    public CharacterProfile characterProfile;
    Text text;

    void Awake()
    {
        text = GetComponent<Text>();
        text.text = characterProfile.CharacterName;
    }
}
