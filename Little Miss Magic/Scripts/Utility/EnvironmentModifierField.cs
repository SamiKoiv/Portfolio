using UnityEngine;

public class EnvironmentModifierField : MonoBehaviour
{
    [Header ("Trigger area effect")]
    public bool Light;
    public bool Shadow;
    public bool Hot;
    public bool Cold;
    public bool Dry;
    public bool Humid;

    private void OnTriggerEnter(Collider other)
    {
        IEnvironmentModifiable modifiable = other.GetComponent<IEnvironmentModifiable>();

        if (modifiable != null)
        {
            Debug.Log("Seed found");

            if (Light)
                modifiable.SetIsLit(true);

            if (Shadow)
                modifiable.SetIsShadowed(true);

            if (Hot)
                modifiable.SetIsHot(true);

            if (Cold)
                modifiable.SetIsCold(true);

            if (Dry)
                modifiable.SetIsDry(true);

            if (Humid)
                modifiable.SetIsHumid(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        IEnvironmentModifiable modifiable = other.GetComponent<IEnvironmentModifiable>();

        if (modifiable != null)
        {
            if (Light)
                modifiable.SetIsLit(false);

            if (Shadow)
                modifiable.SetIsShadowed(false);

            if (Hot)
                modifiable.SetIsHot(false);

            if (Cold)
                modifiable.SetIsCold(false);

            if (Dry)
                modifiable.SetIsDry(false);

            if (Humid)
                modifiable.SetIsHumid(false);
        }
    }
}
