using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyAfterTime : MonoBehaviour
{
    public float timeToDestroy = 1;
    float t = 0f;

    void Update()
    {
        t += Time.deltaTime;

        if (t >= timeToDestroy)
        {
            GameObject.Destroy(this.gameObject);
        }
    }
}
