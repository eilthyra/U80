using OUA.Items.Inventories;
using TMPro;
using UnityEngine;

namespace OUA.Items
{
    public class ItemEater : MonoBehaviour
    {
        [SerializeField] private Inventory inventory = null;
        [SerializeField] private TextMeshProUGUI areYouSureText = null;

        private int slotIndex = 0;

        private void OnDisable() => slotIndex = -1;

        public void Activate(ItemSlot itemSlot, int slotIndex)
        {
            // Check if inventory and areYouSureText are assigned
            if (inventory == null || areYouSureText == null)
            {
                Debug.LogError("Inventory or areYouSureText is not assigned.");
                return;
            }

            this.slotIndex = slotIndex;

            areYouSureText.text = $"Are you sure you wish to consume 1x {itemSlot.item.ColouredName}?";

            gameObject.SetActive(true);
        }

        public void Eat()
        {
            if (inventory != null)
            {
                inventory.ConsumeItem(slotIndex);
            }

            gameObject.SetActive(false);
        }
    }
}