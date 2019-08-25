using UnityEngine;

public class UI_LerpingMenu : ManagedBehaviour_Update
{
    public bool Open;
    [SerializeField] Float_ReadOnly globalLerpSpeed;

    public Vector2 InactivePosition;
    public Vector2 ActivePosition;


    public bool LerpX;
    public bool LerpY;

    public GameObject[] items = new GameObject[1];
    Transform[] transforms;
    Vector2[] itemPositions;

    new RectTransform transform;
    float prog;

    private void Awake()
    {
        transform = GetComponent<RectTransform>();

        transforms = new Transform[items.Length];
        itemPositions = new Vector2[items.Length];

        for (int i = 0; i < items.Length; i++)
        {
            transforms[i] = items[i].transform;
            itemPositions[i] = items[i].transform.position;
        }
    }

    private void OnEnable()
    {
        Subscribe_Update();
    }

    private void OnDisable()
    {
        Unsubscribe_Update();
    }

    public override void M_Update()
    {
        if (Open)
        {
            prog = Mathf.Lerp(prog, 1, prog * globalLerpSpeed.Value * Time.deltaTime);

            for (int i = 0; i < items.Length; i++)
            {

            }
        }
        else
        {
            prog = Mathf.Lerp(prog, 0, prog * globalLerpSpeed.Value * Time.deltaTime);

            for (int i = 0; i < items.Length; i++)
            {

            }
        }
    }
}
