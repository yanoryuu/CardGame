using System.Collections.Generic;
using R3;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class CardPlayModel
{
    // 現在の保持カード
    private ReactiveProperty<List<CardBase>> currentHoldCard;
    public ReactiveProperty<List<CardBase>> CurrentHoldCard => currentHoldCard;
    
    // 最大カード保持数
    private int maxHoldCards; // 初期値を仮で5に設定（必要なら外から設定可能にしてもOK）
    public int MaxHoldCards => maxHoldCards;
    
    // 墓地カード
    private List<CardBase> playedCards;
    public List<CardBase> PlayedCards => playedCards;

    // 新しく追加されたカードを通知する
    private Subject<CardBase> onAddCard;
    public Observable<CardBase> OnAddCard => onAddCard;
    
    // 現在の好感度（マナ）
    private ReactiveProperty<int> currentMana;
    public ReactiveProperty<int> CurrentMana => currentMana;
    
    // マナの最大値
    private ReactiveProperty<int> maxMana;
    public ReactiveProperty<int> MaxMana => maxMana;
    
    //マナの最大値の最大値
    private ReactiveProperty<int> manaMaxCap;
    public ReactiveProperty<int> ManaMaxCap => manaMaxCap;

    // 行動回数
    private ReactiveProperty<int> actionPoint;
    public ReactiveProperty<int> ActionPoint => actionPoint;
    
    //コンストラクタ
    public CardPlayModel()
    {
        currentHoldCard = new ReactiveProperty<List<CardBase>>(new List<CardBase>());
        playedCards = new List<CardBase>();
        onAddCard = new Subject<CardBase>();
        currentMana = new ReactiveProperty<int>();
        maxMana = new ReactiveProperty<int>();
        manaMaxCap = new ReactiveProperty<int>(CardPlayConst.maxManaCap);
        actionPoint = new ReactiveProperty<int>();
        
        actionPoint.Value = 3;
        maxHoldCards = 8;
    }
    
    public void AddCard(CardBase card)
    {
        if (currentHoldCard.Value.Count >= maxHoldCards)
        {
            // 最大手札数に達していたら追加しない
            UnityEngine.Debug.LogWarning($"カードを追加できません。最大保持数({maxHoldCards})に達しています。");
            return;
        }
        
        currentHoldCard.Value.Add(card);
        onAddCard.OnNext(card);
    }

    public void RemoveCard(CardBase card)
    {
        currentHoldCard.Value.Remove(card);
    }

    public void PlayCard(CardBase card, int playActionPoints)
    {
        currentMana.Value -= card.CardData.playCostAffection;
        actionPoint.Value -= playActionPoints;
        playedCards.Add(card);
        
        Debug.Log($"残りのMana{currentMana.Value}");
    }

    public void AddMana(int affection)
    {
        currentMana.Value += affection;
    }

    public void AddActionPoint(int point)
    {
        this.actionPoint.Value += point;
    }

    public void Initialize()
    {
        Debug.Log("Initialize");
        maxMana.Value = CardPlayConst.initMaxMana;
        currentMana.Value = maxMana.Value;
        actionPoint.Value = 3;
    }
}