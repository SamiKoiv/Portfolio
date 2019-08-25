using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class QuestEvents
{

    Event_Function _characterDied = new Event_Function();
    Character _killedCharacter;

    public Character KilledCharacter
    {
        get
        {
            return _killedCharacter;
        }
    }

    public void CharacterDied(Character character)
    {
        _killedCharacter = character;
        _characterDied.Invoke();
    }

    public void Subscribe_CharacterDied(Action action)
    {
        _characterDied.Subscribe(action);
    }

    public void Unsubscribe_CharacterDied(Action action)
    {
        _characterDied.Unsubscribe(action);
    }

}
