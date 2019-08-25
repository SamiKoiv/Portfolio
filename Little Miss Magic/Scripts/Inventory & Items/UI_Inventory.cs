using InventorySystem;
using UnityEngine;
using Unity.Burst;
using Unity.Jobs;

public class UI_Inventory : MonoBehaviour
{
    [SerializeField] RectTransform contents;
    [SerializeField] GameObject entryTemplate;

    UI_ItemProxy[] itemProxys;
    Inventory inventory;

    public void BindToInventory(Inventory inventory)
    {
        this.inventory = inventory;
        inventory.OnEntryUpdated += SetItem;
        Build();
    }

    void Build()
    {
        if (itemProxys != null)
            ClearView();

        itemProxys = new UI_ItemProxy[inventory.Capacity];

        contents.sizeDelta = new Vector2(contents.sizeDelta.x, entryTemplate.GetComponent<RectTransform>().sizeDelta.y * inventory.Capacity);

        for (int i = 0; i < inventory.ItemEntries.Length; i++)
        {
            itemProxys[i] = Instantiate(entryTemplate, contents).GetComponent<UI_ItemProxy>();
        }

        SetItems();
    }

    void SetItem(int index)
    {
        itemProxys[index].Set(inventory, index);
    }

    public void SetItems()
    {
        if (itemProxys[0] == null)
            Build();

        for (int i = 0; i < inventory.ItemEntries.Length; i++)
        {
            SetItem(i);
        }
    }

    void ClearView()
    {
        foreach(UI_ItemProxy ip in itemProxys)
        {
            Destroy(ip.gameObject);
        }
    }
}
