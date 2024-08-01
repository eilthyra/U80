using OUA.Items;
using OUA.Items.Inventories;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace OUA.Npcs.Occupations.Chests
{
        public class ChestItemButton : MonoBehaviour
        {
            [SerializeField] private TextMeshProUGUI itemNameText = null;
            [SerializeField] private Image itemIconImage = null;

            private ChestSystem chestSystem = null;
            private InventoryItem item = null;

            public void Initialise(ChestSystem chestSystem, InventoryItem item, int quantity)
            {
                this.chestSystem = chestSystem;
                this.item = item;

                itemNameText.text = $"{item.Name} ({quantity})";
                itemIconImage.sprite = item.Icon;
            }

            public void SelectItem()
            {
                chestSystem.SetItem(item);
            }
        }
}
