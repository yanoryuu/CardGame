using UnityEngine;

public class HandSwapCard : CardBase
{
    public override void PlayCard(CardPlayPresenter presenter)
    {
        presenter.HandSwap();
    }
}