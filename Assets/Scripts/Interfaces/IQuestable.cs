public interface IQuestable
{
    string QuestName { get; }
    void UpdateProgress(Item item);
}
