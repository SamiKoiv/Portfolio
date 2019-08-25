using Ink.Runtime;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DialogueSystem : ManagedBehaviour_Update
{
    static DialogueSystem instance;
    public static DialogueSystem Instance
    {
        get { return instance; }
    }

    #region Variables

    // Dialogue is currently activated from UI_Juicer once it has opened the window.
    bool AutomaticActivate = false;

    [Header("Object References")]

    [SerializeField] GameObject dialogue;
    [SerializeField] GameObject namePanel;
    [SerializeField] GameObject waitingIndicator;
    [SerializeField] TextMeshProUGUI nameField;
    [SerializeField] TextMeshProUGUI dialogueField;
    [SerializeField] Transform choiceParent;
    [SerializeField] GameObject choicePrefab;

    [Header("Variable References")]
    [SerializeField] EventBool dialogueOpen;
    [SerializeField] Float_ReadOnly textSpeed;
    [SerializeField] String_Public playerName;

    bool isActive;
    Story firstStory;
    Story currentStory;
    Story nextStory;
    CharacterTag NPC;

    bool isPrinting;
    string currentPhrase;
    char[] currentCharacters;
    string builtText;

    int currentCharIndex;
    float buildTimer;

    DialogueChoice[] choices = new DialogueChoice[0];

    Quest loadedQuest;

    bool checkingTopics;

    bool nextStoryPending;
    bool returnPending;

    class QuestContainer
    {
        public Quest ContainedQuest;
    }

    #endregion

    #region Public Functions

    public void StartDialogue(Story story)
    {
        InputSystem.StartDialogue();

        firstStory = story;
        currentStory = firstStory;

        CheckQuests();

        dialogueField.text = string.Empty;
        nameField.text = string.Empty;

        namePanel.gameObject.SetActive(false);

        dialogueOpen.Value = true;

        if (AutomaticActivate)
        {
            isActive = true;
            NextLine();
        }
    }

    public void StartDialogue(Story story, CharacterTag character)
    {
        NPC = character;

        StartDialogue(story);
    }

    public void ManualActivate()
    {
        isActive = true;
        NextLine();
    }

    public void Close()
    {
        InputSystem.EndDialogue();

        isActive = false;
        firstStory = null;
        currentStory = null;
        NPC = null;
        SetQuest(null);

        dialogueOpen.Value = false;

        namePanel.SetActive(false);
        nameField.text = string.Empty;
        dialogueField.text = string.Empty;
    }

    public void SetQuest(Quest quest)
    {
        QuestContainer container = new QuestContainer();
        container.ContainedQuest = quest;
        loadedQuest = quest;
    }

    #endregion

    #region Monobehaviour

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
        {
            Destroy(gameObject);
            Debug.Log("Multiple DialogueSystems in Scene. Destroying duplicates.");
        }

        isActive = false;
    }

    private void OnEnable()
    {
        Subscribe_Update();
        InputSystem.OnDialogueContinue += Click;
    }

    private void OnDisable()
    {
        Unsubscribe_Update();
        InputSystem.OnDialogueContinue -= Click;
    }

    private void Start()
    {
        dialogueOpen.Value = false;
    }

    public override void M_Update()
    {
        if (!isActive)
            return;

        BuildText();
    }

    #endregion

    #region Story Progress & Choices

    void Click()
    {
        if (!isActive)
            return;

        if (isPrinting)
            RevealText();
        else
            NextLine();
    }

    void NextLine()
    {
        if (currentStory.canContinue)
        {
            ClearChoices();

            currentPhrase = ParseContent(currentStory.Continue());
            CheckTags();
            PrintStart();
            CallChoices();
        }
        else
        {
            if (currentStory.currentChoices.Count <= 0)
            {
                if (nextStoryPending)
                {
                    ProceedToNextStory();
                }
                else
                {
                    Debug.Log("Closing Dialogue.");
                    Close();
                    return;
                }
            }
            else
            {
                return;
            }
        }
    }

    void ProceedToNextStory()
    {
        Debug.Log("Moving to next Story");
        currentStory = nextStory;
        nextStoryPending = false;
        NextLine();
    }

    #endregion

    #region Choices

    void CallChoices()
    {
        if (currentStory.currentChoices.Count <= 0)
            return;

        int activeQuestsCount = 0;
        List<Quest> activeQuests = new List<Quest>();
        int openQuestsCount = 0;
        List<Quest> openQuests = new List<Quest>();

        if (checkingTopics)
        {

            if (QuestSystem.QuestNPC(NPC, out activeQuests))
            {
                activeQuestsCount = activeQuests.Count;
            }

            openQuests = Core.Instance.DB.QuestDB.GetOpenQuests(NPC);
            openQuestsCount = openQuests.Count;

            checkingTopics = false;
        }

        int totalCount = currentStory.currentChoices.Count + activeQuestsCount + openQuestsCount;
        choices = new DialogueChoice[totalCount];

        int elementIndex = 0;

        int i = 0;
        while (i < activeQuestsCount)
        {
            DialogueChoice choice = Instantiate(choicePrefab, choiceParent).GetComponent<DialogueChoice>();
            choice.SetQuestTopic(NPC, activeQuests[i]);
            choices[elementIndex] = choice;
            i++;
            elementIndex++;
        }

        i = 0;
        while (i < openQuestsCount)
        {
            DialogueChoice choice = Instantiate(choicePrefab, choiceParent).GetComponent<DialogueChoice>();
            choice.SetQuestTopic(NPC, openQuests[i]);
            choices[elementIndex] = choice;
            i++;
            elementIndex++;
        }

        i = 0;
        while (i < currentStory.currentChoices.Count)
        {
            DialogueChoice choice = Instantiate(choicePrefab, choiceParent).GetComponent<DialogueChoice>();
            choice.SetChoice(NPC, currentStory.currentChoices[i]);
            choices[elementIndex] = choice;
            i++;
            elementIndex++;
        }
    }

    public void ClearChoices()
    {
        if (choices.Length > 0)
        {
            foreach (DialogueChoice choice in choices)
                Destroy(choice.gameObject);

            choices = new DialogueChoice[0];
        }
    }

    public void SetChoice(int choiceIndex)
    {
        currentStory.ChooseChoiceIndex(choiceIndex);
        NextLine();
    }

    public void SetChoice(Story story)
    {
        SetNextStory(story);
        SkipToNextStory();
        NextLine();
    }

    #endregion

    #region Check for Tags & Quests

    void CheckTags()
    {
        if (currentStory.currentTags.Count <= 0)
            return;

        List<string> tags = currentStory.currentTags;

        foreach(string tag in tags)
        {
            Debug.Log(tag);
        }

        if (tags.Contains("Topics"))
        {
            checkingTopics = true;
        }

        if (tags.Contains("RandomQuest"))
        {
            SetQuest(QuestSystem.Instance.GetRandomQuest(NPC));
            SetNextStory(loadedQuest.GetStartDialogue(NPC));
        }

        if (tags.Contains("StartQuest"))
        {
            QuestSystem.Instance.StartQuest(loadedQuest);
            CheckQuests();
        }

        if (tags.Contains("RejectQuest"))
        {
            SetQuest(null);
        }

        if (tags.Contains("Return"))
        {
            firstStory.ChoosePathString("Main");
            SetNextStory(firstStory);
            CheckQuests();
        }

        if (loadedQuest != null)
            loadedQuest.ParseTags(tags);
    }

    void CheckQuests()
    {
        if (QuestSystem.QuestNPC(NPC))
            firstStory.variablesState["onQuest"] = true;
        else
            firstStory.variablesState["onQuest"] = false;
    }

    #endregion

    #region Switch Topic

    public void SetNextStory(Story story)
    {
        nextStory = story;
        nextStoryPending = true;
    }

    void SkipToNextStory()
    {
        currentStory = nextStory;
        nextStoryPending = false;
    }

    #endregion

    #region Content Parsing & Derived Ops

    string ParseContent(string rawContent)
    {
        string speakerID = string.Empty;
        string content = string.Empty;
        if (!TrySplitContentBySearchString(rawContent, ":", ref speakerID, ref content))
            return rawContent;
        ChangeSpeaker(speakerID);
        return content;
    }

    bool TrySplitContentBySearchString(string rawContent, string searchString, ref string left, ref string right)
    {
        int firstSpecialCharacterIndex = rawContent.IndexOf(searchString);
        if (firstSpecialCharacterIndex == -1) return false;

        left = rawContent.Substring(0, firstSpecialCharacterIndex).Trim();
        right = rawContent.Substring(firstSpecialCharacterIndex + searchString.Length, rawContent.Length - firstSpecialCharacterIndex - searchString.Length).Trim();
        return true;
    }

    void ChangeSpeaker(string SpeakerID)
    {
        namePanel.SetActive(true);

        if (SpeakerID.ToUpper() == "PLAYER")
        {
            SpeakerID = playerName.Value;
        }
        else if (NPC != null && SpeakerID.ToUpper() == "NPC")
        {
            SpeakerID = NPC.GetName();
        }
        else if (SpeakerID.ToUpper() == "CLEAR")
        {
            SpeakerID = string.Empty;
            namePanel.SetActive(false);
        }

        nameField.text = SpeakerID;
    }

    #endregion

    #region Printing

    void PrintStart()
    {
        isPrinting = true;
        currentCharacters = currentPhrase.ToCharArray();

        dialogueField.text = string.Empty;
        waitingIndicator.SetActive(false);
    }

    void BuildText()
    {
        if (!isPrinting)
            return;

        buildTimer += Time.deltaTime * textSpeed.Value;

        while (buildTimer > 1)
        {
            if (currentCharIndex < currentCharacters.Length)
            {
                builtText += currentCharacters[currentCharIndex];
                currentCharIndex++;
                buildTimer -= 1;
                dialogueField.text = builtText;
            }
            else
            {
                PrintComplete();
            }
        }

    }

    void RevealText()
    {
        dialogueField.text = currentPhrase;
        PrintComplete();
    }

    void PrintComplete()
    {
        isPrinting = false;
        builtText = string.Empty;
        buildTimer = 0;
        currentCharIndex = 0;
        waitingIndicator.SetActive(true);
    }

    #endregion
}
