using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class Character : MonoBehaviour {

    public WearableItem weapon = null;
    public WearableItem armor = null;
    public WearableItem shield = null;
    public WearableItem jewelry = null;


    public WearableItemDatabase wearableDB;

    private void Start()
    {
        if (wearableDB != null)
            wearableDB = Resources.Load<WearableItemDatabase>("WearableItemDatabase");
    }

    public int? EquipItem(int itemId)
    {
        WearableItem item = wearableDB.wearableItemDatabase.First(x => x.id == itemId);
        int? unequippedItem = null;

        switch (item.wearableType)
        {
            case WearableItemType.Weapon:
                unequippedItem = UnequipItem(item.wearableType);
                weapon = item;
                break;

            case WearableItemType.Armor:
                unequippedItem = UnequipItem(item.wearableType);
                armor = item;
                break;

            case WearableItemType.Shield:
                unequippedItem = UnequipItem(item.wearableType);
                shield = item;
                break;

            case WearableItemType.Jewelry:
                unequippedItem = UnequipItem(item.wearableType);
                jewelry = item;
                break;

            default:
                break;
        }

        UpdateStats();

        return unequippedItem;
    }

    public int? UnequipItem(WearableItemType itemType)
    {
        int? itemId = null;

        switch (itemType)
        {
            case WearableItemType.Weapon:
                itemId = weapon.id;
                weapon = null;
                break;

            case WearableItemType.Armor:
                itemId = armor.id;
                armor = null;
                break;

            case WearableItemType.Shield:
                itemId = shield.id;
                shield = null;
                break;

            case WearableItemType.Jewelry:
                itemId = jewelry.id;
                jewelry = null;
                break;

            default:
                break;
        }

        UpdateStats();

        return itemId;
    }

    public void UpdateStats()
    {

    }
}
