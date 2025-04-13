using System;
using System.Collections.Generic;
using R3;
using UnityEngine;

public class CardPlayModel : MonoBehaviour
{
    private Queue<CardBase> currentHoldCard = new Queue<CardBase>();
    public Queue<CardBase> CurrentHoldCard => currentHoldCard;

    // 新しく追加されたカードを通知する
    private Subject<CardBase> onAddCard = new Subject<CardBase>();
    public Observable<CardBase> OnAddCard => onAddCard;

    public void AddCard(CardBase card)
    {
        currentHoldCard.Enqueue(card);
        onAddCard.OnNext(card);
    }
}
