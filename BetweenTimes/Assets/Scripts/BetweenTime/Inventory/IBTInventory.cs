using System.Collections.Generic;
using DebugHelper;
using UnityEngine;

namespace StarterAssets.BetweenTime.Inventory.Network
{
    public interface IBTInventory 
    {
        public abstract bool AddItem(Item itemToAdd);
        public abstract bool RemoveItem(Item itemToRemove);
        public abstract void ClearInventory();
    }
}