using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "New QuestList", menuName = "Quests/QuestList", order = 1)]
public class QuestList : ScriptableObject
{
    public List<Quest> questList;
}
