using OUA.Items.Inventories;
using UnityEngine;
using UnityEngine.EventSystems;

namespace OUA.Items
{
    public class InventoryItemRightClickHandler : ItemRightClickHandler
    {
        [SerializeField] private ItemEater itemEater = null;

        public override void OnPointerDown(PointerEventData eventData)
        {
            if (eventData.button == PointerEventData.InputButton.Right)
            {
                base.OnPointerDown(eventData);

                // Check if itemEater is assigned
                if (itemEater == null)
                {
                    Debug.LogError("ItemEater is not assigned in the Inspector.");
                    return;
                }

                // Check if ItemSlotUI is assigned and can be cast to InventorySlot
                if (ItemSlotUI == null)
                {
                    Debug.LogError("ItemSlotUI is not assigned.");
                    return;
                }

                InventorySlot thisSlot = ItemSlotUI as InventorySlot;
                if (thisSlot == null)
                {
                    Debug.LogError("ItemSlotUI cannot be cast to InventorySlot.");
                    return;
                }

                // Check if there are no hovered objects
                if (eventData.hovered.Count == 0 && thisSlot.ItemSlot.item.ItemUsageType.Name == "Edible")
                {
                    itemEater.Activate(thisSlot.ItemSlot, thisSlot.SlotIndex);
                }
            }
        }
    }
}