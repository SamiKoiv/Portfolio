using UnityEngine;

public class Objective_TalkToCharacter : Objective
{
    public delegate void VoidEvent();
    public event VoidEvent OnComplete;

    [SerializeField] CharacterTag character;

    public override void Initialize()
    {
        throw new System.NotImplementedException();
    }

    public override void Clean()
    {
        throw new System.NotImplementedException();
    }

    public override string GetObjective()
    {
        return "Collect " + character.GetName();
    }

    public override bool IsComplete()
    {
        return false;
    }
}
