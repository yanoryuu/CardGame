using UnityEngine;
using R3;

public class CardPlayPresenter : MonoBehaviour
{
    private CardPlayModel model;
    [SerializeField] private CardPlayView view;
    [SerializeField] private CardFactory cardFactory;
    
    [SerializeField] private AngelPresenter angelPresenter;
    public AngelPresenter AngelPresenter => angelPresenter;

    private CardBase currentSelectedCard;

    private ReactiveProperty<bool> isProcessing;

    private ReactiveProperty<bool> finishPlayerTurn;
    public ReactiveProperty<bool> FinishedPlayerTurn => finishPlayerTurn;
    private void Start()
    {
        Bind();
        model = new CardPlayModel();
        isProcessing = new ReactiveProperty<bool>();
    }

    private void StartTurn()
    {
        finishPlayerTurn.Value = false;
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
        
        isProcessing.Where(x=>x==false)
            .Subscribe(_ =>
            {
                if (model.ActionPoint <= 0)
                {
                    finishPlayerTurn.Value = true;
                }
            })
            .AddTo(this);
        
    }

    //カード使用時の演出
    private void PlayCard(CardBase card,Parameters parameters = null, int playActionPoint = 1)
    {
        if (playActionPoint > model.ActionPoint)
        {
            Debug.Log("Not enough action point");
            return;
        }
        
        //コストの増減など
        model.PlayCard(card,playActionPoint);
        
        //見た目
        view.PlayCard(card);
        
        //実際の効果
        card.PlayCard(this, parameters);
    }

    //カード追加時の演出
    public void AddCard(CardScriptableObject cardDate)
    {
        var card = cardFactory.CreateCard(cardDate, view.CardParent);
        
        //カード生成用
        model.AddCard(card);
    }
}