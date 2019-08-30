using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct Stats
{
    public int HP { get; }
    public int Attack { get; }
    public int Defence { get; }

    public Stats(int hp, int attack, int defence)
    {
        HP = hp;
        Attack = attack;
        Defence = defence;
    }
}
