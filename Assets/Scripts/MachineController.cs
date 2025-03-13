using System.Collections;
using UnityEngine;

public class MachineController : MonoBehaviour, IMachine
{
    [SerializeField] private InventoryManager _inventory;

    public bool IsAvailable { get; private set; } = true;

    public void StartCrafting(Recipe recipe)
    {
        if (IsAvailable && _inventory.HasItems(recipe.InputItems))
        {
            IsAvailable = false;
            _inventory.RemoveItems(recipe.InputItems);
            StartCoroutine(CraftingCoroutine(recipe));
        }
    }

    private IEnumerator CraftingCoroutine(Recipe recipe)
    {
        yield return new WaitForSeconds(recipe.CraftingTime);
        float successChance = Random.Range(0f, 1f);
        if (successChance <= recipe.SuccessRate)
        {
            _inventory.AddItem(recipe.OutputItem, 1);
        }
        IsAvailable = true;
    }
}
