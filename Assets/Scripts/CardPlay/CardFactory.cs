using UnityEngine;

public class CardFactory : MonoBehaviour
{
    [Header("Card Prefabs")]
    [SerializeField] private AffectionUpCard affectionUpCardPrefab;
    [SerializeField] private PersistentCard persistentCardPrefab;
    [SerializeField] private HandSwapCard handSwapCardPrefab;
    [SerializeField] private DebuffCard debuffCardPrefab;
    [SerializeField] private CardExchangeCard cardExchangeCardPrefab;
    [SerializeField] private DrawAndTrashCard drawAndTrashCardPrefab;
    [SerializeField] private SearchCard searchCardPrefab;
    [SerializeField] private NoAffectionPenaltyCard noAffectionPenaltyCardPrefab;
    [SerializeField] private ReturnFromGraveCard returnFromGraveCardPrefab;
    [SerializeField] private ActionIncreaseCard actionIncreaseCardPrefab;
    [SerializeField] private ParameterBoostCard parameterChangeCardPrefab;
    [SerializeField] private CostBypassCard costBypassCardPrefab;
    [SerializeField] private TrashDiscardCard trashDiscardCardPrefab;
    [SerializeField] private DatingCard datingCardPrefab;

    public CardBase CreateCard(CardScriptableObject date, Transform parent)
    {
        CardBase prefab = date.cardType switch
        {
            CardScriptableObject.cardTypes.ManaUp => affectionUpCardPrefab,
            CardScriptableObject.cardTypes.Persistent => persistentCardPrefab,
            CardScriptableObject.cardTypes.HandSwap => handSwapCardPrefab,
            CardScriptableObject.cardTypes.Debuff => debuffCardPrefab,
            CardScriptableObject.cardTypes.CardExchange => cardExchangeCardPrefab,
            CardScriptableObject.cardTypes.DrawAndTrash => drawAndTrashCardPrefab,
            CardScriptableObject.cardTypes.Search => searchCardPrefab,
            CardScriptableObject.cardTypes.NoAffectionPenalty => noAffectionPenaltyCardPrefab,
            CardScriptableObject.cardTypes.ReturnFromGrave => returnFromGraveCardPrefab,
            CardScriptableObject.cardTypes.ActionIncrease => actionIncreaseCardPrefab,
            CardScriptableObject.cardTypes.ParameterChange => parameterChangeCardPrefab,
            CardScriptableObject.cardTypes.TrashDiscard => trashDiscardCardPrefab,
            CardScriptableObject.cardTypes.Dating => datingCardPrefab,
            _ => null
        };

        if (prefab == null)
        {
            Debug.LogError("Card prefab not found for type: " + date.cardType);
            return null;
        }

        var card = Instantiate(prefab, parent.position, parent.rotation, parent);
        card.SetCard(date);
        return card;
    }
}
