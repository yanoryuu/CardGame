using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CardPlayView : MonoBehaviour
{
    [SerializeField] private GameObject cardParent;
    [SerializeField] private Button playButton;

    public Button PlayButton => playButton;
    public Transform CardParent => cardParent.transform;

    private List<CardBase> currentCards = new List<CardBase>();

    public void ShowCard() => cardParent.SetActive(true);
    public void HideCard() => cardParent.SetActive(false);

    public void AddCard(CardBase card)
    {
        currentCards.Add(card);
        card.ShowCard();
    }

    //カードをプレイするときの演出はここに
    public void PlayCard(CardBase card)
    {
        
    }

    public void ReturnCard(CardBase card)
    {
        card.HideCard();
        currentCards.Remove(card);
    }
}