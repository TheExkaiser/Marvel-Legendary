using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UIElements.Experimental;

public class Player : MonoBehaviour
{
    [SerializeField] GameManager gameManager;
    [SerializeField] UIManager uiManager;
    public Transform playerHand;
    public Transform playerDeck;
    public List<CardSO> deckContents;

    public Transform playedCards;
    public int defaultCardsToDraw;
    public int cardsToDraw;

    public int resources;
    public int attacks;
    public int victoryPoints;

    public List<CardSO> discard;
    public List<CardSO> teleport;
    public List<CardSO> cardsPlayedList;

    Card card;
    int playerHandCardsCount;
    int playedCardsCount;

    private void Start()
    {
        cardsToDraw = defaultCardsToDraw;
    }

    private void Update()
    {
        
    }

    public void DrawCard(int number) 
    {
        gameManager.DrawFromDeckLogic(playerDeck, deckContents, playerHand, number);
        CheckDeckIfEmpty();
    }

    public void DrawNewHand()
    {
        DrawCard(cardsToDraw);          
    }

    public void DiscardHand()
    {
        playerHandCardsCount = playerHand.childCount;
        for (int i = 0; i < playerHandCardsCount; i++)
        {
            card = playerHand.GetChild(0).GetComponent<Card>();
            card.DiscardCard();
        }
    }

    public void DiscardPlayedCards()
    {
        Debug.Log("Dsicardplayed dzia³a");
        playedCardsCount = playedCards.childCount;
        for (int i = 0; i < playedCardsCount; i++)
        {
            card = playedCards.GetChild(0).GetComponent<Card>();
            card.DiscardCard();
        }
    }

    public void AddAttacks(int value) 
    {
        attacks += value;
        uiManager.UpdateAttacksText();
    }

    public void AddResources(int value) 
    { 
        resources += value;
        uiManager.UpdateResourcesText();
    }

    public void ResetAttacks() 
    {
        Debug.Log("resetA");
        attacks = 0; 
        uiManager.UpdateAttacksText();
    }

    public void ResetResources()
    {
        Debug.Log("resetR");

        resources = 0;
        uiManager.UpdateResourcesText();
    }


    public void RescueBystander(int number)
    {
        Debug.Log($"Player rescued {number} bystanders!");
    }

    private void CheckDeckIfEmpty()
    { 
        if (deckContents.Count == 0) 
        {
            Debug.Log("Player's Deck is empty...");
            ShuffleDiscardIntoDeck(); 
        }
    }

    public void ShuffleDiscardIntoDeck()
    {
        Debug.Log("Suffling Discard into Player's Deck");
        if (discard.Count > 0)
        {
            for (int i = 0; i < discard.Count; i++)
            {
                deckContents.Add(discard[i]);
            }
            discard.Clear();
            gameManager.Shuffle(deckContents);
        }
    }

    
}
