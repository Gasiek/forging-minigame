using System.Collections.Generic;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    private Dictionary<Item, int> _inventory = new Dictionary<Item, int>();

    public bool HasItems(List<ItemQuantity> items)
    {
        foreach (var item in items)
        {
            if (!_inventory.ContainsKey(item.Item) || _inventory[item.Item] < item.Quantity)
                return false;
        }
        return true;
    }

    public void AddItem(Item item, int quantity)
    {
        if (_inventory.ContainsKey(item))
            _inventory[item] += quantity;
        else
            _inventory[item] = quantity;
    }

    public void RemoveItems(List<ItemQuantity> items)
    {
        foreach (var item in items)
        {
            if (_inventory.ContainsKey(item.Item))
            {
                _inventory[item.Item] -= item.Quantity;
                if (_inventory[item.Item] <= 0)
                    _inventory.Remove(item.Item);
            }
        }
    }
    
    public List<BonusItem> GetBonusItems()
    {
        List<BonusItem> activeBonuses = new List<BonusItem>();
        foreach (var item in _inventory.Keys)
        {
            if (item is BonusItem bonusItem)
            {
                activeBonuses.Add(bonusItem);
            }
        }
        return activeBonuses;
    }
}