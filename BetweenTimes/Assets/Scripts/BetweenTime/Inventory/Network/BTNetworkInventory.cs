using System.Collections.Generic;
using BetweenTime.Inventory;
using DebugHelper;
using Mirror;
using UnityEngine;

namespace StarterAssets.BetweenTime.Inventory.Network
{
    public class BTNetworkInventory : NetworkBehaviour
    {
        [SerializeField] protected List<Item> _inventory = new List<Item>();
        [SyncVar] public int maxItems = 1;
        
        [Header("Debug")] 
        [SerializeField] private bool showDebug;
        [SerializeField] private Color debugColor;
        
        [Client]
        public void AddItem(Item itemToAdd)
        {
            if (!isLocalPlayer)
                return;
            
            CmdAddItem(itemToAdd);
        }
    
        [Command]
        public void CmdAddItem(Item itemToAdd)
        {            
            if (_inventory.Count >= maxItems)
            {
                DebugColored.Log(true, Color.yellow,"[Server]", this, "Inventory full!");
                RpcAddItem(false, itemToAdd);
                return;
            }
            
            _inventory.Add(itemToAdd);
            DebugColored.Log(true, Color.yellow,"[Server]", this, "Added Item "+itemToAdd.ToString());
            RpcAddItem(true, itemToAdd);
        }
        
        [ClientRpc]
        public void RpcAddItem(bool success, Item itemToAdd)
        {
            if (success)
            {
                _inventory.Add(itemToAdd);
                DebugColored.Log(true, Color.yellow, "[Rpc]", this, "Added Item " + itemToAdd.ToString());
            }else
                DebugColored.Log(true, Color.yellow, "[Rpc]", this, "Failed to add Item " + itemToAdd.ToString());

        }
                
        [Client]
        public void RemoveItem(Item itemToRemove)
        {
            if (!isLocalPlayer)
                return;
            

            CmdAddItem(itemToRemove);
        }

        [Command]
        public void CmdRemoveItem(Item itemToRemove)
        {           
            if (_inventory.Count <= 0)
            {
                DebugColored.Log(showDebug,debugColor,this,"Inventory empty!");   
                return;
            }
            
            if (_inventory.Contains(itemToRemove))
            {
                bool success = _inventory.Remove(itemToRemove);
                DebugColored.Log(showDebug,debugColor,this,"Removed "+itemToRemove.ToString());
                if (success)
                {
                    RpcCmdRemoveItem(true, itemToRemove);
                }else
                    RpcCmdRemoveItem(false, itemToRemove);
                return ;
            }
           
            RpcCmdRemoveItem(false, itemToRemove);
            DebugColored.Log(true, Color.yellow,"[Server]", this, "Failed to remove Item "+itemToRemove.ToString());
        }

        [ClientRpc]
        public void RpcCmdRemoveItem(bool success, Item itemToRemove)
        {
            if (success)
            {
                _inventory.Remove(itemToRemove);
                DebugColored.Log(true, Color.yellow, "[Client]", this,
                    "Removed Item " + itemToRemove.ToString());
            }
            else
            {
                DebugColored.Log(true, Color.yellow, "[Client]", this,
                    "Failed to remove Item " + itemToRemove.ToString());   
            }
        }
        
        public void ClearInventory()
        {
            CmdClearInventory();
        }

        [Command]
        public void CmdClearInventory()
        {
            _inventory.Clear();
            DebugColored.Log(true, Color.yellow,"[Server]", this, "Cleared Inventory" );
            RpcInventory();
        }

        [ClientRpc]
        public void RpcInventory()
        {
            _inventory.Clear();
            DebugColored.Log(true, Color.yellow,"[Client]", this, "Cleared Inventory" );

        }
    }
}