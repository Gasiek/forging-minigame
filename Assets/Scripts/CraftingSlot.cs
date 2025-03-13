using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class CraftingSlot : InventorySlot
{
   
    public override void OnDrop(PointerEventData eventData)
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