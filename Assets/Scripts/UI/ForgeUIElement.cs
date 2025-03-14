using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ForgeUIElement : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] private string _forgeName;
    [SerializeField] private Button machineOpenPanelButton;
    [SerializeField] private GameObject machineOpenPanelButtonPadlock;

    public void UnlockForge()
    {
        machineOpenPanelButton.interactable = true;
        machineOpenPanelButtonPadlock.SetActive(false);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        Tooltip.Instance.ShowTooltip(_forgeName);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        Tooltip.Instance.HideTooltip();
    }
}