using UnityEngine;

[CreateAssetMenu(fileName = "New Quest", menuName = "ForgingGame/Quest")]
public class Quest : ScriptableObject
{
    public string QuestName;
    public string QuestDescription;
    public Item RequiredItem;
    public int RequiredAmount;
    public int CurrentProgress;
    public MachineType MachineToUnlock;
}