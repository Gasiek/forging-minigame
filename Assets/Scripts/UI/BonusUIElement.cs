using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class BonusUIElement : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _bonusNameText;
    [SerializeField] private TextMeshProUGUI _bonusDescriptionText;
    
    public void UpdateUI(string bonusName, string bonusDescription)
    {
        _bonusNameText.text = bonusName;
        _bonusDescriptionText.text = bonusDescription;
    }
}
