using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "New Recipe", menuName = "ForgingGame/Recipe")]
public class Recipe : ScriptableObject
{
    public MachineType MachineRequired;
    public List<Item> InputItems;
    public Item OutputItem;
    [Tooltip("Crafting time in seconds.")]
    public float CraftingTime;
    [Tooltip("Probability value between 0 and 1.")]
    public float SuccessRate;
}

public enum MachineType
{
    Smelter,
    Anvil,
    Enchanter,
    RuneCarver,
    DragonForge
}