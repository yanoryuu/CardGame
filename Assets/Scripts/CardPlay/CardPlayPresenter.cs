using System.Collections.Generic;
using CardGame;
using UnityEngine;
using R3;
using UnityEngine.UI;

public class CardPlayPresenter : MonoBehaviour
{
    private CardPlayModel model;
    
    [SerializeField] private CardPlayView view;
    
    [SerializeField] private CardFactory cardFactory;
    
    [SerializeField] private AngelPresenter angelPresenter;
    public AngelPresenter AngelPresenter => angelPresenter;

    //現在選択中のカード
    private CardBase currentSelectedCard;

    private ReactiveProperty<bool> isProcessing;
    
    //デート中（デート中は告白成功率アップ）
    private ReactiveProperty<bool> isDate;
    public ReactiveProperty<bool> IsDate => isDate;

    //カップル状態
    private ReactiveProperty<bool> isCouple;
    public ReactiveProperty<bool> IsCouple => isCouple;
    
    private void Start()
    {
        Bind();
        model = new CardPlayModel();
        isProcessing = new ReactiveProperty<bool>();
        isDate = new ReactiveProperty<bool>();
    }

    private void Bind()
    {
        model.OnAddCard
            .Subscribe(cardData =>
            {
                cardData.cardButton.OnClickAsObservable()
                    .Where(_ => InGameManager.Instance.CurrentState == InGameEnum.GameState.PlayerTurn)
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
    
    //ゲーム開始時のドロー
    public void StartDrawCards(List<CardScriptableObject.cardTypes> cardData,int drawCount)
    {
        for (int i = 0; i < drawCount; i++)
        {
            AddCard(SelectRandomCard(CollectTargetCardType(cardData)));
        }
    }

    //カード使用時の演出
    private void PlayCard(CardBase card, int playActionPoint = 1)
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
        card.PlayCard(this);
        
        //追加効果
        if (card.CardData.additionalEffect != null) card.PlayAdditionalEffect(this);
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
    
    //マナアップ
    public void ManaUp(int affection)
    {
        model.AddMana(affection);   
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
    public List<CardScriptableObject> CollectTargetCardType(List<CardScriptableObject.cardTypes> cardType)
    {
        List<CardScriptableObject> targetCardList = new List<CardScriptableObject>();
        foreach (var card in CardPool.Instance.cardpool)
        {
            foreach (var Type in cardType)
            {
                if (card.cardType == Type)
                {
                    targetCardList.Add(card);
                    break;
                }
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
        IsDate.Value = true;
    }

    public void FinishDate()
    {
        IsDate.Value = false;
    }
    
    //告白成功
    public void BeCouple()
    {
        isCouple.Value = true;
    }
    
    //カップル別れる
    public void BreackCouple()
    {
        isCouple.Value = false;
    }
    //
    // //継続カードの実装
    // public void StartPersistet(int persistTrun,CardBase card)
    // {
    //     InGameManager.Instance.CurrentTurn.Take(persistTrun)
    //         .Subscribe(_ => card.PlayAdditionalEffect();
    // }
}