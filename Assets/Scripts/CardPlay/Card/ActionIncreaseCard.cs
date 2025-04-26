public class ActionIncreaseCard : CardBase
{
    public override void PlayCard(CardPlayPresenter presenter)
    {
        presenter.AddActionPoint(_cardData.addAP);
    }
}