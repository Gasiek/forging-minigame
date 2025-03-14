using TMPro;
using UnityEngine;

public class Tooltip : MonoBehaviour
{
    public static Tooltip Instance { get; private set; }

    [SerializeField] private TextMeshProUGUI _tooltipText;
    [SerializeField] private RectTransform _tooltipPanel;
    [SerializeField] private Vector3 _tooltipOffset = new(0f, 20f, 0f);

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Update()
    {
        if (_tooltipPanel.gameObject.activeSelf)
        {
            _tooltipPanel.transform.position = Input.mousePosition + _tooltipOffset;
        }
    }

    public void ShowTooltip(string text)
    {
        _tooltipText.text = text;
        _tooltipPanel.gameObject.SetActive(true);
    }

    public void HideTooltip()
    {
        _tooltipPanel.gameObject.SetActive(false);
    }
}