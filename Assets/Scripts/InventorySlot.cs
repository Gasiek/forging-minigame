using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class InventorySlot : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler, IDropHandler
{
    [SerializeField] protected Image _slotImage;
    [SerializeField] protected TextMeshProUGUI _quantityText;
    [SerializeField] protected GameObject _quantityGameObject;
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
    
    private void UpdateQuantityUI()
    {
        if (_quantity <= 0)
        {
            _quantityGameObject.SetActive(false);
        }
        else
        {
            _quantityGameObject.SetActive(true);
        }
        _quantityText.text = _quantity.ToString();
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        if (CurrentItem != null && !IsEmpty())
        {
            DragIcon.Instance.SetSprite(_slotImage.sprite);
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
        if (eventData.pointerDrag != null && eventData.pointerDrag.TryGetComponent(out InventorySlot draggedInventorySlot))
        {
            if (!draggedInventorySlot.IsEmpty())
            {
                CurrentItem = draggedInventorySlot.CurrentItem;
                _slotImage.sprite = CurrentItem.Icon;
                draggedInventorySlot.Quantity--;
            }
        }
    }
}