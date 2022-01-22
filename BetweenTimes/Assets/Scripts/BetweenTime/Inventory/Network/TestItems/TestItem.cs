using System;
using System.Collections;
using System.Collections.Generic;
using Mirror;
using StarterAssets.BetweenTime.Inventory;
using StarterAssets.BetweenTime.Inventory.Network;
using UnityEngine;

public class TestItem : NetworkBehaviour
{
   [SerializeField] private Item testItem;
   [SerializeField] private BTNetworkInventory _inventory;

[ContextMenu("Add on client")]
   public void AddItemOnClient()
   {
      if (!isClient)
         return;

      _inventory.AddItem(testItem);

   }
   [ContextMenu("Add on Server")]
   public void AddItemOnServer()
   {
      if (!isServer)
         return;

      _inventory.ServerAddItemLogic(testItem);

   }
   [ContextMenu("Remove on client")]
   public void RemoveItemClient()
   {
      if (!isClient)
         return;

      _inventory.RemoveItem(testItem);

   }
   [ContextMenu("Remove on Server")]
   public void RemoveItemServer()
   {
      if (!isServer)
         return;

      _inventory.ServerRemoveItemLogic(testItem);

   }
   [ContextMenu("Clear on client")]
   public void ClearInventoryOnClient()
   {
      if (!isClient)
         return;

      _inventory.ClearInventory();
   }
   [ContextMenu("Clear on Server")]
   public void ClearInventoryOnServer()
   {
      if (!isServer)
         return;

      _inventory.ServerClearInventoryLogic();
   }
}
