using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "New NPC", menuName = "NPC", order = 1)]
public class NPC : ScriptableObject
{
    // NPC characteristics here

    public string npcName = "New NPC";

    public string description = "Item Description";

    // These are all that is needed for now.
    // Could possibly add the text for conversations here too.
    // Could possibly also add the quests here.
}
