using OUA.Items;
using OUA.Items.Inventories;
using OUA.Npcs.Occupations.Vendors;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace OUA.Npcs.Occupations.Chests
{
    public class ChestSystem : MonoBehaviour
    {
        [SerializeField] private GameObject buttonPrefab = null;
        [SerializeField] private Transform buttonHolderTransform = null;
        [SerializeField] private GameObject selectedItemDataHolder = null;

        [Header("Data Display")]
        [SerializeField] private TextMeshProUGUI itemNameText = null;
        [SerializeField] private TextMeshProUGUI itemDescriptionText = null;
        [SerializeField] private TextMeshProUGUI itemDataText = null;

        [Header("Quantity Display")]
        [SerializeField] private TextMeshProUGUI quantityText = null;
        [SerializeField] private Slider quantitySlider = null;

        private ChestData scenarioData = null;
        private InventoryItem currentItem = null;

        public void StartScenario(ChestData scenarioData)
        {
            this.scenarioData = scenarioData;

            SetCurrentItemContainer(true);

            SetItem(scenarioData.ChestItemContainer.GetSlotByIndex(0).item);
        }

        public void SetCurrentItemContainer(bool isFirst)
        {
            ClearItemButtons();

            scenarioData.IsFirstContainerGetting = isFirst;

            var items = scenarioData.ChestItemContainer.GetAllUniqueItems();

            for (int i = 0; i < items.Count; i++)
            {
                GameObject buttonInstance = Instantiate(buttonPrefab, buttonHolderTransform);
                buttonInstance.GetComponent<ChestItemButton>().Initialise(
                    this,
                    items[i],
                    scenarioData.ChestItemContainer.GetTotalQuantity(items[i]));
            }

            selectedItemDataHolder.SetActive(false);
        }

        public void SetItem(InventoryItem item)
        {
            currentItem = item;

            if (item == null)
            {
                itemNameText.text = string.Empty;
                itemDescriptionText.text = string.Empty;
                itemDataText.text = string.Empty;
                return;
            }

            itemNameText.text = item.Name;
            itemDescriptionText.text = item.Description;
            itemDataText.text = item.GetInfoDisplayText();

            int totalQuantity = scenarioData.ChestItemContainer.GetTotalQuantity(item);
            quantityText.text = $"0/{totalQuantity}";
            quantitySlider.maxValue = totalQuantity;
            quantitySlider.value = 0;

            selectedItemDataHolder.SetActive(true);
        }

        public void UpdateSliderText(float quantity)
        {
            int totalQuantity = scenarioData.ChestItemContainer.GetTotalQuantity(currentItem);
            quantityText.text = $"{quantity}/{totalQuantity}";
        }

        public void ConfirmButton()
        {
            var itemSlotSawp = new ItemSlot(currentItem, (int)quantitySlider.value);

            bool soldAll = (int)quantitySlider.value == scenarioData.ChestItemContainer.GetTotalQuantity(currentItem);

            if (soldAll) { selectedItemDataHolder.SetActive(false); }

            scenarioData.InventoryItemContainer.AddItem(itemSlotSawp);
            scenarioData.ChestItemContainer.RemoveItem(itemSlotSawp);

            SetCurrentItemContainer(scenarioData.IsFirstContainerGetting);

            if (!soldAll) { SetItem(currentItem); }
        }

        public void ClearItemButtons()
        {
            foreach (Transform child in buttonHolderTransform)
            {
                Destroy(child.gameObject);
            }
        }
        public void UpdateSlotDisplay()
        {
            ClearItemButtons();

            SetCurrentItemContainer(scenarioData.IsFirstContainerGetting);
        }
    }
}


