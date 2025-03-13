using UnityEngine;

[CreateAssetMenu(fileName = "New Quest", menuName = "ForgingGame/Quest")]
public class Quest : ScriptableObject
{
    public string QuestName;
    public Item RequiredItem;
    public int RequiredAmount;
    public int CurrentProgress;
    public MachineType MachineToUnlock;
}