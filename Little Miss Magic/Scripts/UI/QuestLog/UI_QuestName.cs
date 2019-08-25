using TMPro;
using UnityEngine;

public class UI_QuestName : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI textField;

    private void Awake()
    {
        textField = GetComponent<TextMeshProUGUI>();
    }

    private void OnEnable()
    {
        
    }

    private void OnDisable()
    {
        
    }

    void UpdateText(Quest quest)
    {
        if (quest != null)
            textField.text = quest.GetName();
        else
            textField.text = string.Empty;
    }
}
