using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class InventoryItemButton : MonoBehaviour
{

    public Button buttonComponent;
    public Text itemStats;
    public Image iconImage;


    private WearableItem item;
    private WearableItemInventory scrollList;

    // Use this for initialization
    void Start()
    {
        buttonComponent.onClick.AddListener(HandleClick);
    }

    public void Setup(WearableItem currentItem, WearableItemInventory currentScrollList)
    {
        item = currentItem;
        //iconImage.sprite = item.icon;
        itemStats.text = "" ;
        scrollList = currentScrollList;

    }

    public void HandleClick()
    {
        scrollList.EquipItemFromInventory(item.id);
    }
}