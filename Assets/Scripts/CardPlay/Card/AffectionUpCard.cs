using UnityEngine;

public class AffectionUpCard : CardBase
{
    public override void PlayCard(CardPlayPresenter presenter , Parameters parameters)
    {
        presenter.ManaUp(_cardData.affectionUpNum);
    }
}