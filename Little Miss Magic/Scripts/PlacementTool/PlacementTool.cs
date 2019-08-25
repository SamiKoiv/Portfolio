using InventorySystem;
using UnityEngine;
using System.Collections.Generic;

public class PlacementTool : ManagedBehaviour_Update
{
    #region Serialized Variables

    [Header("Controls")]
    public bool IgnoreCollision;

    [Header("Mandatory Data")]
    [SerializeField] Renderer placementRenderer;
    [SerializeField] Material placementMaterial;
    [SerializeField] Material maxDistanceMaterial;
    [SerializeField] Transform maxDistanceT;
    [SerializeField] ParticleSystem placingParticles;
    [SerializeField] Material placeableMaterial;

    [Header("Indicator Colors")]
    [SerializeField] bool PlacementMesh;
    [SerializeField] Color ValidPlacementColor;
    [SerializeField] Color InvalidPlacementColor;
    [SerializeField] Color MaxDistanceColor;

    [Header("Placement Data")]
    [SerializeField] float MaxDistance;
    [SerializeField] bool placeContinuous;

    [Header("Juice")]
    [SerializeField] float lerpSpeed;

    #endregion

    #region Private Variables

    Renderer maxdistRenderer;

    new Camera camera;
    Ray ray;
    RaycastHit[] hits;

    Transform player;

    bool selectionEnabled;
    List<Collider> collidersInside = new List<Collider>();

    GameObject placeableGO;
    GameObject placeableGo_clean;
    Transform placeableT;
    Placeable placeable;

    bool canDetect;
    bool canPlace;

    float maxDistanceLerp;

    bool toolActive;

    Vector2 dragStartPos;

    bool entrySet;
    InventoryEntry entry;

    int quantity;

    bool placementButton;
    bool placementButtonDown;
    bool placementButtonUp;

    #endregion

    #region Monobehaviour

    private void Awake()
    {
        maxdistRenderer = maxDistanceT.GetComponentInChildren<Renderer>();
        placementRenderer.enabled = false;
        maxdistRenderer.enabled = false;
    }

    private void OnEnable()
    {
        EventSystem.Objects.OnBroadcast_MainCamera += GetCamera;
        EventSystem.Objects.OnBroadcast_Player += GetPlayer;

        EventSystem.GameEvents.OnPlacing_Start += StartPlacing;
        EventSystem.GameEvents.OnPlacing_Stop += StopPlacing;
        EventSystem.GameEvents.OnPlacing_PlaceGO += PlaceGO;
        EventSystem.GameEvents.OnPlacing_PlaceEntry += PlaceEntry;

        InputSystem.OnPlacementButton += GetPlacementButton;
        InputSystem.OnPlacementButtonDown += GetPlacementButtonDown;
        InputSystem.OnPlacementButtonUp += GetPlacementButtonUp;
        InputSystem.OnPlacementCancel += StopPlacing;

        maxDistanceLerp = 0;
    }

    private void OnDisable()
    {
        EventSystem.Objects.OnBroadcast_MainCamera -= GetCamera;
        EventSystem.Objects.OnBroadcast_Player -= GetPlayer;

        EventSystem.GameEvents.OnPlacing_Start -= StartPlacing;
        EventSystem.GameEvents.OnPlacing_Stop -= StopPlacing;
        EventSystem.GameEvents.OnPlacing_PlaceGO -= PlaceGO;
        EventSystem.GameEvents.OnPlacing_PlaceEntry -= PlaceEntry;

        InputSystem.OnPlacementButton -= GetPlacementButton;
        InputSystem.OnPlacementButtonDown -= GetPlacementButtonDown;
        InputSystem.OnPlacementButtonUp -= GetPlacementButtonUp;
        InputSystem.OnPlacementCancel -= StopPlacing;

        if (toolActive)
        {
            Unsubscribe_Update();
            toolActive = false;
        }

        placementRenderer.enabled = false;
        maxdistRenderer.enabled = false;
    }

