using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class Character : MonoBehaviour {

    public List<WearableItem> wornItems = new List<WearableItem>();
    public WearableItemDatabase wearableItemDatabase;

    private void Start()
    {
        if (wearableItemDatabase != null)
            wearableItemDatabase = Resources.Load<WearableItemDatabase>("WearableItemDatabase");
    }

    public int? EquipItem(int itemId)
    {
        WearableItem item = wearableItemDatabase.wearableItemDatabase[itemId];
        int? unequippedItem = UnequipItem(item.wearableType);
        wornItems.Add(item);
        UpdateStats();
        return unequippedItem;
    }

    public int? UnequipItem(WearableItemType itemType)
    {
        WearableItem item = wornItems.First(x => x.wearableType == itemType);
        if (item != null)
        {
            int itemId = item.id;
            wornItems.Remove(item);
            UpdateStats();
            return itemId;
        }

        //not wearing an item in that slot
        return null;
    }

    public void UpdateStats()
    {

    }
}
