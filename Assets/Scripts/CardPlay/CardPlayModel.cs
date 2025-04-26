using System.Collections.Generic;
using R3;

public class CardPlayModel
{
    // 現在の保持カード
    private List<CardBase> currentHoldCard;
    public List<CardBase> CurrentHoldCard => currentHoldCard;
    
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
    private ReactiveProperty<float> currentMana;
    
    // 好感度の最大値
    private ReactiveProperty<float> maxMana;
    public ReactiveProperty<float> MaxMana => maxMana;

    // 行動回数
    private int actionPoint;
    public int ActionPoint => actionPoint;
    
    //コンストラクタ
    public CardPlayModel()
    {
        currentHoldCard = new List<CardBase>();
        playedCards = new List<CardBase>();
        onAddCard = new Subject<CardBase>();
        currentMana = new ReactiveProperty<float>();
        maxMana = new ReactiveProperty<float>();
        
        actionPoint = 3;
        maxHoldCards = 8;
    }
    
    public void AddCard(CardBase card)
    {
        if (currentHoldCard.Count >= maxHoldCards)
        {
            // 最大手札数に達していたら追加しない
            UnityEngine.Debug.LogWarning($"カードを追加できません。最大保持数({maxHoldCards})に達しています。");
            return;
        }
        
        currentHoldCard.Add(card);
        onAddCard.OnNext(card);
    }

    public void RemoveCard(CardBase card)
    {
        currentHoldCard.Remove(card);
    }

    public void PlayCard(CardBase card, int playActionPoints)
    {
        currentMana.Value -= card.CardData.playCostAffection;
        actionPoint -= playActionPoints;
        playedCards.Add(card);
    }

    public void AddMana(int affection)
    {
        currentMana.Value += affection;
    }

    public void AddActionPoint(int point)
    {
        this.actionPoint += point;
    }
}