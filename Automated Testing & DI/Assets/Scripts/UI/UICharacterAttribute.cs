using UnityEngine;
using TMPro;

public class UICharacterAttribute : MonoBehaviour
{
    [SerializeField] string attributeName = string.Empty;
    [SerializeField] string value = string.Empty;
    [SerializeField, Multiline] string description = string.Empty;

    [SerializeField] TextMeshProUGUI nameField = null;
    [SerializeField] TextMeshProUGUI valueField = null;

    private void OnValidate()
    {
        nameField.text = attributeName;
        valueField.text = value;
    }
}
