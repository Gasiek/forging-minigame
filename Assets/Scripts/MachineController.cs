using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MachineController : MonoBehaviour, IMachine
{
    private InventoryManager _inventoryManager;
    [SerializeField] private CraftingSlot[] _craftingSlots;
    [SerializeField] private CraftedSlot _craftedSlot;
    [SerializeField] private Recipe[] _availableRecipes;

    public bool IsAvailable { get; private set; } = true;

    public void Initialize(InventoryManager inventoryManager)
    {
        _inventoryManager = inventoryManager;
        for (int i = 0; i < _craftingSlots.Length; i++)
        {
            _craftingSlots[i].Initialize(inventoryManager, i);
        }
        _craftedSlot.Initialize(inventoryManager, 0);
    }
    
    public void TryCraft()
    {
        if (_inventoryManager == null)
        {
            Debug.LogError("InventoryManager not set! Did you forget to call Initialize?");
            return;
        }

        Recipe matchingRecipe = FindMatchingRecipe();

        if (matchingRecipe != null)
        {
            StartCrafting(matchingRecipe);
        }
        else
        {
            Debug.LogWarning("No matching recipe found or missing ingredients.");
        }
    }
    
    private Recipe FindMatchingRecipe()
    {
        List<Item> itemsInSlots = new List<Item>();

        foreach (var slot in _craftingSlots)
        {
            if (slot.Item != null)
            {
                itemsInSlots.Add(slot.Item);
            }
        }

        foreach (var recipe in _availableRecipes)
        {
            if (ItemsMatchRecipe(recipe.InputItems, itemsInSlots))
            {
                return recipe;
            }
        }

        return null;
    }
    
    private bool ItemsMatchRecipe(List<Item> recipeItems, List<Item> slotItems)
    {
        if (recipeItems.Count != slotItems.Count)
            return false;

        List<Item> tempRecipeItems = new List<Item>(recipeItems);
        List<Item> tempSlotItems = new List<Item>(slotItems);

        foreach (var item in tempSlotItems)
        {
            if (!tempRecipeItems.Remove(item))
            {
                return false;
            }
        }

        return tempRecipeItems.Count == 0;
    }

    public void StartCrafting(Recipe recipe)
    {
        IsAvailable = false;
        Debug.Log($"Starting crafting: {recipe.OutputItem.ItemName}");
        // lock the crafting slots
        // foreach (var slot in _craftingSlots)
        // {
        //     slot.Clear();
        // }

        float finalSuccessRate = recipe.SuccessRate;
        float finalCraftingTime = recipe.CraftingTime;

        List<BonusItem> activeBonuses = _inventoryManager.GetBonusItems();
        foreach (var bonus in activeBonuses)
        {
            bonus.ApplyEffect(ref finalSuccessRate, ref finalCraftingTime);
        }

        StartCoroutine(CraftingCoroutine(recipe.OutputItem, finalSuccessRate, finalCraftingTime));
    }
    

    private IEnumerator CraftingCoroutine(Item outputItem, float successRate, float craftingTime)
    {
        yield return new WaitForSeconds(craftingTime);
        foreach (var slot in _craftingSlots)
        {
            slot.Clear();
        }
        if (Random.Range(0f, 1f) <= successRate)
        {
            _craftedSlot.SetItem(outputItem, 1);
            Debug.Log($"{outputItem.ItemName} crafted successfully!");
        }
        else
        {
            Debug.LogWarning($"Crafting of {outputItem.ItemName} failed.");
        }

        IsAvailable = true;
    }
}