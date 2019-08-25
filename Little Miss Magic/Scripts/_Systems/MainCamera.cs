using UnityEngine;

public class MainCamera : MonoBehaviour
{
    void Start()
    {
        EventSystem.Objects.Broadcast_MainCamera(gameObject);
    }
}
