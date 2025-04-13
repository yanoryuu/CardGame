using UnityEngine;
using R3;

public class CardPlayPresenter : MonoBehaviour
{
    [SerializeField] private CardPlayModel model;
    [SerializeField] private CardPlayView view;
    [SerializeField] private CardFactory cardFactory;

    private CardBase currentSelectedCard;

    private void Start()
    {
        Bind();
    }

    private void Bind()
    {
        model.OnAddCard
            .Subscribe(cardData =>
            {
                // プレハブをFactory経由で生成
                cardData.SetCard(cardData.CardData); // データをセット

                cardData.cardButton.OnClickAsObservable()
                    .Subscribe(_ =>
                    {
                        currentSelectedCard = cardData;
                    })
                    .AddTo(cardData);

                // ビューに追加
                view.AddCard(cardData);
            })
            .AddTo(this);

        view.PlayButton.OnClickAsObservable()
            .Subscribe(_ =>
            {
                if (currentSelectedCard != null)
                {
                    PlayCard(currentSelectedCard);
                }
            })
            .AddTo(this);
    }

    //カード使用時の演出
    private void PlayCard(CardBase card)
    {
        card.PlayCard();
    }

    //カード追加時の演出
    public void AddCard(CardScriptableObject cardDate)
    {
        var card = cardFactory.CreateCard(cardDate, view.CardParent);
        
        //カード生成用
        model.AddCard(card);
    }
}