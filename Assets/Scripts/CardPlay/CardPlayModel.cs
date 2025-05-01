using System;
using System.Collections.Generic;
using R3;
using UnityEngine;

/// <summary>
/// ゲーム中のカードプレイに関するモデル
/// </summary>
public class CardPlayModel
{
    // 現在の保持カード
    private ReactiveProperty<List<CardBase>> currentHoldCard;
    public ReactiveProperty<List<CardBase>> CurrentHoldCard => currentHoldCard;
    
    // 現在の保持カードの数
    private ReactiveProperty<int> currentHoldCardIndex;
    public ReactiveProperty<int> CurrentHoldCardIndex => currentHoldCardIndex;
    
    // 最大カード保持数
    private int maxHoldCards; // 初期値を仮で5に設定（必要なら外から設定可能にしてもOK）
    public int MaxHoldCards => maxHoldCards;
    
    // 墓地カード
    private List<CardBase> playedCards;
    public List<CardBase> PlayedCards => playedCards;

    // 新しく追加されたカードを通知する
    private Subject<CardBase> onAddCard;
    public Observable<CardBase> OnAddCard => onAddCard;
    
    // マナの最大値の最大値（例えば装備などで拡張できる最大上限）
    private ReactiveProperty<int> manaMaxCap;
    public ReactiveProperty<int> ManaMaxCap => manaMaxCap;
    // プレイヤーのステータス（ReactiveProperty使用）
    private PlayerParameterRuntime playerParameter;
    public PlayerParameterRuntime PlayerParameter => playerParameter;
    
    // コンストラクタ
    public CardPlayModel()
    {
        // プレイヤーパラメータの初期化（ReactiveProperty）
        playerParameter = new PlayerParameterRuntime();

        // 保持カード、インデックスなどの初期化
        currentHoldCard = new ReactiveProperty<List<CardBase>>(new List<CardBase>());
        currentHoldCardIndex = new ReactiveProperty<int>(0);
        playedCards = new List<CardBase>();
        onAddCard = new Subject<CardBase>();
        manaMaxCap = new ReactiveProperty<int>(CardPlayConst.maxManaCap);
            
        playerParameter.ActionPoint.Value = 3; // 初期AP設定
        maxHoldCards = CardPlayConst.maxHoldCardNum;
    }

    // カードを追加
    public void AddCard(CardBase card)
    {
        if (currentHoldCard.Value.Count >= maxHoldCards)
        {
            // 最大手札数に達していたら追加しない
            Debug.LogWarning($"カードを追加できません。最大保持数({maxHoldCards})に達しています。");
            return;
        }

        currentHoldCardIndex.Value++;
        currentHoldCard.Value.Add(card);
        onAddCard.OnNext(card);
    }

    // カードを削除
    public void RemoveCard(CardBase card)
    {
        currentHoldCard.Value.Remove(card);
        currentHoldCardIndex.Value--;
    }

    // カードプレイ時の処理（マナ消費、AP消費、墓地送り）
    public void PlayCard(CardBase card, int playActionPoints)
    {
        playerParameter.CurrentMana.Value -= card.CardData.playCostAffection;
        playerParameter.ActionPoint.Value -= playActionPoints;
        playedCards.Add(card);
        
        Debug.Log($"残りのMana: {playerParameter.CurrentMana.Value}");
    }

    // マナを増やす
    public void AddMana(int affection)
    {
        playerParameter.CurrentMana.Value += affection;
    }

    // 行動ポイントを追加
    public void AddActionPoint(int point)
    {
        playerParameter.ActionPoint.Value += point;
    }

    // 初期化（ターン開始や再スタート用）
    public void Initialize()
    {
        Debug.Log("Initialize");
        playerParameter.MaxMana.Value = CardPlayConst.initMaxMana;
        playerParameter.CurrentMana.Value = playerParameter.MaxMana.Value;
        playerParameter.ActionPoint.Value = 3;
    }
}