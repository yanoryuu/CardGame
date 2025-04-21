using CardGame;
using UnityEngine;

public class SearchCard : CardBase
{
    public override void PlayCard(CardPlayPresenter presenter , Parameters parameters)
    {
        CardScriptableObject cardScriptableObject;
        
        cardScriptableObject = presenter.SelectRandomCard(presenter.CollectTargetCardType(_cardData.cardType));
        
        presenter.AddCard(cardScriptableObject);
    }
}