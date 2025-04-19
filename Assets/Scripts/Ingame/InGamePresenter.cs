using System;
using System.Reflection;
using R3;
using UnityEngine;

namespace CardGame
{
    public class InGamePresenter : MonoBehaviour
    {
        private InGameModel model;
        
        [SerializeField]
        private InGameView view;
        
        [SerializeField]
        private CardPlayPresenter cardPlayPresenter;
        
        private InGameEnum.GameState currentIngameState;
        
        //現在のターン
        private int currentTurn;

        private void Start()
        {
            model = new InGameModel();
            
            currentIngameState = InGameEnum.GameState.DrawCards;
        }

        private void Bind()
        {
            cardPlayPresenter.FinishedPlayerTurn.Where(x=>x)
                .Subscribe(_ =>
                {
                    currentTurn++;
                })
                .AddTo(this);
        }

        private InGameEnum.GameState ChangeState(InGameEnum.GameState state)
        {
            switch (state)
            {
                case InGameEnum.GameState.DrawCards:
                    return InGameEnum.GameState.DrawCards;
            }
            return state;
        }
    } 
}