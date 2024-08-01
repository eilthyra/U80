using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace OUA.Items.Inventories
{
    public class Inventory : MonoBehaviour, IItemContainer
    {
        [SerializeField] private int money = 100;
        [SerializeField] public UnityEvent onInventoryItemsUpdated = null;
        [SerializeField] private ItemSlot[] itemSlots = new ItemSlot[0];

        public UnityEvent<int> OnMoneyChanged = new UnityEvent<int>();

        [SerializeField] private HungerSystem hungerSystem = null; // HungerSystem referansı

        public int Money
        {
            get { return money; }
            set
            {
                money = value;
                OnMoneyChanged.Invoke(money);
            }
        }

        public ItemSlot GetSlotByIndex(int index) => itemSlots[index];

        public ItemSlot[] GetItemSlots() => itemSlots; // Getter ekledik

        public ItemSlot AddItem(ItemSlot itemSlot)
        {
            for (int i = 0; i < itemSlots.Length; i++)
            {
                if (itemSlots[i].item != null && itemSlots[i].item == itemSlot.item)
                {
                    int slotRemainingSpace = itemSlots[i].item.MaxStack - itemSlots[i].quantity;

                    if (itemSlot.quantity <= slotRemainingSpace)
                    {
                        itemSlots[i].quantity += itemSlot.quantity;
                        itemSlot.quantity = 0;
                        onInventoryItemsUpdated.Invoke();
                        return itemSlot;
                    }
                    else if (slotRemainingSpace > 0)
                    {
                        itemSlots[i].quantity += slotRemainingSpace;
                        itemSlot.quantity -= slotRemainingSpace;
                    }
                }
            }

            for (int i = 0; i < itemSlots.Length; i++)
            {
                if (itemSlots[i].item == null)
                {
                    if (itemSlot.quantity <= itemSlot.item.MaxStack)
                    {
                        itemSlots[i] = itemSlot;
                        itemSlot.quantity = 0;
                        onInventoryItemsUpdated.Invoke();
                        return itemSlot;
                    }
                    else
                    {
                        itemSlots[i] = new ItemSlot(itemSlot.item, itemSlot.item.MaxStack);
                        itemSlot.quantity -= itemSlot.item.MaxStack;
                    }
                }
            }

            onInventoryItemsUpdated.Invoke();
            return itemSlot;
        }

        public void RemoveItem(ItemSlot itemSlot)
        {
            for (int i = 0; i < itemSlots.Length; i++)
            {
                if (itemSlots[i].item != null && itemSlots[i].item == itemSlot.item)
                {
                    if (itemSlots[i].quantity < itemSlot.quantity)
                    {
                        itemSlot.quantity -= itemSlots[i].quantity;
                        itemSlots[i] = new ItemSlot();
                    }
                    else
                    {
                        itemSlots[i].quantity -= itemSlot.quantity;
                        if (itemSlots[i].quantity == 0)
                        {
                            itemSlots[i] = new ItemSlot();
                            onInventoryItemsUpdated.Invoke();
                            return;
                        }
                    }
                }
            }
        }

        public List<InventoryItem> GetAllUniqueItems()
        {
            List<InventoryItem> items = new List<InventoryItem>();

            foreach (var slot in itemSlots)
            {
                if (slot.item == null) continue;
                if (items.Contains(slot.item)) continue;
                items.Add(slot.item);
            }

            return items;
        }

        public void RemoveAt(int slotIndex)
        {
            if (slotIndex < 0 || slotIndex >= itemSlots.Length) return;
            itemSlots[slotIndex] = new ItemSlot();
            onInventoryItemsUpdated.Invoke();
        }

        public void Swap(int indexOne, int indexTwo)
        {
            if (indexOne < 0 || indexOne >= itemSlots.Length || indexTwo < 0 || indexTwo >= itemSlots.Length) return;

            ItemSlot firstSlot = itemSlots[indexOne];
            ItemSlot secondSlot = itemSlots[indexTwo];

            if (firstSlot.Equals(secondSlot)) return;

            if (secondSlot.item != null && firstSlot.item == secondSlot.item)
            {
                int secondSlotRemainingSpace = secondSlot.item.MaxStack - secondSlot.quantity;

                if (firstSlot.quantity <= secondSlotRemainingSpace)
                {
                    itemSlots[indexTwo].quantity += firstSlot.quantity;
                    itemSlots[indexOne] = new ItemSlot();
                    onInventoryItemsUpdated.Invoke();
                    return;
                }
            }

            itemSlots[indexOne] = secondSlot;
            itemSlots[indexTwo] = firstSlot;
            onInventoryItemsUpdated.Invoke();
        }

        public bool HasItem(InventoryItem item)
        {
            foreach (var slot in itemSlots)
            {
                if (slot.item != null && slot.item == item)
                {
                    return true;
                }
            }

            return false;
        }

        public int GetTotalQuantity(InventoryItem item)
        {
            int totalCount = 0;

            foreach (var slot in itemSlots)
            {
                if (slot.item != null && slot.item == item)
                {
                    totalCount += slot.quantity;
                }
            }

            return totalCount;
        }

        public int GetItemIndex(InventoryItem item)
        {
            for (int i = 0; i < itemSlots.Length; i++)
            {
                if (itemSlots[i].item != null && itemSlots[i].item == item)
                {
                    return i;
                }
            }
            return -1;
        }
        public void SetSlotByIndex(int index, ItemSlot itemSlot)
        {
            if (index >= 0 && index < itemSlots.Length)
            {
                itemSlots[index] = itemSlot;
                onInventoryItemsUpdated.Invoke();
            }
        }
        public InventoryItem GetItemByName(string itemName)
        {
            foreach (var slot in itemSlots)
            {
                if (slot.item != null && slot.item.name == itemName)
                {
                    return slot.item;
                }
            }
            return null;
        }

        // Method to consume an item by reducing its quantity by 1
        public void ConsumeItem(int slotIndex)
        {
            if (slotIndex < 0 || slotIndex >= itemSlots.Length) return;
            if (itemSlots[slotIndex].item == null) return;
            if (itemSlots[slotIndex].item.ItemUsageType.Name == "Edible")
            {
                itemSlots[slotIndex].quantity--;

                // Hunger increase based on the foodRate
                if (hungerSystem != null)
                {
                    hungerSystem.IncreaseHunger(itemSlots[slotIndex].item.FoodRate);
                }

                if (itemSlots[slotIndex].quantity <= 0)
                {
                    itemSlots[slotIndex] = new ItemSlot();
                }
            }
            onInventoryItemsUpdated.Invoke();
        }
    }
}
