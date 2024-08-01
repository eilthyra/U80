using OUA.Items.Hotbars;
using OUA.Items.Inventories;
using System;
using System.Text;
using UnityEngine;

namespace OUA.Items
{
    [CreateAssetMenu(fileName = "New Consumable Item", menuName = "Items/Consumable Item")]
    public class ConsumableItem : InventoryItem, IHotbarItem
    {
        [Header("Consumable Data")]
        [SerializeField] private string useText = "Does something, maybe?";

        public override string GetInfoDisplayText()
        {
            StringBuilder builder = new StringBuilder();

            builder.Append(ColouredType).AppendLine();
            if(ItemUsageType.Name == "Edible")
            {
                builder.Append("<color=#996633>Food Rate: ").Append(FoodRate).AppendLine("</color>");
            }
            builder.Append(useText).AppendLine();
            builder.Append("Max Stack: ").Append(MaxStack).AppendLine();
            builder.Append("Sell Price: ").Append(SellPrice).Append(" Gold");

            return builder.ToString();
        }

        public void Use()
        {
            Debug.Log($"Drinking {Name}");
        }
    }

}
