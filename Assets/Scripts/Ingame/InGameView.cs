using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UI;

namespace Ingame
{
    public class InGameView : MonoBehaviour
    {
        [SerializeField]private Button turnEndButton;
        public Button TurnEndButton => turnEndButton;
        
        public void Initialize()
        {
            
        }
    }
}