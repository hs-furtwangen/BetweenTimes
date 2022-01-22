using System;
using System.Collections.Generic;
using UnityEngine;
using BT = StarterAssets.BetweenTime.Utils;
namespace StarterAssets.BetweenTime
{
    [System.Serializable]
    public class DialogueData
    {
        public string identifier;
        public string content;
    }
    [System.Serializable]
    [CreateAssetMenu(fileName = "DialogueDB", menuName = "Databases/DialogueDB", order = 1)]
    public class DialogDBSO : ScriptableObject
    {
        [SerializeField] private List<DialogueData> dialogues = new List<DialogueData>();
        
        public bool ContainsIdentifier(string identifier)
        {
            foreach (var dialogue in dialogues)
            {
                if(dialogue.identifier == identifier)
                    return true;
            }

            return false;
        }
        
        public string GetDialogue(string identifier)
        {
            foreach (var d in dialogues)
            {
                if (d.identifier == identifier)
                    return d.content;
            }

            return "NO_DIALOGUE_FOR_KEY_" + identifier;
        }
    }
}