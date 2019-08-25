using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_TransformRotator : MonoBehaviour
{
    public float rotation;

    void Update()
    {
        transform.Rotate(Vector3.forward * Mathf.Sin(Time.time) * rotation * Time.deltaTime);
    }
}
