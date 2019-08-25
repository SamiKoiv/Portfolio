using UnityEngine;

public class EventBool_SetActiveGO : MonoBehaviour
{
    public EventBool activationEvent;
    public GameObject activatedGO;

    void OnEnable()
    {
        activationEvent.OnChange += SetActive;
    }

    void OnDisable()
    {
        activationEvent.OnChange -= SetActive;
    }

    void SetActive(bool active)
    {
        activatedGO.SetActive(active);
    }
}
