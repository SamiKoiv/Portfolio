using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Ink.Runtime;

public class DialogueChoice : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI textField;
    [SerializeField] Button button;

    enum Mode { InkChoice, QuestTopic }
    Mode mode;

    Choice choice;
    Quest quest;

    CharacterTag npc;

    public void SetChoice(CharacterTag npc, Choice choice)
    {
        this.npc = npc;

        this.choice = choice;
        textField.text = choice.text;
        mode = Mode.InkChoice;
    }

    public void SetQuestTopic(CharacterTag npc, Quest quest)
    {
        this.npc = npc;

        this.quest = quest;
        textField.text = quest.GetTopic();
        mode = Mode.QuestTopic;
    }

    public void Choose()
    {
        switch (mode)
        {
            case Mode.InkChoice:
                DialogueSystem.Instance.SetChoice(choice.index);
                break;

            case Mode.QuestTopic:
                DialogueSystem.Instance.SetQuest(quest);
                DialogueSystem.Instance.SetChoice(quest.GetStateDialogue(npc));
                break;
        }
    }

    public Button GetButton()
    {
        return button;
    }
}
