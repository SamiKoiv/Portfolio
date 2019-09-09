using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ItemDatabase", menuName = "Data/ItemDatabase")]
public class ItemDatabase : ScriptableObject, IItemDatabase
{
    [SerializeField] Item[] items = new Item[0];

    public Item GetItem(int id)
    {
        return items[id];
    }
}
