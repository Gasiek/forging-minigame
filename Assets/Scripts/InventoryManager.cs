using System.Collections.Generic;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    [SerializeField] private InventorySlot[] _inventorySlots;
    [SerializeField] private GameObject _bonusUIPrefab;
    [SerializeField] private Transform _bonusUIContainer;
    private List<BonusItem> _bonusItems = new List<BonusItem>();

    private void Awake()
    {
        for (int i = 0; i < _inventorySlots.Length; i++)
        {
            _inventorySlots[i].Initialize(this, i);
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
        GameObject bonusUI = Instantiate(_bonusUIPrefab, _bonusUIContainer);
        BonusUIElement bonusUIElement = bonusUI.GetComponent<BonusUIElement>();
        bonusUIElement.Initialize(bonusItem);

        if (!_bonusItems.Exists(b => b.ItemName == bonusItem.ItemName))
        {
            _bonusItems.Add(bonusItem);
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
        return new List<BonusItem>(_bonusItems);
    }
}