using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GeneratedQuest
{
    public Objective[] Objectives;
    public CharacterTag Giver;

    public void Initialize()
    {
        foreach(Objective o in Objectives)
        {
            o.Initialize();
        }
    }

    // Finish by talking to giver
    public bool AllObjectivesCompleted()
    {
        foreach(Objective o in Objectives)
        {
            if (!o.IsComplete())
                return false;
        }

        return true;
    }
}
