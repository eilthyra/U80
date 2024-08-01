using System;
using System.Drawing;
using UnityEngine;

namespace OUA.Items.Inventories
{
    public abstract class InventoryItem : Item
    {
        [Header("Item Data")]
        [SerializeField] protected ItemUsageType itemUsageType = null;
        [SerializeField][Min(0)] private int sellPrice = 1;
        [SerializeField][Min(1)] private int maxStack = 1;
        [SerializeField] private int foodRate = 0;
        public override string ColouredName
        {
            get
            {
                string hexColour = ColorUtility.ToHtmlStringRGB(itemUsageType.Colour);
                return $"<color=#{hexColour}>{Name}</color>";
            }
        }

        public override string ColouredType
        {
            get
            {
                string hexColour = ColorUtility.ToHtmlStringRGB(itemUsageType.Colour);
                return $"<color=#{hexColour}>{itemUsageType.Name}</color>";
            }
        }

        public int SellPrice => sellPrice;
        public int MaxStack => maxStack;
        public ItemUsageType ItemUsageType => itemUsageType;
        public int FoodRate => foodRate;
    }
}
