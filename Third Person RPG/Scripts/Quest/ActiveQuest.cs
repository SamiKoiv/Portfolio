using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// 
/// ACTIVE QUEST
/// 
/// Active Quests are sequences of smaller missions inside Open Quest.
/// Active Quests are to be kept brief, e.g. "Kill unit X" or "Go to X".
/// 
/// Active Quests are to be linked so that they manage the progress among themselves.
/// 
/// </summary>

public class ActiveQuest : MonoBehaviour
{
    public string description;
    public UnityEvent nextEvent;

    void OnEnable()
    {
        Core.instance.QuestManager.AddActiveQuest(this);
    }

    public void Finish()
    {
        nextEvent.Invoke();
        gameObject.SetActive(false);
    }
}
