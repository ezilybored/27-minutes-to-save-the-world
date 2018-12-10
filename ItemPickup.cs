using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//This script is dropped on each item in the game
//Create a prefab empty gameobject. Save it with this script. 
//When new item scriptable objects are created with the editor a new prefab can be copied and edited to the new SO

//Dervies from Interactable script
public class ItemPickup : Interactable {

    //This is a public reference to the scriptable object (from the new Inventory system) of the same name.
    //This is assigned in the inspector
    public InventoryItem item;

    //This references the Interact panel UI so that it can be closed when the item is picked up
    //This needs to be dragged and dropped onto the item in the inspector
    public GameObject interactPanelUI;

    public InventoryItemList inventoryItemList;


    void Start()
    {

    }

    public override void Interact()
    {
        base.Interact();
        PickUp();
    }

    void PickUp()
    {
        Debug.Log("Picking up " + item.name);

        //adds the item. checks to see if item was added. true if yes, false if no
        inventoryItemList.itemList.Add(item);
        bool wasPickedUp = true;


        if (wasPickedUp)
        {
            Destroy(gameObject);
            //closes the interact panel UI
            interactPanelUI.SetActive(false);
        }
    }

}