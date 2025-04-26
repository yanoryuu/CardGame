using UnityEngine;

public class AffectionUpCard : CardBase
{
    public override void PlayCard(CardPlayPresenter presenter )
    {
        presenter.ManaUp(_cardData.affectionUpNum);
    }
}