using InventorySystem;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UI_ItemProxy : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler, IPointerDownHandler, IPointerUpHandler
{
    Inventory inventory;
    int index;

    InventoryEntry entry;

    [SerializeField] TextMeshProUGUI itemName;
    [SerializeField] TextMeshProUGUI quantity;
    [SerializeField] Image icon;
    [SerializeField] Image background;
    [SerializeField] Color highlightColor;
    [SerializeField] Color pressColor;
    [SerializeField] Color emptyColor;

    Color defaultColor;

    bool mouseOver;
    bool press;

    private void Awake()
    {
        defaultColor = background.color;
    }

    public void Set(Inventory inventory, int index)
    {
        this.inventory = inventory;
        this.index = index;
        entry = inventory.ItemEntries[index];

        if (entry == null)
        {
            icon.enabled = false;
            quantity.text = string.Empty;
            itemName.text = string.Empty;
            background.color = emptyColor;
            defaultColor = emptyColor;
            return;
        }

        icon.enabled = true;
        itemName.text = entry.ItemName;
        quantity.text = string.Empty + entry.Quantity;
        icon.sprite = entry.Icon;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (entry == null)
            return;

        UISystem.Instance.Description_Set(entry);
        UISystem.Instance.Description_Open();
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        // Pressed Color
        press = true;
        background.color = pressColor;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        // Default Color
        press = false;

        if (mouseOver)
            background.color = highlightColor;
        else
            background.color = defaultColor;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        // Highlight Color
        if (press)
            return;

        mouseOver = true;
        background.color = highlightColor;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        // Default Color
        if (press)
            return;

        mouseOver = false;
        background.color = defaultColor;
    }
}
