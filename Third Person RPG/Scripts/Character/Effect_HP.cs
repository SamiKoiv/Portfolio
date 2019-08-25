using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Effect_HP : MonoBehaviour
{
    public float healthEffect;
    public int maxHits = 1;
    int hits;

    List<int> hashList = new List<int>();

    void OnEnable()
    {
        hashList.Clear();
        hits = 0;
    }

    public float GetHealthEffect(int hashID)
    {
        if (hashList.Contains(hashID) == false)
        {
            if (maxHits == 0 || hits < maxHits)
            {
                hits++;
                return healthEffect;
            }
        }

        return 0;
    }
}
