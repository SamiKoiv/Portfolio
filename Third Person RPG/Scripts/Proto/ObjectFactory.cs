using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectFactory : MonoBehaviour
{
    public bool drawGizmos;

    public GameObject unit;

    public int copiesX;
    public int copiesY;

    public int offset;

    Vector3 currentPosition;

    void Start()
    {
        currentPosition = transform.position;

        for (int y = 0; y < copiesY; y++)
        {
            for (int x = 0; x < copiesX; x++)
            {
                GameObject.Instantiate(unit,
                    new Vector3(currentPosition.x + x * offset, currentPosition.y, currentPosition.z + y * offset),
                    transform.rotation);
            }
        }
    }

    void OnDrawGizmos()
    {
        if (drawGizmos)
        {
            currentPosition = transform.position;

            for (int y = 0; y < copiesY; y++)
            {
                for (int x = 0; x < copiesX; x++)
                {
                    Gizmos.DrawCube(new Vector3(currentPosition.x + x * offset, currentPosition.y, currentPosition.z + y * offset), Vector3.one);
                }
            }
        }
    }
}
