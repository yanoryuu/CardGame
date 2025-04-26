using UnityEngine;

public class TrashDiscardCard : CardBase
{
    public override void PlayCard(CardPlayPresenter presenter)
    {
        var DebuffCards = presenter.CollectTargetCardTypeHoldCards(CardScriptableObject.cardTypes.Debuff);

        foreach (var card in DebuffCards)
        {
            presenter.RemoveCard(card);
        }
    }
}