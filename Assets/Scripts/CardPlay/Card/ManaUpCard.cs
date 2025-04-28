using UnityEngine;

public class ManaUpCard : CardBase
{
    public override void PlayCard(CardPlayPresenter presenter )
    {
        presenter.ManaUp(_cardData.affectionUpNum);
    }
}