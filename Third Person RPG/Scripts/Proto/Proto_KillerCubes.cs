using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Proto_KillerCubes : MonoBehaviour
{
    Vector3 startingposition;
    public Vector3 maxDistance;
    public float speed = 1;

    float t;

    void Start()
    {
        startingposition = transform.position;
    }

    void Update()
    {
        transform.position = startingposition + maxDistance * Mathf.Sin(t);
        t += Time.deltaTime * speed;
    }

    void OnDrawGizmos()
    {
        Gizmos.DrawLine(transform.position - maxDistance, transform.position + maxDistance);
    }
}
