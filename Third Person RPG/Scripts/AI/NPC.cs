using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NPC : MonoBehaviour
{
    NavMeshAgent agent;

    public bool drawGizmos;

    public Vector3 boundsMax;
    public Vector3 boundsMin;
    public Vector3 center;

    public float distanceToInterest;

    Vector3 currentInterest;

    Vector3 corner1;
    Vector3 corner2;
    Vector3 corner3;
    Vector3 corner4;
    Vector3 corner5;
    Vector3 corner6;
    Vector3 corner7;
    Vector3 corner8;

    void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
    }

    void Start()
    {
        currentInterest = RandomPosition();
        agent.SetDestination(currentInterest);
    }

    void Update()
    {
        distanceToInterest = Vector3.Distance(transform.position, currentInterest);

        if (agent.pathStatus == NavMeshPathStatus.PathPartial || agent.pathStatus == NavMeshPathStatus.PathInvalid)
        {
            currentInterest = RandomPosition();
            agent.SetDestination(currentInterest);

        }

        if (distanceToInterest < 1)
        {
            currentInterest = RandomPosition();
            agent.SetDestination(currentInterest);
        }
    }

    Vector3 RandomPosition()
    {
        return center + new Vector3(
            Random.Range(boundsMin.x, boundsMax.x),
            Random.Range(boundsMin.y, boundsMax.y),
            Random.Range(boundsMin.z, boundsMax.z));
    }

    void OnDrawGizmos()
    {
        if (drawGizmos)
        {
            corner1 = center + new Vector3(boundsMin.x, boundsMax.y, boundsMax.z);
            corner2 = center + new Vector3(boundsMax.x, boundsMax.y, boundsMax.z);
            corner3 = center + new Vector3(boundsMax.x, boundsMax.y, boundsMin.z);
            corner4 = center + new Vector3(boundsMin.x, boundsMax.y, boundsMin.z);
            corner5 = center + new Vector3(boundsMin.x, boundsMin.y, boundsMax.z);
            corner6 = center + new Vector3(boundsMax.x, boundsMin.y, boundsMax.z);
            corner7 = center + new Vector3(boundsMax.x, boundsMin.y, boundsMin.z);
            corner8 = center + new Vector3(boundsMin.x, boundsMin.y, boundsMin.z);

            Gizmos.DrawLine(corner1, corner2);
            Gizmos.DrawLine(corner2, corner3);
            Gizmos.DrawLine(corner3, corner4);
            Gizmos.DrawLine(corner4, corner1);

            Gizmos.DrawLine(corner5, corner6);
            Gizmos.DrawLine(corner6, corner7);
            Gizmos.DrawLine(corner7, corner8);
            Gizmos.DrawLine(corner8, corner5);

            Gizmos.DrawLine(corner1, corner5);
            Gizmos.DrawLine(corner2, corner6);
            Gizmos.DrawLine(corner3, corner7);
            Gizmos.DrawLine(corner4, corner8);

            Gizmos.DrawLine(transform.position, currentInterest);
            Gizmos.DrawSphere(currentInterest, 1);
        }



    }
}
