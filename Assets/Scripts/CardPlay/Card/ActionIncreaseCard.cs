public class ActionIncreaseCard : CardBase
{
    public override void PlayCard(CardPlayPresenter presenter , Parameters parameters)
    {
        presenter.AddActionPoint(_cardData.addAP);
    }
}