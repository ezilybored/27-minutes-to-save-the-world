using UnityEngine;
using System.Collections;

[System.Serializable]                                                           //  Our Representation of an InventoryItem
public class InventoryItem : ScriptableObject
{
    new public string name = "New Item";

    public InventoryItemList inventoryItemList;

    //This has no use at present but I want this to be able to be passed into the interact panel text
    //So that each item has its own custom interact text.
    //Maybe use the dialogue system from the GameGrind tutorials
    public string description = "Item Description";

    public Sprite icon = null;              // Item icon
    public bool isDefaultItem = false;
    public bool showInInventory = true;     //Item is in Inventory?

    //This allows the item to be used
    //It is virtual so that it can be over-ridden by the actual item itself
    public virtual void Use()
    {
        //Some use for the item will be implemented here
        Debug.Log("Using the " + name);
    }

    // Call this method to remove the item from inventory
    public void RemoveFromInventory()
    {
        inventoryItemList.itemList.Remove(this);
    }
}
