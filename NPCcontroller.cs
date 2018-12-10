using UnityEngine;
using System.Collections;
using UnityEngine.AI;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine.Serialization;

//This script needs to br dropped on an empty gameobject and made into a prefab.
//Then when making new NPC's. Copy and edit the prefab to make all NPC's with standard editable characteristics

//Dervies from Interactable script
public class NPCcontroller : Interactable
{
    //reference to the ScriptableObject that holds the Players Inventory
    public InventoryItemList inventoryItemList;
    public Quest quest;

    //Public reference to the NPC scriptable object created for this NPC
    public NPC npc;

    //This will be the panel brought up for dialogue between player and NPC
    public GameObject dialoguePanelUI;

    public GameObject dialoguePanel;

    // This gives the name of the node to talk to when running dialogue. This essentially links to the dialogue script in the dialogue runner
    //Currently this is all stored in one dialogue file assigned in the dialogue runner
    [FormerlySerializedAs("startNode")]
    public string talkToNode = "";

    public Text nameText;

    public InventoryItem giveItem;

    public GameObject dialogueRunner;

    // Use this for initialization
    void Start()
    {
        if (quest != null)
        {
            quest.activeQuest = false;
            quest.goalsCompleted = 0;
        }
    }

    //Over-rides the base Interact method with DialogueStart()
    public override void Interact()
    {
        base.Interact();
        DialogueStart();
    }

    //Starts the dialogue with the player character
    void DialogueStart()
    {
        Debug.Log("Speaking to: " + npc.npcName);

        // Need to link this to the dialogue interaction with a condition
        // brings up the dialoguepanelUI
        dialoguePanelUI.SetActive(true);
        dialoguePanel.GetComponent<Yarn.Unity.DialogueRunner>().StartDialogue(talkToNode);
    }

    //This is a Yarn command to be used in Yarn
    //This works perfectly to give the player the item assigned in the inspector
    [Yarn.Unity.YarnCommand("give")] 
    public void GiveItem()
    {
        //gives an item to the player
        inventoryItemList.itemList.Add(giveItem);
    }

    //This ia a Yarn command to chnage the name of the character talking using the Yarn script
    [Yarn.Unity.YarnCommand("name")]
    public void NameChange(string characterName)
    {
        //Changes the name of the name box in the dialogue UI
        nameText.text = characterName;
    }

    // This is a Yarn command to assign quests to the character using the Yarn script
    [Yarn.Unity.YarnCommand("questassign")]
    public void AssignQuest()
    {
        //This will assign the quest if conditions are met.
        //Conditions. Player is in collider, this quest is not currently active, and one conversational que not yet included
        if (quest != null && quest.activeQuest == false)
        {
            quest.activeQuest = true;
        }
    }

