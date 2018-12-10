using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//This is dropped on the InventoryUI parent GameObject

//This now works again. But the remove button does not work

public class InventoryUI : MonoBehaviour {

	//references the ItemsParent parent object
	public Transform itemsParent;

	//references the whole Inventory UI. Assigned in the inspector
	public GameObject inventoryUI;

	//references the inventory. Assigned in the inspector.
	public InventoryItemList inventoryItemList;

	//creates an array of InventorySlot called slot
	InventorySlot[] slots;

    public int inventoryCount;


    //Start is called every time the Game Object is enabled. So therefore every time the Inventory is hidden or viewed
	void Start () 
	{

	}
	

	void Update () 
	{
		//If the button to activate the inventory is pressed
		if (Input.GetButtonDown ("Inventory")) 
		{
			//reverses the SetActive property of the inventoryUI
			inventoryUI.SetActive (!inventoryUI.activeSelf);
            UpdateUI();
		}
	}

	public void UpdateUI()
	{
        //check to see if the inventory has been added to or updated
        if (inventoryItemList.itemList.Count != inventoryCount)
        {
            //sets the array equal to the children of ItemsParent that are of type InventorySlot
            //This only works with a static number of slots.
            //Further work required to have a dynamic inventory
            slots = itemsParent.GetComponentsInChildren<InventorySlot>();

            //Search for all inventory slots. This is easy as they are all children of ItemsParent
            //all children can be found by referencing ItemsParent

            //a for loop to loop through the slots
            for (int i = 0; i < slots.Length; i++)
            {
                //If there is an available slot
                if (i < inventoryItemList.itemList.Count)
                {
                    slots[i].AddItem(inventoryItemList.itemList[i]);
                }
                else
                {
                    slots[i].ClearSlot();
                }
            }

            inventoryCount = inventoryItemList.itemList.Count;
        }

	}
}
