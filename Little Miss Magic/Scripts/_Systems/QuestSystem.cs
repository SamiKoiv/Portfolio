using Ink.Runtime;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class QuestSystem : MonoBehaviour
{
    static QuestSystem instance;
    public static QuestSystem Instance
    {
        get
        {
            if (instance == null)
                instance = new GameObject("QuestSystem").AddComponent<QuestSystem>();

            return instance;
        }
    }

    Database DB;
    static List<Quest> QuestLog = new List<Quest>();
    static int QuestID = 1;

    [SerializeField] EventBool QuestLogOpen;
    [SerializeField] TextMeshProUGUI questNameField;
    [SerializeField] TextMeshProUGUI questDescriptionField;
    [SerializeField] TextMeshProUGUI questObjectiveField;

    [SerializeField] GameObject listElementPrefab;
    [SerializeField] Transform listContainer;

    public CharacterTag testCharacter;

    List<UI_QuestList_Element> elements = new List<UI_QuestList_Element>();

    private void Awake()
    {
        if (instance == null)
            instance = this;
        if (instance != this)
        {
            Debug.Log("Destroying QuestSystem duplicates from the scene.");
            Destroy(this);
        }

        DB = Core.Instance.DB;
    }

    private void OnEnable()
    {
        QuestLogOpen.OnChange += ToggleQuestLog;
    }

    private void OnDisable()
    {
        QuestLogOpen.OnChange -= ToggleQuestLog;
    }

    void ToggleQuestLog(bool isOpen)
    {
        if (isOpen)
            OpenQuestLog();
        else
            CloseQuestLog();
    }

    void ResetQuestScreen()
    {
        questNameField.text = string.Empty;
        questDescriptionField.text = string.Empty;
        questObjectiveField.text = string.Empty;
    }

    [ContextMenu("Open QuestLog")]
    public void OpenQuestLog()
    {
        ResetQuestScreen();
    }

    [ContextMenu("Close QuestLog")]
    public void CloseQuestLog()
    {
        // Do nothing as of now
    }

    public void SetQuestScreen(Quest quest)
    {
        questNameField.text = quest.GetName();
        questDescriptionField.text = quest.GetDescription();
        questObjectiveField.text = quest.GetObjective();
    }

    public Quest GetRandomQuest(CharacterTag characterTag)
    {
        // Add quest type randomization

        Quest_Collection quest = new Quest_Collection(characterTag);

        return quest;
    }

    public void StartQuest(Quest quest)
    {
        QuestLog.Add(quest);
        UI_QuestList_Element element = Instantiate(listElementPrefab, listContainer).GetComponent<UI_QuestList_Element>();
        element.SetQuest(quest);
        elements.Add(element);
        quest.Start();
    }

    public void QuestEnded(Quest quest)
    {
        foreach (UI_QuestList_Element element in elements)
        {
            if (element.GetQuest() == quest)
            {
                Destroy(element.gameObject);
            }
        }

        QuestLog.Remove(quest);
    }

    public static bool QuestNPC(CharacterTag character)
    {
        foreach (Quest quest in QuestLog)
        {
            foreach (CharacterTag tag in quest.GetInvolvedCharacters())
            {
                if (tag == character)
                    return true;
            }
        }

        return false;
    }

    public static bool QuestNPC(CharacterTag character, out List<Quest> quests)
    {
        List<Quest> questList = new List<Quest>();
        bool onQuest = false;

        foreach (Quest quest in QuestLog)
        {
            foreach (CharacterTag tag in quest.GetInvolvedCharacters())
            {
                if (tag == character)
                {
                    questList.Add(quest);
                    onQuest = true;
                }
            }
        }

        quests = questList;
        return onQuest;
    }

    public static bool ContainsQuest(int id)
    {
        foreach (Quest q in QuestLog)
        {
            if (q.ID == id)
            {
                return true;
            }
        }
        return false;
    }

    public static bool ContainsQuest(int id, out Quest quest)
    {
        foreach (Quest q in QuestLog)
        {
            if (q.ID == id)
            {
                quest = q;
                return true;
            }
        }

        quest = null;
        return false;
    }

    public static int GetNewID()
    {
        int id = QuestID;
        QuestID++;
        return id;
    }

    [ContextMenu("Start Mermaid Quest")]
    void TestMermaidQuest()
    {
        Quest quest = new Quests.CharacterArcs.Mermaid_1stChapter();
        StartQuest(quest);
        Story story = DB.StoryDB.MermaidStory;
        story.ChoosePathString("Interact");
        DialogueSystem.Instance.StartDialogue(story, DB.CharacterDB.Mermaid);
        Debug.Log(quest.GetName());
    }
}
