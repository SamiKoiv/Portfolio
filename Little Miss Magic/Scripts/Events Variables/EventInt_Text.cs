using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class EventInt_Text : MonoBehaviour
{
    [SerializeField] EventInt intEvent;
    TextMeshProUGUI Textfield;

    void Awake()
    {
        Textfield = GetComponent<TextMeshProUGUI>();
    }

    private void OnEnable()
    {
        intEvent.OnChange += UpdateText;
    }

    private void OnDisable()
    {
        intEvent.OnChange -= UpdateText;
    }

    void UpdateText(int value)
    {
        Textfield.text = string.Empty + value;
    }
}
