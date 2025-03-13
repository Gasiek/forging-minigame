using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class CraftingSlot : InventorySlot
{
   
    public override void OnDrop(PointerEventData eventData)
    {
        if (eventData.pointerDrag != null && eventData.pointerDrag.TryGetComponent(out InventorySlot draggedInventorySlot))
        {
            if (IsEmpty())
            {
                CurrentItem = draggedInventorySlot.CurrentItem;
                SlotImage.sprite = CurrentItem.Icon;
                Quantity = 1;
                draggedInventorySlot.Quantity--;
            }
        }
    }
}