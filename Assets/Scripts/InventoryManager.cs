using System.Collections.Generic;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    private Dictionary<Item, int> _inventory = new Dictionary<Item, int>();

    public bool HasItems(List<Item> items)
    {
        foreach (var item in items)
        {
            if (!_inventory.ContainsKey(item))
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

    public void RemoveItems(List<Item> items)
    {
        foreach (var item in items)
        {
            if (_inventory.ContainsKey(item))
            {
                _inventory[item] -= 1;
                if (_inventory[item] <= 0)
                    _inventory.Remove(item);
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