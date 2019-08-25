using Ink.Runtime;
using System.Collections.Generic;
using UnityEngine;

public abstract class Quest
{
    // Ink story that holds most of the quest structure and narrative logic.
    protected Story story;
    protected bool previouslyCompleted;

    // Quest is given by this character and they will evaluate quest by default.
    protected CharacterTag npc;

    CharacterTag[] involvedCharacters;

    protected int id;

    public int ID { get { return id; } }

    string state;

    protected void AssignID()
    {
        id = QuestSystem.GetNewID();
    }

    public void SetInvolvedCharacters(CharacterTag[] characters)
    {
        involvedCharacters = characters;
    }

    public CharacterTag[] GetInvolvedCharacters()
    {
        return involvedCharacters;
    }

    public bool IsPreviouslyCompleted()
    {
        return previouslyCompleted;
    }

    public string GetName()
    {
        return ReadPath("Quest.QuestName");
    }

    public string GetDescription()
    {
        return ReadPath("Quest.Description");
    }

    public string GetTopic()
    {
        return ReadPath("Quest.Topic");
    }

    public Story GetStartDialogue(CharacterTag npc)
    {
        story.variablesState["npc"] = npc.GetName();
        story.ChoosePathString("Quest.StartDialogue");
        return story;
    }

    public Story GetStateDialogue(CharacterTag npc)
    {
        story.variablesState["npc"] = npc.GetName();

        if (CompletionConditionsMet())
        {
            story.ChoosePathString("Quest.Complete");
            Complete();
            QuestSystem.Instance.QuestEnded(this);
            return story;
        }
        else if (FailConditionsMet())
        {
            story.ChoosePathString("Quest.Fail");
            Fail();
            return story;
        }
        else
        {
            story.ChoosePathString("Quest.State");
            return story;
        }
    }

    public delegate void ProgressEvent(string progressMessage);
    public event ProgressEvent OnProgress;

    public abstract void Start();

    // "Do something for X amount."
    public abstract string GetObjective();

    // Evaluate whether quest objectives have been met and quest can be completed.
    // Could later be split into Evaluate & End to allow quest monitoring in UI.

    public abstract void ParseTags(List<string> tags);

    // Here we check the requirements for giving the quest
    public abstract bool CanStart(CharacterTag character);

    public abstract bool CompletionConditionsMet();

    public abstract bool FailConditionsMet();

    public abstract void Complete();

    public abstract void Fail();

    string ReadPath(string path)
    {
        state = story.state.ToJson();
        story.ChoosePathString(path);
        string result = story.ContinueMaximally();
        story.state.LoadJson(state);
        return result;
    }
}
