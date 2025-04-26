using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Card", menuName = "Card/CreateCardAsset")]
public class CardScriptableObject : ScriptableObject
{
    /// <summary>
    /// 基本的なカード情報
    /// </summary>
    //追加効果が必要であればアタッチ
    [SerializeReference]
    public List<AdditionalEffect> additionalEffect = new List<AdditionalEffect>();
    
    //プレイに必要な好感度
    public int playCostAffection = 1;
    
    //プレイに必要なアクションポイント
    public int playActionPoints = 1; 
    
    public  cardTypes cardType;
    
    public  Sprite cardSprite;
    
    /// <summary>
    /// カードの種類ごとの効果値
    /// </summary>
    /// 
    //好感度を上げる
    public  int affectionUpNum = 0;
    
    //継続ターン数
    public int keepTurns = 1;
    
    //探すカードの種類
    public List<cardTypes> searchCardType = new List<cardTypes>();
    
    //追加するActionPoint
    public int addAP = 2; 
    
    //効果を与えるターゲットパラメーター
    public Parameters addParameterNum;
    
    [Serializable]
    public enum cardTypes
    {
        Debuff,             // 弱体（デバフ）カード（＝ゴミカード）

        // パラメータ・ステータス系
        ManaUp,        // 好感度増加
        ParameterChange,     // パラメータ上昇（弱・強）を包括
        ActionIncrease,     // 行動回数を増やす

        // 持続・状態系
        Persistent,         // 持続型（ターン継続型など）
        Dating,             // デートカード（特定条件で効果）

        // 手札操作系
        HandSwap,           // 手札入れ替え
        DrawAndTrash,       // 1ドロー1デス（1枚引いて1枚捨て）
        TrashDiscard,       // ゴミカードを捨てるカード
        CardExchange,       // カード交換（他カードと交換）

        // 墓地操作
        ReturnFromGrave,    // 墓地カードを手札に戻す

        // サポート・特殊
        Search,             // サーチ（指定カード入手）
        NoAffectionPenalty, // 次カードの好感度減少を防ぐ
    }
}