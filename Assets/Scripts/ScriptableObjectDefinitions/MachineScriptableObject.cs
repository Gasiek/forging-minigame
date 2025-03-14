using UnityEngine;

[CreateAssetMenu(fileName = "MachineScriptableObject", menuName = "ForgingGame/Machine")]
public class MachineScriptableObject : ScriptableObject
{
    public string MachineName;
    public string ActionName;
    public int NumberOfCraftingSlots;
}
