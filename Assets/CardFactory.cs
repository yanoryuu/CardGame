using UnityEngine;

public class CardFactory : MonoBehaviour
{
    [SerializeField] private AttackCard attackCardPrefab;
    [SerializeField] private BuffCard buffCardPrefab;
    [SerializeField] private DefenceCard defenceCardPrefab;

    public CardBase CreateCard(CardScriptableObject data, Transform parent)
    {
        CardBase prefab = data.cardType switch
        {
            CardScriptableObject.cardTypes.Attack => attackCardPrefab,
            CardScriptableObject.cardTypes.Buff => buffCardPrefab,
            CardScriptableObject.cardTypes.Defence => defenceCardPrefab,
            _ => null
        };

        if (prefab == null)
        {
            Debug.LogError("Card prefab not found for type: " + data.cardType);
            return null;
        }

        return Instantiate(prefab, parent);
    }
}