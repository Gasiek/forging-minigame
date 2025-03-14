using TMPro;
using UnityEngine;

public class BonusUIElement : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _bonusNameText;
    [SerializeField] private TextMeshProUGUI _bonusDescriptionText;
    [SerializeField] private InventorySlot _bonusSlot;

    public void Initialize(BonusItem bonusItem)
    {
        _bonusNameText.text = bonusItem.ItemName;
        _bonusDescriptionText.text = bonusItem.Description;
        _bonusSlot.SetItem(bonusItem, 1);
    }
}