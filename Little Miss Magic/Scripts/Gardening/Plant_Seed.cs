using UnityEngine;

public class Plant_Seed : MonoBehaviour, IEnvironmentModifiable
{
    [Header("Mode")]

    [Header("Plant")]
    public GameObject PlantPrefab;
    public float LifetimeUngrown;

    [Header("Requirements for growth")]
    [SerializeField] bool isSown;
    [SerializeField] int germinationTime;

    [Header("Required Modifiers")]
    [SerializeField] bool requiresLight;
    [SerializeField] bool requiresShadow;
    [SerializeField] bool requiresHot;
    [SerializeField] bool requiresCold;
    [SerializeField] bool requiresDry;
    [SerializeField] bool requiresHumid;

    [Header("Avoid Modifiers")]
    [SerializeField] bool avoidLight;
    [SerializeField] bool avoidShadow;
    [SerializeField] bool avoidHot;
    [SerializeField] bool avoidCold;
    [SerializeField] bool avoidDry;
    [SerializeField] bool avoidHumid;

    [Header("Global limiters")]
    [SerializeField] int maxSeeds;

    static int s_spawnSeeds;
    static int s_maxSeeds;

    float t;

    public static int SpawnSeeds
    {
        get
        {
            return s_spawnSeeds;
        }
    }

    public static int MaxSeeds
    {
        get
        {
            return s_maxSeeds;
        }
    }

    int isLit;
    int isShadowed;
    int isHot;
    int isCold;
    int isDry;
    int isHumid;

    Rigidbody rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void Start()
    {
        s_maxSeeds = maxSeeds;
    }

    private void Update()
    {
        t += Time.deltaTime;

        if (t >= LifetimeUngrown)
            Despawn();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.CompareTag("Earth"))
        {
            isSown = true;

            if (RequirementsMet())
                StartGrowth();
        }
    }

    void StartGrowth()
    {
        if (s_spawnSeeds < maxSeeds)
        {
            Instantiate(PlantPrefab, new Vector3(transform.position.x, 0, transform.position.z), Quaternion.Euler(Vector3.zero + Vector3.up * Random.Range(0f, 360f))).GetComponent<Plant>().AddGrowthTime(3);
            rb.velocity = Vector3.zero;
            rb.isKinematic = true;

            Despawn();
            s_spawnSeeds++;
        }
    }

    void Despawn()
    {
        Destroy(gameObject);
    }

    bool RequirementsMet()
    {
        // Return true by default and filter through all of the eliminating conditions.

        if (requiresLight && isLit <= 0)
            return false;

        if (requiresShadow && isShadowed <= 0)
            return false;

        if (requiresHot && isHot <= 0)
            return false;

        if (requiresCold && isCold <= 0)
            return false;

        if (requiresDry && isDry <= 0)
            return false;

        if (requiresHumid && isHumid <= 0)
            return false;

        if (avoidLight && isLit > 0)
            return false;

        if (avoidShadow && isShadowed > 0)
            return false;

        if (avoidHot && isHot > 0)
            return false;

        if (avoidCold && isCold > 0)
            return false;

        if (avoidDry && isDry > 0)
            return false;

        if (avoidHumid && isHumid > 0)
            return false;

        return true;
    }

    public void SetIsCold(bool isCold)
    {
        if (isCold)
        {
            this.isCold++;
        }
        else
        {
            this.isCold--;
        }
    }

    public void SetIsDry(bool isDry)
    {
        if (isDry)
        {
            this.isDry++;
        }
        else
        {
            this.isDry--;
        }
    }

    public void SetIsHot(bool isHot)
    {
        if (isHot)
        {
            this.isHot++;
        }
        else
        {
            this.isHot--;
        }
    }

    public void SetIsHumid(bool isHumid)
    {
        if (isHumid)
        {
            this.isHumid++;
        }
        else
        {
            this.isHumid--;
        }
    }

    public void SetIsLit(bool isLit)
    {
        if (isLit)
        {
            this.isLit++;
        }
        else
        {
            this.isLit--;
        }
    }

    public void SetIsShadowed(bool isShadowed)
    {
        if (isShadowed)
        {
            this.isShadowed++;
        }
        else
        {
            this.isShadowed--;
        }
    }
}
