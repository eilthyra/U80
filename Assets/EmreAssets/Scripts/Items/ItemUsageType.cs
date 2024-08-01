using UnityEngine;

namespace OUA.Items
{
    [CreateAssetMenu(fileName = "New ItemUsageType", menuName = "Items/ItemUsageType")]
    public class ItemUsageType : ScriptableObject
    {
        [SerializeField] private new string name = "New ItemUsageType Name";
        [SerializeField] private Color colour = new Color(1f, 1f, 1f, 1f);

        public string Name => name;
        public Color Colour => colour;

    }
}
