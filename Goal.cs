using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Goal", menuName = "Quests/Goal", order = 1)]

public class Goal : ScriptableObject
{
    //This is the generic Goal scriptable object.
    //The goal could be an item to collect
    //Could be a conversation outcome to achieve
    public string goalName = "Goal Name";
    public string goalDescription = "Goal Description";
    public bool goalActive = false;
    public bool goalAchieved = false;

    public InventoryItem questItem;

}

