using UnityEngine;

public abstract class Objective
{
    public abstract void Initialize();
    public abstract void Clean();
    public abstract string GetObjective();
    public abstract bool IsComplete();
}
