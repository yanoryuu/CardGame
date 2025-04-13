using UnityEngine;

[CreateAssetMenu(fileName = "Card", menuName = "Card/CreateCardAsset")]
public class CardScriptableObject : ScriptableObject
{
    public int playCost;
    
    public Sprite cardSprite;
    
    public int cardPower;
    
    public cardTypes cardType;
    
    public enum cardTypes
    {
        Attack,
        Buff,
        Defence,
    }
}