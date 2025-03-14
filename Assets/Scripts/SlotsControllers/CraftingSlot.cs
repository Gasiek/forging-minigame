using System;
using UnityEngine.EventSystems;

public class CraftingSlot : InventorySlot
{
    public event Action OnItemSlotChanged;

    public override void OnDrop(PointerEventData eventData)
    {
        if (eventData.pointerDrag != null &&
            eventData.pointerDrag.TryGetComponent(out InventorySlot draggedSlot))
        {
            if (draggedSlot == this) return;
            SetItem(draggedSlot.Item, 1);
            draggedSlot.RemoveQuantity(1);

            OnItemSlotChanged?.Invoke();
        }
    }

    public override void Clear()
    {
        base.Clear();
        OnItemSlotChanged?.Invoke();
    }
}