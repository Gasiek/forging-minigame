using TMPro;
using UnityEngine;

public class ForgeUIElement : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _forgeName;
    [SerializeField] private TextMeshProUGUI _forgeAction;
    [SerializeField] private GameObject _singleCrafting;
    [SerializeField] private GameObject _doubleCrafting;
    [SerializeField] private GameObject _forgePanel;

    public void OpenForgePanel(MachineScriptableObject machine)
    {
        _forgeName.text = machine.MachineName;
        _forgeAction.text = machine.ActionName;
        if (machine.NumberOfCraftingSlots == 1)
        {
            _doubleCrafting.SetActive(false);
            _singleCrafting.SetActive(true);
        }
        else
        {
            _singleCrafting.SetActive(false);
            _doubleCrafting.SetActive(true);
        }
        _forgePanel.SetActive(true);
    }   
}
