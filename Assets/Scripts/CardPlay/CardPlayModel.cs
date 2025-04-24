using System.Collections.Generic;
using R3;
using UnityEngine.Rendering;

public class CardPlayModel
{
    //現在の保持カード
    private List<CardBase> currentHoldCard = new List<CardBase>();
    public List<CardBase> CurrentHoldCard => currentHoldCard;
    
    //最大カード保持数
    private int maxHoldCards;
    public int MaxHoldCards => maxHoldCards;
    
    //墓地カード
    private List<CardBase> playedCards = new List<CardBase>();
    public List<CardBase> PlayedCards => playedCards;

    // 新しく追加されたカードを通知する
    private Subject<CardBase> onAddCard = new Subject<CardBase>();
    public R3.Observable<CardBase> OnAddCard => onAddCard;
    
    //現在の好感度（マナ）
    private ReactiveProperty<float> currentMana = new ReactiveProperty<float>();
    
    //好感度の最大値
    private ReactiveProperty<float> maxMana = new ReactiveProperty<float>();
    public ReactiveProperty<float> MaxMana => maxMana;

    //行動回数
    private int actionPoint = 3;
    public int ActionPoint => actionPoint;
    
    public CardPlayModel()
    {
        currentMana = new ReactiveProperty<float>();
        currentHoldCard = new List<CardBase>();
        onAddCard = new Subject<CardBase>();
    }
    
    public void AddCard(CardBase card)
    {
        currentHoldCard.Add(card);
        onAddCard.OnNext(card);
    }

    public void RemoveCard(CardBase card)
    {
        currentHoldCard.Remove(card);
    }

    public void PlayCard(CardBase card,int playActionPoints)
    {
        currentMana.Value -= card.CardData.playCostAffection;

        actionPoint -= playActionPoints;
        
        playedCards.Add(card);
    }

    public void AddMana(int affection)
    {
        maxMana.Value += affection;
    }

    public void AddActionPoint(int actionPoint)
    {
        actionPoint += actionPoint;
    }
}