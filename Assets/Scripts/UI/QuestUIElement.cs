using TMPro;
using UnityEngine;

public class QuestUIElement : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _questNameText;
    [SerializeField] private TextMeshProUGUI _questDescriptionText;
    [SerializeField] private TextMeshProUGUI _progressText;

    public void Initialize(string questName, string questDescription, int currentProgress, int requiredAmount)
    {
        _questNameText.text = questName;
        _questDescriptionText.text = questDescription;
        UpdateUI(currentProgress, requiredAmount);
    }

    public void UpdateUI(int currentProgress, int requiredAmount)
    {
        _progressText.text = $"{currentProgress}/{requiredAmount}";
    }
}