using UnityEngine;

[System.Serializable]
public class Objective_CollectItems : Objective
{
    [SerializeField] Item item;
    [SerializeField] int amount;

    bool sufficientAmount;

    public Objective_CollectItems(Item item, int amount)
    {
        this.item = item;
        this.amount = amount;
    }

    public override void Initialize()
    {
        PlayerInventory.Instance.OnItemAdded += CheckItems;
        PlayerInventory.Instance.OnItemReduced += CheckItems;
    }

    public override void Clean()
    {
        PlayerInventory.Instance.OnItemAdded -= CheckItems;
        PlayerInventory.Instance.OnItemReduced -= CheckItems;
    }

    public override string GetObjective()
    {
        return "Collect " + amount + " " + item.ItemName;
    }

    public override bool IsComplete()
    {
        if (PlayerInventory.Instance.GetAmount(item) >= amount)
            return true;
        else
            return false;
    }

    void CheckItems(Item item)
    {
        if (PlayerInventory.Instance.GetAmount(item) >= amount)
            sufficientAmount = true;
        else
            sufficientAmount = false;
    }
}
