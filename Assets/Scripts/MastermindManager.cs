using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using NaughtyAttributes;
using UnityEngine;

public class MastermindManager : MonoBehaviour
{
    [SerializeField] GameManager gameManager;
    Player player;
    UIManager uiManager;
    public int masterMindHP;
    public CardSetSO masterMindDeck;
    public List<CardSO> tacticsDeck;
    [SerializeField] GameObject mastermindGO;
    public bool mastermindSelected;

    GameObject card;
    SpriteRenderer cardSpriteRenderer;


    // Start is called before the first frame update
    void Start()
    {
        player = gameManager.player;
        uiManager = gameManager.uiManager;
        SetupMasterind();
    }

    public void SetupMasterind()
    {
        mastermindGO.GetComponent<SpriteRenderer>().sprite = masterMindDeck.cards[0].image;
        CreateTacticsDeck();
        UpdateMastermindHP();
    }

    public void CreateTacticsDeck()
    {
        for (int i = 1; i < masterMindDeck.cards.Count; i++)
        {
            tacticsDeck.Add(masterMindDeck.cards[i]);
        }
        gameManager.Shuffle(tacticsDeck);
    }

    public void UpdateMastermindHP()
    {
        masterMindHP = tacticsDeck.Count;
    }

    public void clickOnMastermind()
    {
        if (player.attacks >= tacticsDeck[0].villainAttacks)
        {
            mastermindSelected = true;
            uiManager.EnableUseCardButtonFightMastermind();
        }
    }

    public void FightMastermind()
    {
        Debug.Log("Fight mastermind dzia³a!");
        player.attacks -= tacticsDeck[0].villainAttacks;
        gameManager.DrawFromDeck(mastermindGO.transform, tacticsDeck, mastermindGO.transform, 1, Card.CardLocation.None);
        card = mastermindGO.transform.GetChild(0).gameObject;
        cardSpriteRenderer = card.GetComponent<SpriteRenderer>();
        cardSpriteRenderer.sortingLayerName = "ShownCard";

        Sequence mySequence = DOTween.Sequence();
        mySequence.Append(card.transform.DOMove(new Vector3(0f, 0f, card.transform.position.z), 1f));
        mySequence.Join(card.transform.DOScale(new Vector3(2f, 2f, 0f), 1f));
        mySequence.AppendInterval(3f);
        mySequence.Append(card.transform.DOMoveX(20f, 0.5f));
        mySequence.Join(card.transform.DOScale(new Vector3(1f, 1f, 1f), 0.5f));
        
        mySequence.AppendCallback(() =>
        {
            player.victoryPool.Add(card.GetComponent<Card>().cardData);
            card.GetComponent<Card>().RemovePrefab();
            card = null;
            mastermindSelected = false;


        });

        
    }
}
