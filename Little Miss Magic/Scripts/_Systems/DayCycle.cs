using UnityEngine;

public class DayCycle : MonoBehaviour
{
    [SerializeField] EventFloat dayProgress;
    Vector3 originalRot;

    WorldClock clock;

    private void Awake()
    {
        originalRot = transform.eulerAngles;
    }

    private void OnEnable()
    {
        dayProgress.OnChange += TrackProgress;
    }

    private void OnDisable()
    {
        dayProgress.OnChange -= TrackProgress;
    }

    void TrackProgress(float prog)
    {
        transform.rotation = Quaternion.Euler(originalRot - Vector3.right * (originalRot.x + 90) + Vector3.right * 360 * prog);
    }
}
