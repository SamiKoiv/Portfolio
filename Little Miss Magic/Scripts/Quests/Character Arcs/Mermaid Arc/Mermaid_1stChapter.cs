using System.Collections.Generic;
using UnityEngine;

namespace Quests.CharacterArcs
{
    public class Mermaid_1stChapter : Quest
    {
        Item requiredItem;
        static bool startedOnce;

        public Mermaid_1stChapter()
        {
            Database DB = Core.Instance.DB;

            story = DB.StoryDB.MermaidStory;
            npc = DB.CharacterDB.Mermaid;
            requiredItem = DB.ItemDB.Quest.MermaidShoe;

            story.variablesState["chapter"] = 1;
            story.variablesState["player"] = GameManager.Instance.PlayerName;
            story.variablesState["item"] = requiredItem.ItemName;

            AssignID();

            SetInvolvedCharacters(new CharacterTag[] { npc });
        }

        public override bool CanStart(CharacterTag character)
        {
            if (!startedOnce && character == Core.Instance.DB.CharacterDB.Mermaid)
                return true;

            return false;
        }

        public override void Start()
        {
            // No actions as of yet.
            startedOnce = true;
        }

        public override bool CompletionConditionsMet()
        {
            if (PlayerInventory.Instance.GetAmount(requiredItem) >= 1)
                return true;
            else
                return false;
        }

        public override bool FailConditionsMet()
        {
            // Cannot fail.
            return false;
        }

        public override string GetObjective()
        {
            return "Help " + npc.GetName() + " find her lost " + requiredItem.ItemName + ".";
        }

        public override void ParseTags(List<string> tags)
        {

        }

        public override void Complete()
        {
            previouslyCompleted = true;
            PlayerInventory.Instance.Reduce(requiredItem, 1);
            PlayerInventory.Instance.Add(Core.Instance.DB.ItemDB.Money, requiredItem.Price);
        }

        public override void Fail()
        {
            throw new System.NotImplementedException();
        }
    }
}
