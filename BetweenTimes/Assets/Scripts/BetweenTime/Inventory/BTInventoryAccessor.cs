using System;
using System.Collections;
using System.Collections.Generic;
using DebugHelper;
using StarterAssets.BetweenTime.Inventory;
using StarterAssets.BetweenTime.Inventory.Network;
using UnityEngine;
using UnityEngine.Events;

public class BTInventoryAccessor : MonoBehaviour
{
    [SerializeField] private IBTInventory inventory;
        
    [Header("Debug")] 
    [SerializeField] private bool showDebug;
    [SerializeField] private Color debugColor;

    #region  Events

    public UnityEvent<Item> EventOnItemAdded = new UnityEvent<Item>();
    public UnityEvent<Item> EventOnItemRemoved = new UnityEvent<Item>();
    public UnityEvent EventOnInventoryClear;

    #endregion Events
    
    public bool AddItem(Item itemToAdd)
    {
        if (inventory.AddItem(itemToAdd))
        {
            EventOnItemAdded?.Invoke(itemToAdd);
            return true;
        }
        else
            return false;

    }

    public bool RemoveItem(Item itemToRemove)
    {
        if (inventory.RemoveItem(itemToRemove))
        {
            EventOnItemRemoved?.Invoke(itemToRemove);
            return true;
        }
        else
            return false;
    }

    public void ClearInventory()
    {
        inventory.ClearInventory();
        DebugColored.Log(showDebug,debugColor,this,"Inventory cleared!");
        EventOnInventoryClear?.Invoke();
    }
}
