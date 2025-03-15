using UnityEngine;

[System.Serializable]
public struct StartingInventoryItem
{
    public Item Item;
    [Min(0)] public int MinQuantity;
    [Min(1)] public int MaxQuantity;
}

public class StartingInventoryGenerator : MonoBehaviour
{
    [SerializeField] private StartingInventoryItem[] _startingItems;
    [SerializeField] private BonusItem[] _bonusItems;
    [Range(0f, 1f)] [SerializeField] private float _bonusItemDropChance = 0.25f;
    private InventoryManager _inventoryManager;

    private void Start()
    {
        GenerateStartingInventory();
    }
    
    public void Initialize(InventoryManager inventoryManager)
    {
        _inventoryManager = inventoryManager;
    }

    private void GenerateStartingInventory()
    {
        foreach (var startingItem in _startingItems)
        {
            if (startingItem.Item == null || startingItem.MinQuantity > startingItem.MaxQuantity) continue;

            int quantity = Random.Range(startingItem.MinQuantity, startingItem.MaxQuantity + 1);
            if (quantity > 0)
            {
                _inventoryManager.AddItem(startingItem.Item, quantity);
            }
        }

        foreach (var bonusItem in _bonusItems)
        {
            if (bonusItem != null && Random.value <= _bonusItemDropChance)
            {
                _inventoryManager.AddBonusItem(bonusItem);
            }
        }
    }
}