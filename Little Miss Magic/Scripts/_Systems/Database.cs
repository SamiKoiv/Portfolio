using Ink.Runtime;
using Quests.CharacterArcs;
using System.Collections.Generic;
using UnityEngine;

public class Database : ScriptableObject
{
    [SerializeField] CharacterDatabase characterDatabase;
    public CharacterDatabase CharacterDB { get { return characterDatabase; } }

    [SerializeField] StoryDatabase storyDatabase;
    public StoryDatabase StoryDB { get { return storyDatabase; } }

    [SerializeField] QuestDatabase questDatabase;
    public QuestDatabase QuestDB { get { return questDatabase; } }

    [SerializeField] ItemDatabase itemDatabase;
    public ItemDatabase ItemDB { get { return itemDatabase; } }

    //--------------------------------------------------------------------------------------------

    public void BuildDatabase()
    {
        storyDatabase.BuildDatabase(this);
        questDatabase.BuildDatabase();
    }

    [System.Serializable]
    public class CharacterDatabase
    {
        [SerializeField] CharacterTag mermaid;
        public CharacterTag Mermaid { get { return mermaid; } }

        public void BuildDatabase()
        {

        }
    }

    [System.Serializable]
    public class StoryDatabase
    {
        Database DB;

        CharacterDatabase characterDB;

        [SerializeField] TextAsset mermaidStoryJson;
        Story mermaidStory;
        public Story MermaidStory { get { return mermaidStory; } }

        public void BuildDatabase(Database DB)
        {
            this.DB = DB;

            mermaidStory = new Story(mermaidStoryJson.text);
            mermaidStory.variablesState["player"] = GameManager.Instance.PlayerName;
            mermaidStory.variablesState["mermaid"] = DB.CharacterDB.Mermaid.GetName();
        }

        public Story GetCharacterStory(CharacterTag character)
        {
            if (character = DB.CharacterDB.Mermaid)
                return MermaidStory;
            else
                return null;
        }
    }

    [System.Serializable]
    public class QuestDatabase
    {

        [Header("Procedural Quests")]
        [SerializeField] TextAsset collectionQuest_inkJsonAsset;
        public TextAsset CollectionQuest_Json { get { return collectionQuest_inkJsonAsset; } }

        Quest[] quests = new Quest[1];

        public void BuildDatabase()
        {
            int i = 0;
            quests[i] = new Mermaid_1stChapter();
        }

        public List<Quest> GetOpenQuests(CharacterTag character)
        {
            List<Quest> results = new List<Quest>();

            foreach (Quest q in quests)
            {
                if (q.CanStart(character))
                    results.Add(q);
            }

            return results;
        }
    }

    [System.Serializable]
    public class ItemDatabase
    {
        [SerializeField] Item money;
        public Item Money { get { return money; } }

        public CommonItems Common;
        public QuestItems Quest;

        [System.Serializable]
        public class CommonItems
        {
            [SerializeField] Item apple;

            public Item GetRandom()
            {
                return apple;
            }
        }

        [System.Serializable]
        public class QuestItems
        {
            [SerializeField] Item mermaidShoe;
            public Item MermaidShoe { get { return mermaidShoe; } }
        }

        Item RandomFromArray(Item[] array)
        {
            int rng = Random.Range(0, array.Length);
            return array[rng];
        }

        public void BuildDatabase()
        {

        }
    }

}
