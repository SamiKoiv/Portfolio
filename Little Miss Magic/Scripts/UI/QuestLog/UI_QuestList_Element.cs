using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UI_QuestList_Element : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI textfield;

    Quest quest;

    public void SetQuest(Quest quest)
    {
        this.quest = quest;
        textfield.text = quest.GetName();
    }

    public Quest GetQuest()
    {
        return quest;
    }

    public void ChooseQuest()
    {
        QuestSystem.Instance.SetQuestScreen(quest);
    }
}
