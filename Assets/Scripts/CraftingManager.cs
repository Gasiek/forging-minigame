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
            float successChance = Random.Range(0f, 1f);
            if (successChance <= recipe.SuccessRate)
            {
                _inventory.AddItem(recipe.OutputItem, 1);
            }
        }
    }
}