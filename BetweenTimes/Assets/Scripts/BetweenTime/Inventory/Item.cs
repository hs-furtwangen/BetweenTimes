using BetweenTime.Controlling;
using UnityEngine;

namespace StarterAssets.BetweenTime.Inventory
{
    public class Item
    {
        public int id;
        public string name;
        public GameObject visualsPrefab;


        public string ToString()
        {
            return "Item:\nID: " + id + "\nName: " + name + "\nVisuals: " +
                   ((visualsPrefab != null) ? visualsPrefab.name : "none");
        }
    }
}