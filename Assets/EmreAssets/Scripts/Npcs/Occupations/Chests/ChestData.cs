using OUA.Items;

namespace OUA.Npcs.Occupations.Chests
{
    public class ChestData
    {
        public ChestData(
            IItemContainer invetoryItemContainer,
            IItemContainer chestItemContainer)
        {
            itemContainers[0] = invetoryItemContainer;
            itemContainers[1] = chestItemContainer;
        }

        private IItemContainer[] itemContainers = new IItemContainer[2];
        public bool IsFirstContainerGetting { get; set; } = true;

        public IItemContainer InventoryItemContainer => IsFirstContainerGetting ? itemContainers[0] : itemContainers[1];
        public IItemContainer ChestItemContainer => IsFirstContainerGetting ? itemContainers[1] : itemContainers[0];
    }
}
