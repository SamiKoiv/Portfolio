using UnityEngine;

[System.Serializable]
public class ObjectiveSet
{
    [SerializeField] Objective[] objectives;

    public bool SetComplete
    {
        get
        {
            foreach(Objective o in objectives)
            {
                if (!o.IsComplete())
                    return false;
            }
            return true;
        }
    }
}
