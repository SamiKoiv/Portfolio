using Ink.Runtime;
using UnityEngine;
using System.Collections.Generic;

public class Quest_Collection : Quest
{
    Item requiredItem;
    int requiredAmount;

    Item rewardItem;
    int rewardQuantity;

    public Quest_Collection(CharacterTag npc)
    {
        this.npc = npc;

        Database DB = Core.Instance.DB;

        story = new Story(DB.QuestDB.CollectionQuest_Json.text);
        requiredItem = DB.ItemDB.Common.GetRandom();
        requiredAmount = 1;
        rewardItem = DB.ItemDB.Money;
        rewardQuantity = requiredItem.Price * 3;

        story.variablesState["player"] = GameManager.Instance.PlayerName;
        story.variablesState["npc"] = npc.GetName();
        story.variablesState["partitive"] = npc.Partitive();
        story.variablesState["genetive"] = npc.Genetive();
        story.variablesState["itemName"] = requiredItem.ItemName;
        story.variablesState["amount"] = requiredAmount;

        AssignID();

        SetInvolvedCharacters(new CharacterTag[] { npc });
    }

    public override bool CanStart(CharacterTag character)
    {
        return true;
    }

    public override void Start()
    {
        // No actions needed.
    }

    public override bool CompletionConditionsMet()
    {
        if (PlayerInventory.Instance.GetAmount(requiredItem) >= requiredAmount)
            return true;
        else
            return false;
    }

    public override bool FailConditionsMet()
    {
        return false;
    }

    public override void Complete()
    {
        Debug.Log("Quest Completed: ");
        PlayerInventory.Instance.Reduce(requiredItem, requiredAmount);
        PlayerInventory.Instance.Add(rewardItem, rewardQuantity);
    }

    public override void Fail()
    {
        // Cannot fail.
    }

    public override string GetObjective()
    {
        if (requiredAmount > 1)
            return "Collect " + requiredAmount + " " + requiredItem.ItemName + " and return them to " + npc.GetName() + ".";
        else
            return "Collect " + requiredAmount + " " + requiredItem.ItemName + " and return it to " + npc.GetName() + ".";
    }

    public override void ParseTags(List<string> tags)
    {
        // No special tags
    }
}
