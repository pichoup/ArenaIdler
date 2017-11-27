using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WearableItemInventory : MonoBehaviour {

    //key-value pair for id of item, and amount of item
    public Dictionary<int, int> inventoryItems;
    public int inventorySize;
    public Character character;
    public WearableItemDatabase wearableDB;

    void Start()
    {
        if (wearableDB != null)
            wearableDB = Resources.Load<WearableItemDatabase>("WearableItemList");

        inventoryItems = new Dictionary<int, int>();

        for (int i = 0; i < wearableDB.wearableItemDatabase.Count; i++)
        {
            inventoryItems.Add(wearableDB.wearableItemDatabase[i].id, 0);
        }
    }

    public void EquipItemFromInventory(int itemId)
    {
        if (inventoryItems[itemId] > 0)
        {
            inventoryItems[itemId] -= 1;
            AddItemToInventory(character.EquipItem(itemId));
        }

    }


    public void AddItemToInventory(int? wearableItemId, int amount = 1)
    {
        //check inventory space at some point

        if (wearableItemId != 0)
        {
            inventoryItems[(int)wearableItemId] += amount;
        }
    }

    public void RemoveItemFromInventory(int wearableItemId, int amount = 1)
    {

    }
}
