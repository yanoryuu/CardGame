using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class CardPlayView : MonoBehaviour
{
    [SerializeField] private GameObject cardParent;
    public Transform CardParent => cardParent.transform;
    
    [SerializeField] private Button playButton;
    public Button PlayButton => playButton;

    [SerializeField] private List<Image> apStars;
    public List<Image> ApStars => apStars;
    
    public void ShowCard() => cardParent.SetActive(true);
    public void HideCard() => cardParent.SetActive(false);

    public void AddCard(CardBase card)
    {
        card.ShowCard();
    }

    //カードをプレイするときの演出はここに
    public void PlayCard(CardBase card)
    {
        
    }

    public void RemoveCard(CardBase card)
    {
        Destroy(card.gameObject);
    }

    public void ReturnCard(CardBase card)
    {
        card.HideCard();
    }

    public void ConfigCard(List<CardBase> cards)
    {
        // 親オブジェクトの中心からカードを並べるイメージ
        float spacing = 150f; // カード同士の間隔（適宜調整）
        float startX = -(cards.Count - 1) * spacing * 0.5f; // 最初のカードのX位置

        for (int i = 0; i < cards.Count; i++)
        {
            var card = cards[i];
            card.transform.localPosition = new Vector3(startX + i * spacing, 0, 0); // 横に並べる
            card.transform.localRotation = Quaternion.identity; // 回転リセット（必要なら）
        }
    }

    public void SetApStars(int restAp)
    {
        foreach (var ap in apStars)
        {
            ap.enabled = false;
        }
        
        for (int i = 0; i < restAp; i++)
        {
            var star = ApStars[i];
            star.enabled = true;
        }
    }
}