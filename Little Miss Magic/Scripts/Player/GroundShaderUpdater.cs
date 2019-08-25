using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundShaderUpdater : MonoBehaviour
{
    public Material GroundMaterial;

    new Transform transform;
    Vector3 position;

    void Start()
    {
        transform = gameObject.transform;
    }

    void Update()
    {
        position = transform.position;
        GroundMaterial.SetVector("_ObjectPos", new Vector4(position.x, position.y, position.z, 0));
    }
}
