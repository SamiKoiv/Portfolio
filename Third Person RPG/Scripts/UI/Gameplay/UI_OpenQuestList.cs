using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_OpenQuestList : MonoBehaviour
{
    //QuestManager questManager;

    Text text;

    // Feed for Open Quests when Open Quest is not yet activated (locked)
    string openFeed;

    // Feed for active (locked) Open Quest
    string lockedFeed = "";

    UI_Events events;
    List<OpenQuest> quests;
    public OpenQuest lockedQuest;

    Transform player;

    public bool realtimeDistance;
    string description;
    int distance;

    void Awake()
    {
        text = GetComponent<Text>();
        //questManager = Core.instance.QuestManager;
        events = Core.instance.UIEvents;

        player = Core.instance.GetPlayer();
    }

    void OnEnable()
    {
        events.OpenQuests_Changed.Subscribe(OpenQuestsChanged);
        events.LockedQuest_Changed.Subscribe(LockedQuestChanged);
    }

    void OnDisable()
    {
        events.OpenQuests_Changed.Unsubscribe(OpenQuestsChanged);
        events.LockedQuest_Changed.Unsubscribe(LockedQuestChanged);
    }

    void Update()
    {
        if (quests == null)
        {
            quests = events.OpenQuests_Get();
        }

        if (realtimeDistance == true && lockedQuest == null)
        {
            if (quests.Count == 0)
            {
                openFeed = "No Open Quests";
            }
            else
            {
                openFeed = "Open Quests: \n";

                foreach (OpenQuest quest in quests)
                {
                    description = quest.questName;
                    distance = (int)Vector3.Distance(player.position, quest.transform.position);

                    openFeed += "* " + description + " - " + distance + "\n";
                }

            }

            TextUpdate();
        }

    }

    void LockedQuestChanged()
    {
        lockedQuest = events.LockedQuest_Get();

        TextUpdate();
    }

    void OpenQuestsChanged()
    {

        quests = events.OpenQuests_Get();

        if (quests.Count == 0)
        {
            openFeed = "No Open Quests";
        }
        else
        {
            openFeed = "Open Quests: \n";

            foreach (OpenQuest quest in quests)
            {
                description = quest.questName;
                distance = (int)Vector3.Distance(player.position, quest.transform.position);

                openFeed += "* " + description + "\n";
            }

        }

        TextUpdate();
    }

    void TextUpdate()
    {
        if (lockedQuest != null)
        {
            text.text = lockedFeed;
        }
        else
        {
            text.text = openFeed;
        }
    }
}
