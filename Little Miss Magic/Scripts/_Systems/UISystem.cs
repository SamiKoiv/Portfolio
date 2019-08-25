using InventorySystem;
using UnityEngine;

public class UISystem : MonoBehaviour
{
    static UISystem instance;
    public static UISystem Instance
    {
        get
        {
            return instance;
        }
    }
    private void Awake()
    {
        if (instance == null)
            instance = this;
        else if (instance != this)
        {
            Debug.Log("Multiple instances of UISystem in the scene. Destroying duplicates.");
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        InventoryOpen.Value = false;
        DescriptionOpen.Value = false;
        QuestLogOpen.Value = false;
    }

    private void OnEnable()
    {
        InputSystem.UI_OnToggleInventory += Inventory_Toggle;
        InputSystem.UI_OnToggleQuestLog += QuestLog_Toggle;
    }

    private void OnDisable()
    {
        InputSystem.UI_OnToggleInventory -= Inventory_Toggle;
        InputSystem.UI_OnToggleQuestLog += QuestLog_Toggle;
    }

    public delegate void VoidEvent();
    public delegate void InventoryEntryEvent(InventoryEntry entry);

    // Inventory Screen --------------------------------------------------------------

    [Header("Inventory")]
    [SerializeField] EventBool InventoryOpen;
    [SerializeField] EventBool DescriptionOpen;


    public void Inventory_Toggle()
    {
        InventoryOpen.Value = !InventoryOpen.Value;
    }

    public void Inventory_Open()
    {
        InventoryOpen.Value = true;
    }

    public void Inventory_Close()
    {
        InventoryOpen.Value = false;
    }

    // Description Screen --------------------------------------------------------------

    public static event InventoryEntryEvent OnDescription_Set;

    public void Description_Open()
    {
        DescriptionOpen.Value = true;
    }

    public void Description_Close()
    {
        DescriptionOpen.Value = false;
    }

    public void Description_Set(InventoryEntry entry)
    {
        if (OnDescription_Set != null)
            OnDescription_Set(entry);
    }

    // Quest Log --------------------------------------------------------------

    [SerializeField] EventBool QuestLogOpen;

    void QuestLog_Toggle()
    {
        QuestLogOpen.Value = !QuestLogOpen.Value;
    }
}
