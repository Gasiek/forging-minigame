using System.Collections.Generic;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    [SerializeField] private InventorySlot[] _inventorySlots;
    [SerializeField] private InventorySlot[] _bonusSlots;
    [SerializeField] private List<BonusUIElement> _bonusUIElements;

    private void Awake()
    {
        for (int i = 0; i < _inventorySlots.Length; i++)
        {
            _inventorySlots[i].Initialize(this, i);
        }

        for (int i = 0; i < _bonusSlots.Length; i++)
        {
            _bonusSlots[i].Initialize(this, i);
        }
    }

    public void AddItem(Item item, int quantity)
    {
        foreach (var slot in _inventorySlots)
        {
            if (slot.Item == item)
            {
                slot.AddQuantity(quantity);
                return;
            }
        }

        foreach (var slot in _inventorySlots)
        {
            if (slot.IsEmpty)
            {
                slot.SetItem(item, quantity);
                return;
            }
        }

        Debug.LogWarning("Inventory full! Couldn’t add item.");
    }

    public void AddBonusItem(BonusItem bonusItem)
    {
        for (int i = 0; i < _bonusSlots.Length; i++)
        {
            if(_bonusSlots[i].IsEmpty) { continue; }
            if (_bonusSlots[i].Item.ItemName == bonusItem.ItemName) return;
        }

        for (int i = 0; i < _bonusSlots.Length; i++)
        {
            if (_bonusSlots[i].IsEmpty)
            {
                _bonusUIElements[i].UpdateUI(bonusItem.ItemName, bonusItem.Description);
                _bonusSlots[i].SetItem(bonusItem, 1);
                return;
            }
        }
    }

    public void RemoveItem(Item item, int quantity)
    {
        foreach (var slot in _inventorySlots)
        {
            if (slot.Item == item)
            {
                slot.RemoveQuantity(quantity);
                if (slot.Quantity <= 0)
                {
                    slot.Clear();
                }

                return;
            }
        }
    }

    public void SwapSlots(int fromIndex, int toIndex)
    {
        var fromSlot = _inventorySlots[fromIndex];
        var toSlot = _inventorySlots[toIndex];
        var tempItem = fromSlot.Item;
        var tempQuantity = fromSlot.Quantity;

        fromSlot.SetItem(toSlot.Item, toSlot.Quantity);
        toSlot.SetItem(tempItem, tempQuantity);

        fromSlot.UpdateUI();
        toSlot.UpdateUI();
    }

    public InventorySlot GetSlot(int index)
    {
        return _inventorySlots[index];
    }

    public List<BonusItem> GetBonusItems()
    {
        List<BonusItem> bonusItems = new List<BonusItem>();
        foreach (var slot in _bonusSlots)
        {
            if (slot.Item == null) continue;
            bonusItems.Add(slot.Item as BonusItem);
        }

        return bonusItems;
    }
}