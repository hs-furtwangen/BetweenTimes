using System;
using UnityEngine;

namespace BetweenTime.Player.Quests
{
    [Serializable]
    public class QuestTester : Questcontroller
    {
        [SerializeField] private string taskToken;
        [SerializeField] private string questToken;
        
        [ContextMenu("Try task")]
        public void TaskTestComplete()
        {
            SetTaskComplete(taskToken);
        }    
        
        [ContextMenu("Try Quest")]
        public void QuestTestComplete()
        {
            SetQuestComplete(questToken);
        } 
        
        [ContextMenu("Reset Quests")]
        public void ResetTest()
        {
            Reset();
        }
        
        [ContextMenu("All Quests")]
        public void AllQuestsCompleteTest()
        {
            SetAllQuestsComplete();
        }
    }
}