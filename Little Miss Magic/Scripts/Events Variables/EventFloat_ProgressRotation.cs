using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventFloat_ProgressRotation : MonoBehaviour
{
    [SerializeField] EventFloat progress;
    [SerializeField] float progressOffset;

    public Vector3 rotationAngle;
    Vector3 originalRot;

    private void Awake()
    {
        originalRot = transform.eulerAngles;
    }

    private void OnEnable()
    {
        progress.OnChange += TrackProgress;
    }

    private void OnDisable()
    {
        progress.OnChange -= TrackProgress;
    }

    void TrackProgress(float prog)
    {
            transform.rotation = Quaternion.Euler(originalRot + rotationAngle.normalized * 360 * (prog + progressOffset));
    }
}
