using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "New Quest", menuName = "Quests/Quest", order = 1)]
public class Quest : ScriptableObject
{
    //Maybe use a list of goals.
    //public List<Goal> questGoalList;

    //Or just use a posibility of 5 goals
    public Goal goalOne;
    public Goal goalTwo;
    public Goal goalThree;
    public Goal goalFour;
    public Goal goalFive;

    //Activating the goals
    public bool goalOneActive = false;
    public bool goalTwoActive = false;
    public bool goalThreeActive = false;
    public bool goalFourActive = false;
    public bool goalFiveActive = false;

    //Assigns the number of goals
    public int numberOfGoals = 0;

    //Tracks how many goals have been met
    public int goalsCompleted = 0;

    //Is the quest active?
    public bool activeQuest = false;

    //reference to the ScriptableObject that holds the Players Inventory
    //This is so that the rewardItem can be assigned
    public InventoryItemList inventoryItemList;

    //An item to give to the player when all Goals are completed
    public InventoryItem rewardItem;

    public bool giveRewardItem = false;

    /*
     * This may not be needed as the completed boolean can be accesed through the reference to each Goal above
    public bool goalOneComplete = false;
    public bool goalTwoComplete = false;
    public bool goalThreeComplete = false;
    public bool goalFourComplete = false;
    public bool goalFiveComplete = false;
    */
}
