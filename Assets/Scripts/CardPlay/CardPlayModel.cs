using System.Collections.Generic;
using R3;

public class CardPlayModel
{
    private Queue<CardBase> currentHoldCard = new Queue<CardBase>();
    public Queue<CardBase> CurrentHoldCard => currentHoldCard;

    // 新しく追加されたカードを通知する
    private Subject<CardBase> onAddCard = new Subject<CardBase>();
    public Observable<CardBase> OnAddCard => onAddCard;
    
    private ReactiveProperty<float> currentLikeability = new ReactiveProperty<float>();

    private int actionPoint = 3;
    public int ActionPoint => actionPoint;
    
    public CardPlayModel()
    {
        currentLikeability = new ReactiveProperty<float>();
        currentHoldCard = new Queue<CardBase>();
        onAddCard = new Subject<CardBase>();
    }
    
    public void AddCard(CardBase card)
    {
        currentHoldCard.Enqueue(card);
        onAddCard.OnNext(card);
    }

    public void PlayCard(CardBase card,int playActionPoints)
    {
        currentLikeability.Value -= card.CardData.playCost;

        actionPoint -= playActionPoints;
    }
}