using TMPro;
using UnityEngine;

public class UI_QuestObjectives : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI textField;

    private void OnEnable()
    {
        
    }

    private void OnDisable()
    {
        
    }

    void UpdateText(Quest quest)
    {
        if (quest != null)
        {
            textField.text = quest.GetObjective();
        }
        else
        {
            textField.text = string.Empty;
        }
    }
}
