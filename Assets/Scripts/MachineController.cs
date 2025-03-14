using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MachineController : MonoBehaviour, IMachine
{
    private InventoryManager _inventoryManager;
    [SerializeField] private Button machineOpenPanelButton;
    [SerializeField] private CanvasGroup craftingPanelCanvasGroup;
    [SerializeField] private CraftingSlot[] _craftingSlots;
    [SerializeField] private CraftedSlot _craftedSlot;
    [SerializeField] private Recipe[] _availableRecipes;

    [SerializeField] private Button _craftButton;
    [SerializeField] private TextMeshProUGUI _possibleOutputText;
    [SerializeField] private Image _progressBar;
    [SerializeField] private CanvasGroup[] _craftingSlotsCanvasGroup;

    private Recipe _currentRecipe;
    public bool IsAvailable { get; private set; } = true;

    private void Start()
    {
        _craftButton.interactable = false;
        _progressBar.gameObject.SetActive(false);
        _possibleOutputText.text = "";
        SetCraftingSlotsInteractable(true);
    }

    public void Initialize(InventoryManager inventoryManager)
    {
        _inventoryManager = inventoryManager;
        for (int i = 0; i < _craftingSlots.Length; i++)
        {
            _craftingSlots[i].Initialize(inventoryManager, i);
            _craftingSlots[i].OnItemSlotChanged += OnItemSlotChanged;
        }

        _craftedSlot.Initialize(inventoryManager, 0);
    }

    public void OnItemSlotChanged()
    {
        _currentRecipe = FindMatchingRecipe();

        if (_currentRecipe != null)
        {
            _possibleOutputText.text = _currentRecipe.OutputItem.ItemName;
            _craftButton.interactable = true;
        }
        else
        {
            _possibleOutputText.text = "";
            _craftButton.interactable = false;
        }
    }

    public void TryCraft()
    {
        if (_currentRecipe == null)
        {
            Debug.LogWarning("No valid recipe!");
            return;
        }

        StartCrafting(_currentRecipe);
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
        SetCraftingSlotsInteractable(false);
        Debug.Log($"Starting crafting: {recipe.OutputItem.ItemName}");

        _craftButton.interactable = false;
        foreach (var slot in _craftingSlots)
        {
            slot.OnItemSlotChanged -= OnItemSlotChanged;
        }

        _progressBar.gameObject.SetActive(true);
        _progressBar.fillAmount = 0;

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
        float timeElapsed = 0f;

        while (timeElapsed < craftingTime)
        {
            timeElapsed += Time.deltaTime;
            _progressBar.fillAmount = Mathf.Clamp01(timeElapsed / craftingTime);
            yield return null;
        }

        CompleteCrafting(outputItem, successRate);
    }

    private void CompleteCrafting(Item outputItem, float successRate)
    {
        foreach (var slot in _craftingSlots)
        {
            slot.Clear();
        }

        foreach (var slot in _craftingSlots)
        {
            slot.OnItemSlotChanged += OnItemSlotChanged;
        }

        _progressBar.gameObject.SetActive(false);

        if (Random.Range(0f, 1f) <= successRate)
        {
            _craftedSlot.SetItem(outputItem, 1);
            Debug.Log($"{outputItem.ItemName} crafted successfully!");
            CraftingEvents.ItemCrafted(outputItem);
        }
        else
        {
            Debug.LogWarning($"Crafting of {outputItem.ItemName} failed.");
        }

        SetCraftingSlotsInteractable(true);
        _possibleOutputText.text = "";
        IsAvailable = true;
    }

    private void SetCraftingSlotsInteractable(bool interactable)
    {
        foreach (var craftingSlotCanvasGroup in _craftingSlotsCanvasGroup)
        {
            craftingSlotCanvasGroup.alpha = interactable ? 1f : 0.5f;
            craftingSlotCanvasGroup.blocksRaycasts = interactable;
        }
    }

    public void UnlockThisNewMachine()
    {
        machineOpenPanelButton.interactable = true;
    }

    public void ShowCraftingPanel()
    {
        craftingPanelCanvasGroup.alpha = 1;
        craftingPanelCanvasGroup.interactable = true; 
        craftingPanelCanvasGroup.blocksRaycasts = true;
    }

    public void HideCraftingPanel()
    {
        craftingPanelCanvasGroup.alpha = 0;
        craftingPanelCanvasGroup.interactable = false;
        craftingPanelCanvasGroup.blocksRaycasts = false;
    }
}