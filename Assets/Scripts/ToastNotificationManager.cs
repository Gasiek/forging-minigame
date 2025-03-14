using UnityEngine;
using TMPro;

public class ToastNotificationManager : MonoBehaviour
{
    public static ToastNotificationManager Instance;

    [SerializeField] private GameObject _toastPrefab;
    [SerializeField] private Transform _toastContainer;
    [SerializeField] private float _displayDuration = 3f;

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

    public void ShowNotification(string message)
    {
        GameObject toast = Instantiate(_toastPrefab, _toastContainer);
        TextMeshProUGUI toastText = toast.GetComponentInChildren<TextMeshProUGUI>();
        toastText.text = message;
        Destroy(toast, _displayDuration);
    }
}