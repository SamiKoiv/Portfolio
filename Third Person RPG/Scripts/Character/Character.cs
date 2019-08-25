using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Character : MonoBehaviour
{
    [Header("Character")]
    public CharacterProfile characterProfile;
    public float hp;
    public bool dead;

    public void Heal(float power)
    {
        if (!characterProfile.invulnerable)
        {
            print(characterProfile.CharacterName + " was healed for " + power + " hp.");
            hp += power;
        }
    }

    public void Damage(float power)
    {
        if (!characterProfile.invulnerable && !dead)
        {
            print(characterProfile.CharacterName + " was hit and dealt " + power + " damage.");
            hp -= power;

            if (hp <= 0)
            {
                Die();
            }
        }
    }

    public virtual void Die()
    {
        print(characterProfile.CharacterName + " was slain and lies lifeless on the ground.");
        dead = true;
        gameObject.SetActive(false);
    }
}
