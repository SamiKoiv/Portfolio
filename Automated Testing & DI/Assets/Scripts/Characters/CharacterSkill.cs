using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class CharacterSkill : MonoBehaviour
{
    [SerializeField] string skillName = string.Empty;
    [SerializeField] Color color = Color.white;
    [SerializeField, Multiline] string description = string.Empty;

    [SerializeField] TextMeshProUGUI nameField = null;
    [SerializeField] Image buttonImage = null;
    [SerializeField] Image descriptionImage = null;
    [SerializeField] TextMeshProUGUI descriptionField = null;
    [SerializeField] GameObject descriptionWindow = null;

    static CharacterSkill currentSkill;

    private void OnValidate()
    {
        nameField.text = skillName;
        buttonImage.color = color;
        descriptionImage.color = color;
        descriptionField.text = description;
    }

    public void ToggleDescription()
    {
        if (currentSkill == this)
        {
            CloseDescription();
            return;
        }

        if (currentSkill != null)
            currentSkill.CloseDescription();

        descriptionWindow.SetActive(true);
        currentSkill = this;
    }

    void CloseDescription()
    {
        descriptionWindow.SetActive(false);
        currentSkill = null;
    }
}
