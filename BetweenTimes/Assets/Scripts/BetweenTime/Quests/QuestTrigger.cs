using System;
using UnityEngine;

namespace BetweenTime.Player.Quests
{
    public class QuestTrigger : MonoBehaviour
    {
        private Questcontroller _questcontroller;
        [SerializeField] private string token;
        [SerializeField] private TokenType _tokenType;
        
        public void ExecuteQuest()
        {
            if(_questcontroller == null) _questcontroller = Questcontroller.GetInstance();
            
            if(_questcontroller == null)
                return;

            if(_tokenType == TokenType.Task)
            _questcontroller.SetTaskComplete(token);
            else if(_tokenType == TokenType.Quest)
                _questcontroller.SetQuestComplete(token);
            
        }
    }
}