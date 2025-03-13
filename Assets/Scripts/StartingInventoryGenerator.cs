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
    [SerializeField] private InventorySlot[] _inventorySlots;
    [SerializeField] private StartingInventoryItem[] _startingItems;
    [SerializeField] private Item[] _bonusItems;
    [Range(0f, 1f)] [SerializeField] private float _bonusItemDropChance = 0.25f;

    private void Start()
    {
        GenerateStartingInventory();
    }

    private void GenerateStartingInventory()
    {
        int slotIndex = 0;

        foreach (var startingItem in _startingItems)
        {
            if (startingItem.Item == null || startingItem.MinQuantity > startingItem.MaxQuantity) continue;

            int quantity = Random.Range(startingItem.MinQuantity, startingItem.MaxQuantity + 1);
            if (quantity > 0)
            {
                AddToInventory(startingItem.Item, quantity, ref slotIndex);
            }
        }

        foreach (var bonusItem in _bonusItems)
        {
            if (bonusItem != null && Random.value <= _bonusItemDropChance)
            {
                AddToInventory(bonusItem, 1, ref slotIndex);
            }
        }
    }

    private void AddToInventory(Item item, int quantity, ref int slotIndex)
    {
        if (slotIndex >= _inventorySlots.Length)
        {
            Debug.LogWarning("Not enough inventory slots for starting items!");
            return;
        }

        _inventorySlots[slotIndex].CurrentItem = item;
        _inventorySlots[slotIndex].Quantity = quantity;
        _inventorySlots[slotIndex].SlotImage.sprite = item.Icon;
        slotIndex++;
    }
}