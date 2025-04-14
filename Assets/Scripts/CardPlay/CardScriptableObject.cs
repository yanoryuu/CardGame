using UnityEngine;

[CreateAssetMenu(fileName = "Card", menuName = "Card/CreateCardAsset")]
public class CardScriptableObject : ScriptableObject
{
    public readonly int playCost;
    
    public readonly Sprite cardSprite;
    
    public readonly int cardPower;
    
    public readonly cardTypes cardType;
    
    public enum cardTypes
    {
        Attack,
        Buff,
        Defence,
    }
}