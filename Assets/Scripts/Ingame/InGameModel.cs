using UnityEngine;

namespace CardGame
{
    public class InGameModel 
    {
        
        private InGameEnum.GameState currentIngameState;
        public InGameEnum.GameState CurrentIngameState => currentIngameState;
        
        public InGameModel()
        {
            currentIngameState = InGameEnum.GameState.Default;
        }

        public void ChangeState(InGameEnum.GameState state)
        {
            currentIngameState = state;
        }
    }

}