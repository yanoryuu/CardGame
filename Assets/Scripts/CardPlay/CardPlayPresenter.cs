using System.Collections.Generic;
using CardGame;
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
        
        //追加効果
        if (card.CardData.additionalEffect != null) card.PlayAdditionalEffect(this, parameters);
    }

    //カード追加時の演出
    public void AddCard(CardScriptableObject cardDate)
    {
        var card = cardFactory.CreateCard(cardDate, view.CardParent);
        
        //カード生成用
        model.AddCard(card);
    }
    
    //カード削除
    public void RemoveCard(CardBase cardDate)
    {
        model.RemoveCard(cardDate);
    }
    
    //ランダムでチョイス
    public CardScriptableObject SelectRandomCard(List<CardScriptableObject> cards)
    {
        return cards[Random.Range(0, cards.Count)];
    }
    
    //好感度アップ
    public void AffectionUp(int affection)
    {
        model.AddAffection(affection);   
    }

    //手札総入れ替え
    public void HandSwap()
    {
        for (int i = 0; i > model.CurrentHoldCard.Count; i++)
        {
            AddCard(SelectRandomCard(CardPool.Instance.cardpool));
        }
    }

    //選択したカードタイプを選択
    public List<CardScriptableObject> CollectTargetCardType(CardScriptableObject.cardTypes cardType)
    {
        List<CardScriptableObject> targetCardList = new List<CardScriptableObject>();
        foreach (var card in CardPool.Instance.cardpool)
        {
            if (card.cardType == cardType)
            {
                targetCardList.Add(card);
            }
        }
        return targetCardList;
    }
    
    //手札にある選択したカードタイプを選択
    public List<CardBase> CollectTargetCardTypeHoldCards(CardScriptableObject.cardTypes cardType)
    {
        List<CardBase> targetCardList = new List<CardBase>();
        foreach (var card in model.CurrentHoldCard)
        {
            if (card.CardData.cardType == cardType)
            {
                targetCardList.Add(card);
            }
        }
        return targetCardList;
    }
    

    //アクションポイントを増加
    public void AddActionPoint(int actionPoint)
    {
        model.AddActionPoint(actionPoint);
    }
    
    //デートモードに以降
    public void GoToDate()
    {
        
    }
}