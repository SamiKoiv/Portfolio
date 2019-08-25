using UnityEngine;

namespace InventorySystem
{
    [System.Serializable]
    public class InventoryEntry
    {
        public int Index;
        public Item Item;
        public int Quantity;
        public Inventory Owner;

        public InventoryEntry(Item item, int quantity, Inventory owner)
        {
            Index = -1;
            Item = item;
            Quantity = quantity;
            Owner = owner;
        }

        public InventoryEntry(Item item, int quantity, Inventory owner, int index)
        {
            Index = index;
            Item = item;
            Quantity = quantity;
            Owner = owner;
        }

        public bool MatchesWith(Item item)
        {
            if (item == Item)
                return true;
            else
                return false;
        }

        public void Empty()
        {
            Item = null;
            Quantity = 0;
            Owner = null;
        }

        public void Add()
        {

        }

        public void Reduce(int quantity)
        {
            if (Owner != null)
                Owner.ReduceFromIndex(Item, Index, quantity);
        }

        public string ItemName
        {
            get
            {
                return Item.ItemName;
            }
        }

        public string Description
        {
            get
            {
                return Item.Description;
            }
        }

        public int Price
        {
            get
            {
                return Item.Price;
            }
        }

        public bool Stacks
        {
            get
            {
                return Item.Stacks();
            }
        }

        public GameObject WorldPrefab
        {
            get
            {
                return Item.WorldPrefab;
            }
        }

        public Sprite Icon
        {
            get
            {
                return Item.Icon;
            }
        }
    }
}