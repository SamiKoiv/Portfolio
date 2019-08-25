using UnityEngine;

namespace InventorySystem
{
    public class Inventory : MonoBehaviour
    {
        #region Variables

        public int Capacity;
        public bool AutoSort;

        public delegate void IntEvent(int i);
        public event IntEvent OnEntryUpdated;

        public delegate void ItemEvent(Item item);
        public event ItemEvent OnItemAdded;
        public event ItemEvent OnItemReduced;

        public InventoryEntry[] itemEntries;
        public InventoryEntry[] ItemEntries
        {
            get
            {
                return itemEntries;
            }
        }

        bool dirty;

        bool isFull;
        public bool IsFull
        {
            get
            {
                if (dirty)
                    CheckForFull();

                return isFull;
            }
        }

        #endregion

        #region Initialization

        virtual protected void Awake()
        {
            if (Capacity <= 0)
                Capacity = 1;

            InitializeEntries(Capacity);
        }

        protected void InitializeEntries(int capacity)
        {
            itemEntries = new InventoryEntry[capacity];
        }

        #endregion

        #region Add

        public void Add(Item item, int quantity)
        {
            if (item == null)
            {
                Debug.Log("Rejected null item from inventory.");
                return;
            }

            Debug.Log("Adding " + item.ItemName + " to Player's Inventory");

            dirty = true;

            int firstEmpty = -1;

            for (int i = 0; i <= itemEntries.Length; i++)
            {

                // This section is reached if no entries are found. i is outside of array range so giving 'return' command is critical.
                if (i == itemEntries.Length)
                {
                    if (firstEmpty != -1)
                    {
                        itemEntries[firstEmpty] = new InventoryEntry(item, quantity, this, firstEmpty);
                        EntryUpdated(firstEmpty);

                        if (OnItemAdded != null)
                            OnItemAdded(item);

                        Debug.Log("Made new inventory entry for " + item.ItemName + ".");
                    }
                    else
                    {
                        // Inventory Full!
                        EventSystem.InventoryEvents.InventoryFull(item);
                    }
                    return;
                }

                // Take note of the first free spot in case no previous entry with given item is found.
                if (firstEmpty == -1)
                {
                    if (IndexIsEmpty(i))
                    {
                        firstEmpty = i;
                        continue;
                    }
                }

                // If entry is found, simply add to the entry quantity.
                if (item.MatchesWith(itemEntries[i]) && item.Stacks())
                {
                    ItemEntries[i].Quantity += quantity;
                    EntryUpdated(i);

                    if (OnItemAdded != null)
                        OnItemAdded(item);

                    return;
                }
            }
        }

        #endregion

        #region Add To Index

        public void AddToIndex(Item item, int index, int quantity)
        {
            if (IndexIsEmpty(index) || !itemEntries[index].MatchesWith(item))
                return;

            itemEntries[index].Quantity += quantity;
            EntryUpdated(index);
        }

        #endregion

        #region Get Amount

        public int GetAmount(Item item)
        {
            if (item == null)
                return 0;

            int detected = 0;

            for (int i = 0; i < itemEntries.Length; i++)
            {
                // If entry is found, reduce from the entry quantity.
                if (!IndexIsEmpty(i) && itemEntries[i].Item == item)
                {
                    detected += itemEntries[i].Quantity;
                }
            }

            return detected;
        }

        #endregion

        #region Reduce

        // This function removes items ONLY if asked quantity is met. As a precaution, use Contains check for valid chance of removal.
        public void Reduce(Item item, int quantity)
        {
            if (item == null)
            {
                Debug.Log("Tried to remove nonexistent item from inventory.");
                return;
            }

            dirty = true;

            // Variables used for processing portioned item entries.
            int[] detectedEntries = new int[Capacity];
            int detectionIndex = 0;
            int detectedQuantity = 0;

            detectedEntries[0] = -1;

            for (int i = 0; i <= itemEntries.Length; i++)
            {
                // Entry is found.
                if (itemEntries[i].MatchesWith(item))
                {
                    if (itemEntries[i].Quantity > quantity)
                    {
                        // Entry has more than sufficient amount. Reduce from the entry and return.
                        itemEntries[i].Quantity -= quantity;

                        if (OnItemReduced != null)
                            OnItemReduced(item);

                        EntryUpdated(i);
                        return;
                    }
                    else if (itemEntries[i].Quantity == quantity)
                    {
                        // Entry has exactly sufficient amount. Remove the entry and return.
                        RemoveIndex(i);

                        if (OnItemReduced != null)
                            OnItemReduced(item);

                        return;
                    }
                    else
                    {
                        // Entry has less than sufficient amount. Log the index in case the item has multiple portions.
                        detectedEntries[detectionIndex] = i;
                        detectedQuantity += itemEntries[i].Quantity;
                    }
                }
            }

            // Sufficient amount found in multiple portions.
            if (detectedQuantity >= quantity)
            {
                int amountReduced = 0;
                int portion = 0;

                for (int i = 0; i < detectedEntries.Length; i++)
                {
                    portion = Mathf.Min(itemEntries[detectedEntries[i]].Quantity, quantity - amountReduced);

                    // Initial entries should be smaller than required amount, thereby removed until requirement has been satisfied.
                    if (itemEntries[detectedEntries[i]].Quantity == portion)
                    {
                        RemoveIndex(detectedEntries[i]);
                    }
                    else
                    {
                        itemEntries[detectedEntries[i]].Quantity -= portion;
                        EntryUpdated(i);
                    }

                    amountReduced += portion;

                    if (amountReduced == quantity)
                        return;
                }

                if (OnItemReduced != null)
                    OnItemReduced(item);
            }
        }

        #endregion

        #region RemoveIndex & ReduceFromIndex

        void RemoveIndex(int index)
        {
            if (IndexIsEmpty(index))
                return;

            itemEntries[index].Empty();
            itemEntries[index] = null;

            if (AutoSort)
                Sort();
            else
                EntryUpdated(index);
        }

        public void ReduceFromIndex(Item item, int index, int quantity)
        {
            if (IndexIsEmpty(index) || !itemEntries[index].MatchesWith(item))
                return;

            if (itemEntries[index].Quantity > quantity)
            {
                itemEntries[index].Quantity -= quantity;
            }
            else if (itemEntries[index].Quantity == quantity)
            {
                itemEntries[index].Empty();
                itemEntries[index] = null;
            }

            if (AutoSort)
                Sort();
            else
                EntryUpdated(index);
        }

        #endregion

        //----------------------------------------------------------------------------------------------

        void CheckForFull()
        {
            for (int i = 0; i < itemEntries.Length; i++)
            {
                if (IndexIsEmpty(i))
                {
                    isFull = false;
                }
            }

            isFull = true;
            dirty = false;
        }

        bool IndexIsEmpty(int index)
        {
            if (itemEntries[index] == null)
                return true;
            else
                return false;
        }

        void Sort()
        {
            InventoryEntry[] temp = new InventoryEntry[itemEntries.Length];

            int tempIndex = 0;

            for (int i = 0; i < temp.Length; i++)
            {
                if (!IndexIsEmpty(i))
                {
                    temp[tempIndex] = itemEntries[i];
                    tempIndex++;
                }
            }

            InventoryEntry[] oldEntries = itemEntries;
            itemEntries = temp;

            for (int i = 0; i < itemEntries.Length; i++)
            {
                if (itemEntries[i] != null)
                    itemEntries[i].Index = i;

                EntryUpdated(i);
            }
        }

        void EntryUpdated(int i)
        {
            if (OnEntryUpdated != null)
                OnEntryUpdated(i);
        }
    }
}
