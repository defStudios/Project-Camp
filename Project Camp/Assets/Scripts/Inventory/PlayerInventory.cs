using System.Collections.Generic;
using Inventory.Items;

namespace Inventory
{
    public class PlayerInventory
    {
        public System.Action InventoryStateChanged;

        public List<IItem> Items { get; private set; }
        
        public PlayerInventory()
        {
            Items = new List<IItem>();
        }
        
        public void AddItem(IItem item)
        {
            if (Items.Contains(item))
                return;
            
            Items.Add(item);
            InventoryStateChanged?.Invoke();
        }

        public int GetArtifactsCount() => 
            Items.FindAll(x => x is ArtifactItem).Count;
    }
}
