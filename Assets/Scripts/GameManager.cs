using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private InventoryManager _inventoryManager;
    [SerializeField] private MachineController[] _machines;

    private void Awake()
    {
        if (_inventoryManager == null)
        {
            Debug.LogError("InventoryManager is not assigned in GameManager!");
            return;
        }

        if (_machines == null || _machines.Length == 0)
        {
            Debug.LogWarning("No machines assigned to GameManager.");
        }

        foreach (var machine in _machines)
        {
            machine.Initialize(_inventoryManager);
        }
    }
}