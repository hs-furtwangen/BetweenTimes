using BetweenTime.Inventory;
using Mirror;
using UnityEngine;

namespace StarterAssets.BetweenTime.Inventory.Network
{
    public class BTNetworkInventory : NetworkBehaviour, IBTInventory
    {
        private BTInventory _inventory;
        
        public bool AddItem(Item itemToAdd)
        {
            if (!isServer)
                return false;
            
            
            
        }

        public void RpcAddItem(Item itemToAdd)
        {
            
        }
                
        public bool RemoveItem(Item itemToRemove)
        {
            throw new System.NotImplementedException();
        }

        public void ClearInventory()
        {
            throw new System.NotImplementedException();
        }
    }
}