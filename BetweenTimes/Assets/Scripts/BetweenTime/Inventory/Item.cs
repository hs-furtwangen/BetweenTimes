using BetweenTime.Controlling;
using UnityEngine;

namespace StarterAssets.BetweenTime.Inventory
{
    [System.Serializable]
    public class Item
    {
        public int id;
        public string name;
        public GameObject visualsPrefab;
        public GameObject reference;

        public string ToString()
        {
            return "Item:\nID: " + id + "\nName: " + name + "\nVisuals: " +
                   ((visualsPrefab != null) ? visualsPrefab.name : "none");
        }
    }
}