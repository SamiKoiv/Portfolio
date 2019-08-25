using System;
using UnityEngine;

public class Placeable : MonoBehaviour
{
    public Collider[] colliders;
    public bool[] collidersActive;
    public Renderer[] renderers;
    public bool[] renderersActive;
    public Rigidbody[] rigidbodies;
    public bool[] rigidbodiesKinematic;

    bool suppressed;

    public void Suppress(Material placingMaterial)
    {
        colliders = GetComponentsInChildren<Collider>();
        renderers = GetComponentsInChildren<Renderer>();
        rigidbodies = GetComponentsInChildren<Rigidbody>();

        collidersActive = new bool[colliders.Length];
        renderersActive = new bool[renderers.Length];
        rigidbodiesKinematic = new bool[rigidbodies.Length];

        for (int i = 0; i < colliders.Length; i++)
        {
            collidersActive[i] = colliders[i].enabled;
            colliders[i].enabled = false;
        }


        for (int i = 0; i < renderers.Length; i++)
        {
            renderersActive[i] = renderers[i].enabled;
            renderers[i].material = placingMaterial;
        }

        for (int i = 0; i < rigidbodies.Length; i++)
        {
            rigidbodiesKinematic[i] = rigidbodies[i].isKinematic;
            rigidbodies[i].isKinematic = true;
        }

        suppressed = true;
    }

    void Release()
    {
        for (int i = 0; i < colliders.Length; i++)
        {
            colliders[i].enabled = collidersActive[i];

        }

        for (int i = 0; i < renderers.Length; i++)
        {
            renderers[i].enabled = renderersActive[i];
        }

        for (int i = 0; i < rigidbodies.Length; i++)
        {
            rigidbodies[i].isKinematic = rigidbodiesKinematic[i];
        }
    }

    internal void Suppress(object placeableMaterial)
    {
        throw new NotImplementedException();
    }
}
