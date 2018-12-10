using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//This is the script to control the slots on the inventory UI
//This is dropped on the InventorySlot prefab GameObject

public class InventorySlot : MonoBehaviour {

	//To set the icon for the Item in the inventory
	public Image icon;
	public Button removeButton;

	InventoryItem item;

    //references the inventory. Assigned in the inspector.
    public InventoryItemList inventoryItemList;

	public void AddItem(InventoryItem newItem)
	{
		//Sets the Item passed in to the method as the stored item
		item = newItem;

		//sets the icon of this item slot as the icon of the item passed in
		icon.sprite = item.icon;
		//enables the icon as this is disabled by default
		icon.enabled = true;

		removeButton.interactable = true;
	}

	public void ClearSlot()
	{
		item = null;

		icon.sprite = null;
		icon.enabled = false;

		removeButton.interactable = false;
	}

	//Actually removes the item from the player inventorywhen the button is pressed
    public void OnRemoveButton()
	{
        inventoryItemList.itemList.Remove(item);
	}

	//The ability to use an item for some purpose
	public void UseItem()
	{
		item.Use ();
	}
}
