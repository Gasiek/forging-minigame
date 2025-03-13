using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DragIcon : MonoBehaviour
{
    public static DragIcon Instance { get; private set; }
    [SerializeField] private Image iconImage;
    [SerializeField] private TextMeshProUGUI countText;
    private Transform _transform;

    private void Awake()
    {
        _transform = transform;
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }

        gameObject.SetActive(false);
    }

    public void SetSprite(Sprite sprite)
    {
        iconImage.sprite = sprite;
    }
    
    public void SetPosition(Vector3 position)
    {
        _transform.position = position;
    }

    public void SetCount(int count)
    {
        countText.text = count.ToString();
    }
}