    // This is a Yarn Command to check for quest items
    // Link it to a choice. If the player says "Yes" then run check to look for all items.
    // If they say no, then don't run check
    [Yarn.Unity.YarnCommand("questcheck")]
    public void CheckQuest()
    {
        if (quest != null && quest.activeQuest == true)
        {
            //Checks to see if that quest goal is active
            if (quest.goalOneActive == true && quest != null)
            {
                //Searches for the quest goal one item in the player inventory
                if (inventoryItemList.itemList.Contains(quest.goalOne.questItem))
                {
                    //sets the goal to achieved
                    quest.goalOne.goalAchieved = true;
                    //Increases the number of completed goals
                    quest.goalsCompleted += 1;
                    //Logs that the goal is complete
                    Debug.Log("Goal One Complete");
                }
            }
            if (quest.goalTwoActive == true && quest != null)
            {
                if (inventoryItemList.itemList.Contains(quest.goalTwo.questItem))
                {
                    quest.goalTwo.goalAchieved = true;
                    quest.goalsCompleted += 1;
                    Debug.Log("Goal Two Complete");
                }
            }
            if (quest.goalThreeActive == true && quest != null)
            {
                if (inventoryItemList.itemList.Contains(quest.goalThree.questItem))
                {
                    quest.goalThree.goalAchieved = true;
                    quest.goalsCompleted += 1;
                    Debug.Log("Goal Three Complete");
                }
            }
            if (quest.goalFourActive == true && quest != null)
            {
                if (inventoryItemList.itemList.Contains(quest.goalFour.questItem))
                {
                    quest.goalFour.goalAchieved = true;
                    quest.goalsCompleted += 1;
                    Debug.Log("Goal Four Complete");
                }
            }
            if (quest.goalFiveActive == true && quest != null)
            {
                if (inventoryItemList.itemList.Contains(quest.goalFive.questItem))
                {
                    quest.goalFive.goalAchieved = true;
                    quest.goalsCompleted += 1;
                    Debug.Log("Goal Five Complete");
                }
            }
        }

        // Can this bit be written with a function? Something like this?
        /// public void removeGoalItem(Quest)
        /// {
        ///     inventoryItemList.itemList.Remove(questItem);
        ///     goalActive = false;
        /// }
        /// 
        /// To run then use:
        /// if (quest.goalOneActive == true)
        /// {
        ///     removeGoalItem(quest.goalOne);
        /// }

        if (quest != null && quest.numberOfGoals <= quest.goalsCompleted)
        {
            //Set quest as complete
            quest.giveRewardItem = true;
            Debug.Log("Reward Given");
            //Deletes the items from the players Inventory
            if (quest.goalOneActive == true)
            {
                inventoryItemList.itemList.Remove(quest.goalOne.questItem);
                //Inactivates the goal
                quest.goalOne.goalActive = false;
            }
            if (quest.goalTwoActive == true)
            {
                inventoryItemList.itemList.Remove(quest.goalTwo.questItem);
                quest.goalTwo.goalActive = false;
            }
            if (quest.goalThreeActive == true)
            {
                inventoryItemList.itemList.Remove(quest.goalThree.questItem);
                quest.goalThree.goalActive = false;
            }
            if (quest.goalFourActive == true)
            {
                inventoryItemList.itemList.Remove(quest.goalFour.questItem);
                quest.goalFour.goalActive = false;
            }
            if (quest.goalFiveActive == true)
            {
                inventoryItemList.itemList.Remove(quest.goalFive.questItem);
                quest.goalFive.goalActive = false;
            }
        }

        //If the quest is complete
        if (quest != null && quest.giveRewardItem == true)
        {
            //Give item one to player inventory
            inventoryItemList.itemList.Add(quest.rewardItem);
            quest.giveRewardItem = false;
            quest.activeQuest = false;
            quest.goalsCompleted = 0;
            Debug.Log("Quest Complete");

            //Sets the quest complete variable within Yarn and progresses the conversation
            VariableStorage varStore = dialogueRunner.GetComponent<VariableStorage>();
            var questSet = new Yarn.Value(true);
            varStore.SetValue("$quest_complete", questSet);
        }
    }


    /// Setting Yarn variables in Yarn spinner using <<set $Yarn_Variable to true>>
    /// The most useful variables for Yarn are Booleans.
    /// These can be changed to record interactions with the rest of the game


    /*To read or "get" a Yarn variable via Unity C#, use "GetValue" on the Variable Storage component:

    int myNumber = GetComponent<ExampleVariableStorage>().GetValue("$YarnVar").AsNumber;
    string myStr = GetComponent<ExampleVariableStorage>().GetValue("$YarnVar").AsString;

    To write or "set" a Yarn variable via Unity C#, create a Yarn.Value and then assign it on the Variable Storage component with "SetValue":

            dialogue runner needs to be assigned in the inspector
            VariableStorage varStore = dialogueRunner.GetComponent<VariableStorage>();
            var questSet = new Yarn.Value(true);
            varStore.SetValue("$quest_complete", questSet);
     * 
     */


}
