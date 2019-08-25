using TMPro;
using UnityEngine;

public class EventBool_SwitchText : MonoBehaviour
{
    [SerializeField] EventBool boolEvent;
    [SerializeField] string textOnTrue;
    [SerializeField] string textOnFalse;

    TextMeshProUGUI textField;

    private void Awake()
    {
        textField = GetComponent<TextMeshProUGUI>();
    }

    private void OnEnable()
    {
        boolEvent.OnChange += SetText;
    }

    private void OnDisable()
    {
        boolEvent.OnChange -= SetText;
    }

    void SetText(bool state)
    {
        switch (state)
        {
            case true:
                textField.text = textOnTrue;
                break;

            case false:
                textField.text = textOnFalse;
                break;
        }
    }
}
