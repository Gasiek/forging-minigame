using System;

public static class CraftingEvents
{
    public static event Action<Item> OnItemCrafted;

    public static void ItemCrafted(Item item)
    {
        OnItemCrafted?.Invoke(item);
    }
}