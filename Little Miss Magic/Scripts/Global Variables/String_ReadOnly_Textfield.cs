using TMPro;
using UnityEngine;

public class String_ReadOnly_Textfield : MonoBehaviour
{
    [SerializeField] String_ReadOnly globalText;

    private void Awake()
    {
        GetComponent<TextMeshProUGUI>().text = globalText.Value;
    }
}
