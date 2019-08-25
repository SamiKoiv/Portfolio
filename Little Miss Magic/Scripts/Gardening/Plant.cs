using UnityEngine;

public class Plant : MonoBehaviour, IWaterable
{
    [Range(0, 1)] public float growth = 1;
    [SerializeField] float timeUntilGrown;
    [SerializeField] float maxGrowth;
    [SerializeField] float water;
    [SerializeField] float waterConsumption;

    [SerializeField] Transform model;
    [SerializeField] Plant_SeedLauncher seedLauncher;

    bool plantComplete;

    void Update()
    {
        water = Mathf.Max(water - Time.deltaTime * waterConsumption, 0);

        if (water > 0)
        {
            growth = Mathf.Min(growth + Time.deltaTime / timeUntilGrown, 1);

            Grow();
        }

        if (plantComplete == false && growth == 1)
        {
            plantComplete = true;
            seedLauncher.enabled = true;
            this.enabled = false;
        }
    }

    void OnValidate()
    {
        Grow();
    }

    void Grow()
    {
        model.localScale = Vector3.one * growth;
    }

    public void AddGrowthTime(float time)
    {
        water += Mathf.Max(time * waterConsumption, 1);
    }

    public void Water()
    {
        water += 1;
    }
}
