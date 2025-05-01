using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEditor;
using UnityEngine;
using UnityEngine.Android;
using UnityEngine.UI;

public class CardPlayView : MonoBehaviour
{
    [SerializeField] private GameObject cardParent;
    public Transform CardParent => cardParent.transform;
    
    [SerializeField] private Button playButton;
    public Button PlayButton => playButton;

    [SerializeField] private List<Image> apStars;

    [SerializeField] private Image manaVar;

    [SerializeField] private Image manaMaxVar;

    //カードの感覚
    [SerializeField] private float spacing = 150f;
    
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
        Debug.Log(restAp);
        foreach (var ap in apStars)
        {
            ap.enabled = false;
        }
        
        for (int i = 0; i < restAp; i++)
        {
            var star = apStars[i];
            star.enabled = true;
        }
    }

    public void SetManaVar(int currentMana,float maxManaCap)
    {
        Debug.Log(currentMana/maxManaCap);
        manaVar.fillAmount = currentMana / maxManaCap;
    }

    public void SetMaxManaVar(float currentMaxMana, float maxManaCap)
    {   
        Debug.Log(currentMaxMana / maxManaCap);
        manaMaxVar.fillAmount = currentMaxMana / maxManaCap;
    }
}