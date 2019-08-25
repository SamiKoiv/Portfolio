using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SineScaler : MonoBehaviour
{

    [SerializeField] float rate;
    [SerializeField] Vector3 increase;

    new Transform transform;
    Vector3 originalScale;

    float t;

    void Awake()
    {
        transform = gameObject.transform;
        originalScale = transform.localScale;
    }

    void Update()
    {
        float prog = Mathf.Abs(Mathf.Sin(t));

        transform.localScale = Vector3.Lerp(originalScale, originalScale + increase, prog);
        t += rate * Time.deltaTime;
    }
}
