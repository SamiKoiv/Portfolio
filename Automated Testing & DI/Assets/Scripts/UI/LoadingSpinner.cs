using UnityEngine;
using UniRx;

public class LoadingSpinner : MonoBehaviour
{
    [SerializeField] float minSpeed = 1;
    [SerializeField] float topSpeed = 1;
    [SerializeField] float rate = 1;

    float i = 0.3f;
    new Transform transform;

    private void OnEnable()
    {
        i = 0.3f;
    }

    void Start()
    {
        transform = gameObject.transform;

        Observable.EveryLateUpdate()
            .Subscribe(x => {
                transform.Rotate(-Vector3.forward * minSpeed + -Vector3.forward * Mathf.Abs(Mathf.Sin(i * rate)) * topSpeed);
                i += Time.deltaTime;
            });
    }
}