    private void OnValidate()
    {
        GetComponentInChildren<MeshRenderer>().material = placementMaterial;

        maxDistanceT.GetComponentInChildren<Renderer>().material = maxDistanceMaterial;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Earth"))
            return;
        else if (other.CompareTag("Floor"))
            return;
        else if (other.CompareTag("Wall"))
            return;
        else if (other.CompareTag("Surface"))
            return;

        if (!collidersInside.Contains(other))
            collidersInside.Add(other);
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Earth"))
            return;
        else if (other.CompareTag("Floor"))
            return;
        else if (other.CompareTag("Wall"))
            return;
        else if (other.CompareTag("Surface"))
            return;

        if (collidersInside.Contains(other))
            collidersInside.Remove(other);
    }

    private void OnDrawGizmos()
    {
        // Draw sphere on every raycast hit
        if (hits != null && hits.Length > 0)
        {
            Gizmos.DrawLine(ray.origin, hits[0].point);

            for (int i = hits.Length - 1; i >= 0; i--)
            {
                if (i == hits.Length - 1)
                    Gizmos.color = Color.green;
                else if (hits[i].transform.CompareTag("Player"))
                    Gizmos.color = Color.yellow;
                else
                    Gizmos.color = Color.red;

                Gizmos.DrawSphere(hits[i].point, 0.1f);
            }
        }
    }

    #endregion

    #region Start, Stop & Preparations

    void StartPlacing()
    {
        Subscribe_Update();
        EventSystem.CameraEvents.SetMode(CameraMode.Top);
        EventSystem.CameraEvents.Lock(gameObject);

        if (PlacementMesh)
            placementRenderer.enabled = true;

        maxdistRenderer.enabled = true;
        toolActive = true;

        InputSystem.StartPlacementTool();

        collidersInside.Clear();
    }

    void StopPlacing()
    {
        if (!toolActive)
            return;

        Unsubscribe_Update();
        EventSystem.CameraEvents.Unlock(gameObject);
        EventSystem.CameraEvents.PreviousMode();
        placementRenderer.enabled = false;
        maxdistRenderer.enabled = false;
        maxDistanceLerp = 0;
        maxDistanceT.localScale = Vector3.zero;
        toolActive = false;

        InputSystem.StopPlacementTool();

        if (placeableGO != null)
        {
            Destroy(placeableGO);
        }
    }

    void PlaceEntry(InventoryEntry entry, int quantity)
    {
        Debug.Log("Entry set to place: " + entry.ItemName + ", loaded with " + entry.ItemName);

        if (!toolActive)
            return;

        entrySet = true;
        this.entry = entry;
        this.quantity = quantity;

        SetPlaceableObject(entry.WorldPrefab);
    }

    void PlaceGO(GameObject go)
    {
        entrySet = false;
        quantity = 1;

        SetPlaceableObject(go);
    }

    void SetPlaceableObject(GameObject go)
    {
        if (!toolActive)
            return;

        if (placeableGO != null)
        {
            Destroy(placeableGO);
            placeableGo_clean = null;
        }

        if (go != null)
        {
            placeableGO = Instantiate(go, transform.position, transform.rotation);
            placeableGO.layer = 9;
            placeableGo_clean = go;

            placeableT = placeableGO.transform;
            placeable = placeableGO.AddComponent<Placeable>();
            placeable.Suppress(placeableMaterial);
        }
        else
        {
            placeableGO = null;
            placeableGo_clean = null;
        }
    }

    #endregion

    #region Update

