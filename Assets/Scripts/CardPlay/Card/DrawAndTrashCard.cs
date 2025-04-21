using CardGame;
using UnityEngine;

public class DrawAndTrashCard : CardBase
{
    public override void PlayCard(CardPlayPresenter presenter , Parameters parameters)
    {
        presenter.RemoveCard(this);
        presenter.AddCard(presenter.SelectRandomCard(CardPool.Instance.cardpool));
    }
}