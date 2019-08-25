using UnityEngine;

public class Plant_SeedLauncher : MonoBehaviour
{
    public GameObject SeedPrefab;
    Rigidbody seedRigidbody;

    public float launchTime;
    public float minForce;
    public float maxForce;

    float t;

    void Update()
    {
        t += Time.deltaTime;

        if (t >= launchTime)
        {
            if (Plant_Seed.SpawnSeeds < Plant_Seed.MaxSeeds)
            {
                LaunchSeed();
            }
        }
    }

    void LaunchSeed()
    {
        transform.Rotate(Vector3.up, Random.Range(-180f, 180f));
        seedRigidbody = Instantiate(SeedPrefab, transform.position, transform.rotation).GetComponent<Rigidbody>();

        seedRigidbody.velocity = transform.forward * Random.Range(minForce, maxForce);

        t = 0;
    }
}
