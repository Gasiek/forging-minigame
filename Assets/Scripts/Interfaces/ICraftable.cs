public interface ICraftable
{
    string Name { get; }
    float CraftingTime { get; }
    bool TryCraft(out Item craftedItem);
}
