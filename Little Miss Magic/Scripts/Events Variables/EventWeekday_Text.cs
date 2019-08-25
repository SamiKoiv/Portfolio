using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class EventWeekday_Text : MonoBehaviour
{
    [SerializeField] EventWeekday weekday;

    TextMeshProUGUI textField;

    private void Awake()
    {
        textField = GetComponent<TextMeshProUGUI>();
    }

    private void OnEnable()
    {
        weekday.OnChange += GetDay;
    }

    private void OnDisable()
    {
        weekday.OnChange -= GetDay;
    }

    void GetDay(Weekday day)
    {
        textField.text = day.ToString();
    }
}
