using UnityEngine;
using System.Collections;
using UnityEngine.AI;
using UnityEngine.EventSystems;
using UnityEngine.UI;

//This script needs to be dropped onto the player GameObject in the inspector

//namespace Yarn.Unity.Example
//{
    public class PlayerMovementController : MonoBehaviour
    {
        //public Interactable class called focus
        public Interactable focus;

        //The players inventory assigned in the inspector
        public InventoryItemList inventoryItemList;

        //Used for facing the target
        //public Vector3 target;

        //reference to the main camera
        Camera cam;

        //This references the Interact panel UI
        public GameObject interactPanelUI;

        //This references the Dialogue panel UI
        public GameObject dialoguePanelUI;

        //These allow references to the item description and name fields of the Interaction UI
        // These fields are required on all interactable objects, items and NPCs
        public Text itemName;
        public Text itemDescription;


        public GameObject inventoryUI;

        //sets the goal for the NavMeshAgent
        private Vector3 goal;
        //Calls the NavMeshAgent
        private NavMeshAgent agent;

        void Start()
        {
            //This clears the players inventory when the player is initialised. This will ultimately need to be moved the game state controller
            inventoryItemList.itemList.Clear();

            //Cursor.visible = false;

            //Sets the goal as the transform.position of the player
            goal = transform.position;
            //gets the NavMeshComponent for the player
            agent = GetComponent<NavMeshAgent>();
        }

        void Update()
        {
            //freezes the player when the inventory is open
            //This will need to be added to all NPC and enemies etc
            if (inventoryUI.activeSelf == true)
            {
                return;
            }

            //freezes the player when the Dialogue panel is open
            //This will need to be added to all NPC and enemies etc
            if (dialoguePanelUI.activeSelf == true)
            {
                return;
            }

            //sets the goal for the NavMesh using the WASD keys
            goal = transform.position
            + Vector3.right * Input.GetAxis("Horizontal")
            + Vector3.forward * Input.GetAxis("Vertical");

            agent.destination = goal;

        }

        void OnTriggerEnter(Collider col)
        {
            //checks for the Interactable tag on the the object who's collider we just entered
            if (col.tag == "Interactable")
            {
                //Stores the interactable that has been entered as a new Interactable instance
                Interactable interactable = col.GetComponent<Interactable>();
                ItemPickup itemPickup = col.GetComponent<ItemPickup>();
                //Debug.Log ("This object is: " + col.gameObject.name);

                //used to set the transfrom Vector3 of the target collider
                //target = col.transform.position;

                itemName.text = itemPickup.item.name;
                itemDescription.text = itemPickup.item.description;

                //If we have hit an interactable, set it as the focus of the player
                if (interactable != null)
                {
                    //brings up the interact panel UI
                    interactPanelUI.SetActive(true);

                    SetFocus(interactable);
                    //FaceTarget ();
                }
            }

            //checks for the NPC tag on the the object who's collider we just entered
            if (col.tag == "NPC")
            {
                //Stores the NPC that has been entered as a new Interactable instance
                Interactable interactable = col.GetComponent<Interactable>();
                NPCcontroller npcController = col.GetComponent<NPCcontroller>();

                itemName.text = npcController.npc.npcName;
                itemDescription.text = npcController.npc.description;

                //If we have hit an interactable, set it as the focus of the player
                if (interactable != null)
                {
                    //brings up the interact panel UI
                    interactPanelUI.SetActive(true);

                    SetFocus(interactable);
                    //FaceTarget ();
                }
            }
        }

        void OnTriggerExit(Collider col)
        {
            {
                //closes the interact panel UI
                interactPanelUI.SetActive(false);

                RemoveFocus();
            }
        }

        //Sets the focus of the Player as a new Interactable called newFocus
        void SetFocus(Interactable newFocus)
        {
            //if the newFocus is not the focus
            if (newFocus != focus)
            {
                if (focus != null)
                    //then calls onDefocus from Interactable
                    focus.OnDefocused();

                //and sets the correct focus
                focus = newFocus;

            }

            focus = newFocus;
            //Calls OnFocused from Interactable
            newFocus.OnFocused(transform);

        }

        //Removes the focus of the player from the object
        void RemoveFocus()
        {
            if (focus != null)
                //Calls OnDefocused from Interactable
                focus.OnDefocused();
            focus = null;
        }

        //Sets the player to face the interactable target
        //Only set this when the interaction button has been pressed
        //set target to be the interacting object at some point
        /*void FaceTarget()
        {
            Vector3 direction = (target - transform.position).normalized;
            Quaternion lookRotation = Quaternion.LookRotation (new Vector3(direction.x, 0f, direction.z));
            transform.rotation = Quaternion.Slerp (transform.rotation, lookRotation, Time.deltaTime * 5f);
        }*/


    }
//}	