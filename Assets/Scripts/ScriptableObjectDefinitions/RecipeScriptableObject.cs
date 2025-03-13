using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "New Recipe", menuName = "ForgingGame/Recipe")]
public class Recipe : ScriptableObject
{
    public MachineType MachineRequired;
    public List<ItemQuantity> InputItems;
    public Item OutputItem;
    public float CraftingTime;
    public float SuccessRate;
}

[System.Serializable]
public struct ItemQuantity
{
    public Item Item;
    public int Quantity;
}

public enum MachineType
{
    Smelter,
    Anvil,
    Enchanter,
    RuneCarver,
    DragonForge
}