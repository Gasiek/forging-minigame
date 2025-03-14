using TMPro;
using UnityEngine;

public class QuestUIElement : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _questNameText;
    [SerializeField] private TextMeshProUGUI _questDescriptionText;
    [SerializeField] private TextMeshProUGUI _progressText;
    
    public void UpdateUI(string questName, string questDescription, int currentProgress, int requiredAmount)
    {
        _questNameText.text = questName;
        _questDescriptionText.text = questDescription;
        _progressText.text = $"{currentProgress}/{requiredAmount}";
    }
}
