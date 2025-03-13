using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CraftingManager
{
    private readonly InventoryManager _inventory;

    public CraftingManager(InventoryManager inventory)
    {
        _inventory = inventory;
    }

    public void Craft(Recipe recipe)
    {
        if (_inventory.HasItems(recipe.InputItems))
        {
            _inventory.RemoveItems(recipe.InputItems);
            
            float finalSuccessRate = recipe.SuccessRate;
            float finalCraftingTime = recipe.CraftingTime;

            List<BonusItem> activeBonuses = _inventory.GetBonusItems();
            foreach (var bonus in activeBonuses)
            {
                bonus.ApplyEffect(ref finalSuccessRate, ref finalCraftingTime);
            }
            
            _inventory.StartCoroutine(CraftingCoroutine(recipe.OutputItem, finalSuccessRate, finalCraftingTime));
        }
    }

    private IEnumerator CraftingCoroutine(Item outputItem, float successRate, float craftingTime)
    {
        yield return new WaitForSeconds(craftingTime);
        if (Random.Range(0f, 1f) <= successRate)
        {
            _inventory.AddItem(outputItem, 1);
        }
    }
}
