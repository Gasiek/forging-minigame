using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class InventorySlot : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler, IDropHandler
{
    [SerializeField] private Image _slotImage;
    [SerializeField] private TextMeshProUGUI _quantityText;
    [SerializeField] private GameObject _quantityGameObject;
    [SerializeField] private Sprite _emptySprite;

    public Item Item { get; private set; }
    public int Quantity { get; private set; }
    public bool IsEmpty => Item == null || Quantity <= 0;
    protected InventoryManager _inventoryManager;
    private int _slotIndex;

    public void Initialize(InventoryManager inventoryManager, int slotIndex)
    {
        _inventoryManager = inventoryManager;
        _slotIndex = slotIndex;
        Clear();
    }

    public void SetItem(Item item, int quantity)
    {
        Item = item;
        Quantity = quantity;
        UpdateUI();
    }

    public void SetSpriteColor(Color color)
    {
        _slotImage.color = color;
    }

    public void AddQuantity(int amount)
    {
        if (Item != null)
        {
            Quantity += amount;
            UpdateUI();
        }
    }

    public void RemoveQuantity(int amount)
    {
        if (Item != null)
        {
            Quantity -= amount;
            if (Quantity <= 0)
            {
                Clear();
            }
            else
            {
                UpdateUI();
            }
        }
    }

    public virtual void Clear()
    {
        Item = null;
        Quantity = 0;
        UpdateUI();
    }

    public void UpdateUI()
    {
        if (IsEmpty)
        {
            _slotImage.sprite = _emptySprite;
            _quantityGameObject.SetActive(false);
        }
        else
        {
            _slotImage.sprite = Item.Icon;
            _quantityText.text = Quantity.ToString();
            _quantityGameObject.SetActive(true);
        }
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        if (!IsEmpty)
        {
            Debug.Log("OnBeginDrag");
            DragIcon.Instance.SetSprite(_slotImage.sprite);
            DragIcon.Instance.SetCount(Quantity);
            DragIcon.Instance.gameObject.SetActive(true);
            DragIcon.Instance.SetPosition(eventData.position);
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

    public virtual void OnDrop(PointerEventData eventData)
    {
        if (eventData.pointerDrag != null)
        {
            if (eventData.pointerDrag.TryGetComponent(out CraftedSlot draggedCraftedSlot))
            {
                Debug.Log("Dragging crafted slot");
                if (IsEmpty)
                {
                    SetItem(draggedCraftedSlot.Item, draggedCraftedSlot.Quantity);
                    draggedCraftedSlot.Clear();
                }
                else if (draggedCraftedSlot.Item == Item)
                {
                    AddQuantity(draggedCraftedSlot.Quantity);
                    draggedCraftedSlot.Clear();
                }

                return;
            }

            if (eventData.pointerDrag.TryGetComponent(out CraftingSlot draggedCraftingSlot))
            {
                Debug.Log("Dragging crafting slot");
                if (IsEmpty)
                {
                    SetItem(draggedCraftingSlot.Item, draggedCraftingSlot.Quantity);
                    draggedCraftingSlot.Clear();
                }
                else if (draggedCraftingSlot.Item == Item)
                {
                    AddQuantity(draggedCraftingSlot.Quantity);
                    draggedCraftingSlot.Clear();
                }

                return;
            }

            if (eventData.pointerDrag.TryGetComponent(out InventorySlot draggedSlot))
            {
                Debug.Log("Dragging slot");
                if (draggedSlot == this) return;
                if (draggedSlot.Item == Item)
                {
                    AddQuantity(draggedSlot.Quantity);
                    draggedSlot.Clear();
                    return;
                }

                _inventoryManager.SwapSlots(draggedSlot._slotIndex, _slotIndex);
            }
        }
    }
}