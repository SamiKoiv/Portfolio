using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 
/// OPEN QUEST
/// 
/// Quests that are open in the world around and open for the taking.
/// Only one Open Quest should be active at a time.
/// Think of it as a story you start.
/// 
/// When started, activated Open Quest gets locked for completion.
/// The other Open Quests are hidden until all Active Quests are completed.
/// 
/// </summary>

public class OpenQuest : MonoBehaviour
{
    QuestManager questManager;

    public string questName;
    public string description;
    public bool mainQuest;
    public bool completedOnce;
    public bool repeatable;

    void Awake()
    {
        questManager = Core.instance.QuestManager;
    }

    void OnEnable()
    {
        questManager.AddOpenQuest(this);
    }

    public void StartQuest()
    {
        questManager.LockQuest(this);
    }

    public void FinishQuest()
    {
        if (mainQuest)
        {
            mainQuest = false;
        }

        gameObject.SetActive(false);
    }
}
