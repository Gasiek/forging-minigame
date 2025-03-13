using System;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class InventorySlot : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler, IDropHandler
{
    public Image SlotImage;
    [SerializeField] protected TextMeshProUGUI _quantityText;
    [SerializeField] protected GameObject _quantityGameObject;
    [SerializeField] protected Sprite _emptySprite;
    public Item CurrentItem;

    private int _quantity;

    public int Quantity
    {
        get => _quantity;
        set
        {
            _quantity = value;
            UpdateQuantityUI();
        }
    }

    private void Awake()
    {
        Clear();
    }

    private void UpdateQuantityUI()
    {
        if (_quantity <= 0)
        {
            _quantityGameObject.SetActive(false);
            SlotImage.sprite = _emptySprite;
        }
        else
        {
            _quantityGameObject.SetActive(true);
        }

        _quantityText.text = _quantity.ToString();
    }

    public void Clear()
    {
        CurrentItem = null;
        Quantity = 0;
        SlotImage.sprite = _emptySprite;
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        if (CurrentItem != null && !IsEmpty())
        {
            DragIcon.Instance.SetSprite(SlotImage.sprite);
            DragIcon.Instance.gameObject.SetActive(true);
            DragIcon.Instance.SetPosition(eventData.position);
            DragIcon.Instance.SetCount(Quantity);
        }
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (DragIcon.Instance.gameObject.activeSelf)
        {
            DragIcon.Instance.SetPosition(eventData.position);
        }
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        DragIcon.Instance.gameObject.SetActive(false);
    }

    public bool IsEmpty()
    {
        return Quantity <= 0;
    }

    public virtual void OnDrop(PointerEventData eventData)
    {
        if (eventData.pointerDrag != null &&
            eventData.pointerDrag.TryGetComponent(out InventorySlot draggedInventorySlot))
        {
            if (!IsEmpty())
            {
                if (draggedInventorySlot.CurrentItem.ItemName == CurrentItem.ItemName)
                {
                    Quantity += draggedInventorySlot.Quantity;
                    draggedInventorySlot.Clear();
                }
                else
                {
                    var tempItem = draggedInventorySlot.CurrentItem;
                    var tempQuantity = draggedInventorySlot.Quantity;
                    var tempSprite = draggedInventorySlot.SlotImage.sprite;
                    draggedInventorySlot.CurrentItem = CurrentItem;
                    draggedInventorySlot.Quantity = Quantity;
                    draggedInventorySlot.SlotImage.sprite = SlotImage.sprite;
                    CurrentItem = tempItem;
                    Quantity = tempQuantity;
                    SlotImage.sprite = tempSprite;
                }
            }
            else
            {
                CurrentItem = draggedInventorySlot.CurrentItem;
                Quantity = draggedInventorySlot.Quantity;
                SlotImage.sprite = draggedInventorySlot.SlotImage.sprite;
                draggedInventorySlot.Clear();
            }
        }
    }
}