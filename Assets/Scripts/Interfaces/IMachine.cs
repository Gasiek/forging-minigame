public interface IMachine
{
    void StartCrafting(Recipe recipe);
    bool IsAvailable { get; }
}