using UnityEngine;

public class AttackCard : CardBase
{
    public override void PlayCard(CardPlayPresenter presenter , Parameters parameters)
    {
        presenter.AngelPresenter.UpdateAngel(parameters);
    }
}
