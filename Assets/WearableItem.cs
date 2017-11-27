using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class WearableItem {

    //wearable slot
    public WearableItemType wearableType;
    public string itemName;
    public int id;

    //base stats
    public int accuracy;
    public int strength;
    public int defence;
    public int health;

    //global modifiers
    public float accuracyMod;
    public float strengthMod;
    public float defenceMod;
    public float healthMod;

    public WearableItem()
    {
        wearableType = WearableItemType.Weapon;
        itemName = "Some Wearable";
        id = 0;

        accuracy = 0;
        strength = 0;
        defence = 0;
        health = 0;

        accuracyMod = 1f;
        strengthMod = 1f;
        defenceMod = 1f;
        healthMod = 1f;
    }
}

public enum WearableItemType { Weapon, Armor, Shield, Jewelry}
