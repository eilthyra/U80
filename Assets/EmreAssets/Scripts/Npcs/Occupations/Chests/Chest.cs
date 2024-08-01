using OUA.Events.CustomEvents;
using OUA.Items;
using UnityEngine;

namespace OUA.Npcs.Occupations.Chests
{
    public class Chest : MonoBehaviour, IOccupation
    {
        [SerializeField] private ChestDataEvent onStartChestScenario = null;

        public string Name => "Store your Stuff";

        private IItemContainer itemContainer = null;

        private void Start() => itemContainer = GetComponent<IItemContainer>();

        public void Trigger(GameObject other)
        {
            var otherItemContainer = other.GetComponent<IItemContainer>();

            if (otherItemContainer == null) { return; }

            ChestData chestData = new ChestData(otherItemContainer, itemContainer);

            onStartChestScenario.Raise(chestData);
        }
    }
}
