using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UI;

namespace Ingame
{
    public class InGameView : MonoBehaviour
    {
        [SerializeField]private Button turnEndButton;
        public Button TurnEndButton => turnEndButton;

        [SerializeField] private Button talkButton;
        public Button TalkButton => talkButton;
        
        public void Initialize()
        {
            
        }
    }
}