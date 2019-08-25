using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveObject : MonoBehaviour
{

    public Vector3 direction;
    public float speed;

    void Update()
    {
        transform.Translate(direction * speed * Time.deltaTime);
    }
}
