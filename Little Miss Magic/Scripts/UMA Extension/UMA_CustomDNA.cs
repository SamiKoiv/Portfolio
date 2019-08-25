using UMA;
using UMA.CharacterSystem;
using UnityEngine;

public class UMA_CustomDNA : MonoBehaviour
{
    DynamicCharacterAvatar characterAvatar;
    public UMAPredefinedDNA DNA;

    private void Start()
    {
        characterAvatar = GetComponent<DynamicCharacterAvatar>();
        characterAvatar.predefinedDNA.PreloadValues = DNA.PreloadValues;
    }
}
