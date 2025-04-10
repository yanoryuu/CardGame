using System;
using UnityEngine;

namespace CardGame
{
    public class InGamePresenter : MonoBehaviour
    {
        private InGameModel model;
        
        [SerializeField]
        private InGameView view;

        private void Start()
        {
            model = new InGameModel();
        }
    } 
}