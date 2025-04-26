using CardGame;
using UnityEngine;

public class SearchCard : CardBase
{
    public override void PlayCard(CardPlayPresenter presenter , Parameters parameters)
    {
        CardScriptableObject cardScriptableObject = new CardScriptableObject();
        
        cardScriptableObject = presenter.SelectRandomCard(presenter.CollectTargetCardType(_cardData.searchCardType));
        
        presenter.AddCard(cardScriptableObject);
    }
}