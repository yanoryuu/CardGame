using System.Collections.Generic;
using CardGame;
using UnityEngine;

public class CardPlayPresenter : MonoBehaviour
{
    //カードプール
    [SerializeField] private List<CardScriptableObject> cards = new List<CardScriptableObject>();
    
    //現在保持しているカード
    private Dictionary<int, CardScriptableObject> currentCard = new Dictionary<int, CardScriptableObject>();
    
    
}
