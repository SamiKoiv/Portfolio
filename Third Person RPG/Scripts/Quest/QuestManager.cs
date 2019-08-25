using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 
/// Quest system consists of Quest Manager, Open Quests and Active Quests.
/// 
/// Open Quests can be thought of as storylines.
/// Multiple Open Quests can be available at the same time but only one can be activated at a time.
/// When activated, Open Quest is locked (QuestLock) and other Open Quests disappear until lock is released.
/// 
/// Active Quests are to be considered secondary in hierarchy under Open Quests.
/// Open Quest can include multiple Active Quests, both simultaneous and sequential.
/// Active Quests manage themselves and are to release QuestLock when the last Active Quest is completed.
/// 
/// </summary>

public class QuestManager
{

    List<OpenQuest> _openQuests = new List<OpenQuest>();
    List<ActiveQuest> _activeQuests = new List<ActiveQuest>();

    // GameObjects that indicate quest properties. Disabled if there are no Open Quests.
    List<GameObject> _questGameObjects = new List<GameObject>();

    OpenQuest _lockedQuest = null;
    UI_Events UIEvents;

    public QuestManager(UI_Events events)
    {
        UIEvents = events;
    }

    #region Add / Remove Quest Objects (Pointers etc.)

    public void AddQuestGameObject(GameObject go)
    {
        _questGameObjects.Add(go);
    }

    public void RemoveQuestGameObject(GameObject go)
    {
        _questGameObjects.Remove(go);
    }

    #endregion

    #region Add / Remove Quests

    public void AddOpenQuest(OpenQuest oq)
    {
        _openQuests.Add(oq);
        UIEvents.OpenQuests_Set(_openQuests);

        if (_openQuests.Count == 1)
        {
            foreach (GameObject go in _questGameObjects)
            {
                go.SetActive(true);
            }
        }
    }

    public void RemoveOpenQuest(OpenQuest oq)
    {
        _openQuests.Remove(oq);
        UIEvents.OpenQuests_Set(_openQuests);

        if (_openQuests.Count == 0)
        {
            foreach (GameObject go in _questGameObjects)
            {
                go.SetActive(false);
            }
        }
    }

    public void AddActiveQuest(ActiveQuest aq)
    {
        _activeQuests.Add(aq);
    }

    public void RemoveActiveQuest(ActiveQuest aq)
    {
        _activeQuests.Remove(aq);
    }

    #endregion

    #region Quests Available

    public bool QuestsOpen()
    {
        if (_openQuests.Count != 0)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public bool QuestsActive()
    {
        if (_activeQuests.Count != 0)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    #endregion

    #region Closest Target Position

    public Vector3 GetClosestTargetPosition()
    {

        Vector3 closestPosition = Vector3.zero;

        if (QuestsActive())
        {
            Vector3 playerPos = Core.instance.GetPlayer().transform.position;
            ActiveQuest closestTarget = null;
            float closestDistance = -1;

            foreach (ActiveQuest aq in _activeQuests)
            {
                float distance = Vector3.Distance(playerPos, aq.transform.position);

                if (distance < closestDistance || closestDistance < 0)
                {
                    closestTarget = aq;
                    closestDistance = distance;
                }
            }

            closestPosition = closestTarget.transform.position;

        }
        else if (QuestsOpen())
        {
            Vector3 playerPos = Core.instance.GetPlayer().transform.position;
            OpenQuest closestTarget = null;
            float closestDistance = -1;

            foreach (OpenQuest oq in _openQuests)
            {
                float distance = Vector3.Distance(playerPos, oq.transform.position);

                if (distance < closestDistance || closestDistance < 0)
                {
                    closestTarget = oq;
                    closestDistance = distance;
                }
            }

            closestPosition = closestTarget.transform.position;

        }



        return closestPosition;
    }

    #endregion

    #region Quest Lock

    public void LockQuest(OpenQuest openQuest)
    {
        _lockedQuest = openQuest;
        UIEvents.LockedQuest_Lock(_lockedQuest);

        foreach (OpenQuest oq in _openQuests)
        {
            oq.gameObject.SetActive(false);
        }
    }

    public void ReleaseQuest()
    {
        _lockedQuest = null;
        UIEvents.LockedQuest_Release();

        foreach (OpenQuest oq in _openQuests)
        {
            oq.gameObject.SetActive(true);
        }
    }

    #endregion

}
