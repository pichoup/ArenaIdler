using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class WearableItemInventory : MonoBehaviour {

    //key-value pair for id of item, and amount of item
    public Dictionary<int, int> inventoryItems;
    public int inventorySize;
    public Character character;
    public WearableItemDatabase wearableDB;
    public SimpleObjectPool buttonObjectPool;
    public Transform contentPanel;

    void Start()
    {
        if (wearableDB != null)
            wearableDB = Resources.Load<WearableItemDatabase>("WearableItemList");

        inventoryItems = new Dictionary<int, int>();

        for (int i = 0; i < wearableDB.wearableItemDatabase.Count; i++)
        {
            inventoryItems.Add(wearableDB.wearableItemDatabase[i].id, 0);
        }

        UpdateInventoryDisplay();
    }

    public void EquipItemFromInventory(int itemId)
    {
        if (inventoryItems[itemId] > 0)
        {
            AddItemToInventory(character.EquipItem(itemId));
            RemoveItemFromInventory(itemId);
        }

    }

    public void AddItemToInventory(int? wearableItemId, int amount = 1)
    {
        //check inventory space at some point

        if (wearableItemId != null)
        {
            inventoryItems[(int)wearableItemId] += amount;
        }
    }

    public void RemoveItemFromInventory(int? wearableItemId, int amount = 1)
    {
        if (wearableItemId != null)
        {
            inventoryItems[(int)wearableItemId] -= amount;
        }
    }

    private void UpdateInventoryDisplay()
    {
        RemoveInventoryItems();
        AddInventoryItems();
    }

    private void RemoveInventoryItems()
    {
        while (contentPanel.childCount > 0)
        {
            GameObject toRemove = transform.GetChild(0).gameObject;
            buttonObjectPool.ReturnObject(toRemove);
        }
    }

    private void AddInventoryItems()
    {
        foreach (int key in inventoryItems.Keys)
        {
            //if quantity > 0
            if (inventoryItems[key] > 0)
            {
                WearableItem item = wearableDB.wearableItemDatabase.First(x => x.id == key);

                GameObject newButton = buttonObjectPool.GetObject();
                newButton.transform.SetParent(contentPanel);

                InventoryItemButton itemButton = newButton.GetComponent<InventoryItemButton>();
                itemButton.Setup(item, this);
            }
        }
    }
}
