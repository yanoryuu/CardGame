using UnityEngine;

public class DatingCard : CardBase
{
    public override void PlayCard(CardPlayPresenter presenter)
    {
        // 処理はここに記述
        presenter.GoToDate();
    }
}