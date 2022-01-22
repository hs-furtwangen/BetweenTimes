using System.Collections.Generic;
using DebugHelper;
using StarterAssets.BetweenTime.Inventory;
using StarterAssets.BetweenTime.Inventory.Network;
using UnityEngine;

namespace BetweenTime.Inventory
{
    public class BTInventory : MonoBehaviour, IBTInventory
    {        
        [SerializeField] protected List<Item> inventory = new List<Item>();
        [SerializeField] protected int maxItems = 1;

        [Header("Debug")] 
        [SerializeField] private bool showDebug;
        [SerializeField] private Color debugColor;
        
        public virtual bool AddItem(Item itemToAdd)
        {
            if (inventory.Count >= maxItems)
            {
                DebugColored.Log(showDebug,debugColor,this,"Inventory full!");
                return false;
            }
        
            inventory.Add(itemToAdd);
            DebugColored.Log(showDebug,debugColor,this,"Added "+itemToAdd.ToString());
            return true;
        }

        public virtual bool RemoveItem(Item itemToRemove)
        {
            if (inventory.Count <= 0)
            {
                DebugColored.Log(showDebug,debugColor,this,"Inventory empty!");   return false;
            }

            if (inventory.Contains(itemToRemove))
            {
                inventory.Remove(itemToRemove);
                DebugColored.Log(showDebug,debugColor,this,"Removed "+itemToRemove.ToString());
                return true;
            }
          
            DebugColored.Log(showDebug,debugColor,this,"Item not in inventory: "+itemToRemove.ToString());
            return false;
        }

        public virtual void ClearInventory()
        {
            inventory.Clear();
        }
    }
}