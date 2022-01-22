using System;
using DebugHelper;
using UnityEngine;

namespace StarterAssets.BetweenTime
{
    public class DialogueController : MonoBehaviour
    {
        [SerializeField] private DialogDBSO _dialogDB;

        private static DialogueController instance;

        public static DialogueController GetInstance()
        {
            return instance;
        }
        

        public bool ContainsIdentifier(string identifier)
        {
            if (_dialogDB == null)
            {
                DebugColored.LogError(this,"There is no DialogueDB assigned!");
                return false;
            }

            return _dialogDB.ContainsIdentifier(identifier);
        }
        
        public string GetDialogue(string identifier)
        {
            if (_dialogDB == null)
            {
                DebugColored.LogError(this,"There is no DialogueDB assigned!");
                return "NO_DIALOGUE_DB_ASSIGNED";
            }

            return _dialogDB.GetDialogue(identifier);
        }
    }
}