    public override void M_Update()
    {
        maxDistanceLerp = Mathf.Lerp(maxDistanceLerp, MaxDistance, Time.deltaTime * lerpSpeed);

        maxDistanceT.position = player.position;
        maxDistanceT.localScale = Vector3.one * maxDistanceLerp * 2;

        ray = camera.ScreenPointToRay(Input.mousePosition);
        hits = Physics.RaycastAll(ray);

        CheckDetection();
        CheckPlacing();
        SetColors();

        if (canDetect)
        {
            if (placeableGO != null)
            {
                MoveRotateObject();

                if (canPlace)
                {
                    Placing();
                }
            }
        }

        ResetButtons();
    }

    #endregion

    #region Detection & Checks

    void CheckDetection()
    {
        // Differentiate whether mouse pointed at an object or empty space.
        if (hits.Length != 0)
        {
            if (!selectionEnabled)
            {
                if (PlacementMesh)
                    placementRenderer.enabled = true;

                selectionEnabled = true;
            }

            for (int i = hits.Length - 1; i >= 0; i--)
            {
                if (!hits[i].transform.CompareTag("Player") && hits[i].transform.gameObject.layer != 9)
                {
                    transform.position = hits[i].point;
                    canDetect = true;
                    return;
                }
            }
        }

        placementRenderer.enabled = false;
        selectionEnabled = false;
        canDetect = false;
    }

    void CheckPlacing()
    {
        // Check if area is within max distance.
        if (MaxDistance > 0 && Vector3.Distance(transform.position, player.position) > MaxDistance)
        {
            canPlace = false;
            return;
        }

        // Check if area is free of obstacles.
        if (collidersInside.Count == 0 || IgnoreCollision)
        {
            canPlace = true;
        }
        else
        {
            canPlace = false;
        }
    }

    #endregion

    #region Placing Object

    void MoveRotateObject()
    {
        if (placementButtonDown)
        {
            dragStartPos = Input.mousePosition;
        }

        if (placementButton || placementButtonUp)
        {
            if (Vector2.Distance(dragStartPos, Input.mousePosition) > 30)
            {
                placeableT.rotation = Quaternion.LookRotation(new Vector3(transform.position.x - placeableT.position.x, 0, transform.position.z - placeableT.position.z), Vector3.up);
            }
        }
        else
        {
            placeableT.position = transform.position;
        }
    }

    void Placing()
    {
        if (placeContinuous && placementButton)
        {
            Instantiate(placeableGo_clean, placeableT.position, placeableT.rotation);
            return;
        }

        if (placementButtonUp)
        {
            Place();
        }
    }

    void Place()
    {
        if (!entrySet)
        {
            Instantiate(placeableGo_clean, placeableT.position, placeableT.rotation);
            Stop();
            return;
        }

        Instantiate(placeableGo_clean, placeableT.position, placeableT.rotation).GetComponent<ItemProxy>().Set(entry.Item, quantity);

        PlayParticles();

        entry.Reduce(quantity);

        if (entry.Quantity <= 0)
            Stop();
    }

    void Stop()
    {
        if (placeableGO == null)
            return;

        Destroy(placeableGO);
        placeableGO = null;

        StopPlacing();
    }

    #endregion

    #region Colors & Particles

    void SetColors()
    {
        if (canPlace)
            placeableMaterial.color = ValidPlacementColor;
        else
            placeableMaterial.color = InvalidPlacementColor;
    }

    void PlayParticles()
    {
        placingParticles.transform.position = placeableT.position;
        placingParticles.Play();
    }

    #endregion

    #region Get References

    void GetCamera(GameObject camera)
    {
        this.camera = camera.GetComponent<Camera>();
    }

    void GetPlayer(GameObject player)
    {
        this.player = player.transform;
    }

    #endregion

    #region Input Buttons

    void GetPlacementButton()
    {
        placementButton = true;
    }

    void GetPlacementButtonDown()
    {
        placementButtonDown = true;
    }

    void GetPlacementButtonUp()
    {
        placementButtonUp = true;
    }

    void ResetButtons()
    {
        placementButton = false;
        placementButtonDown = false;
        placementButtonUp = false;
    }

    #endregion

}
