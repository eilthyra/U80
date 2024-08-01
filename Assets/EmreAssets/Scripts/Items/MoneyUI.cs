using OUA.Items.Inventories;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace OUA.Items.Inventories
{
    public class MoneyUI : MonoBehaviour
    {
        [SerializeField] private TMP_Text moneyText; // TextMeshPro referansý
        [SerializeField] private Inventory inventory; // Inventory referansý

        private void OnEnable()
        {
            // Event'e abone ol
            if (inventory != null)
            {
                inventory.OnMoneyChanged.AddListener(UpdateMoneyText);
            }
        }

        private void OnDisable()
        {
            // Event'ten aboneliði kaldýr
            if (inventory != null)
            {
                inventory.OnMoneyChanged.RemoveListener(UpdateMoneyText);
            }
        }

        private void Start()
        {
            // Baþlangýçta UI'yi güncelle
            if (inventory != null)
            {
                UpdateMoneyText(inventory.Money);
            }
        }

        private void UpdateMoneyText(int newMoneyValue)
        {
            if (moneyText != null)
            {
                moneyText.text = ": " + newMoneyValue.ToString() + "$";
            }
        }
    }
}