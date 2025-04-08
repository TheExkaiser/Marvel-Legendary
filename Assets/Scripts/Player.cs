using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UIElements.Experimental;

public class Player : MonoBehaviour
{
    [SerializeField] GameManager gameManager;
    [SerializeField] UIManager uiManager;
    [SerializeField] GameObject cardsPool;
    [SerializeField] GameObject cardSlotsPool;
    public GameObject playerHand;
    public CardContainerAutoLayout playerHandManager;
    public Transform playedCards;
    [SerializeField] CardSetSO startingDeck;
    public int resources;
    public int attacks;
    public int victoryPoints;
    public RectTransform deckTransform;

    public List<CardSO> deck;
    public List<CardSO> discard;
    public List<CardSO> teleport;
    public List<CardSO> cardsPlayedList;

    private void Start()
    {
        StartGame();
    }
    private void Update()
    {
        CheckDeckIfEmpty();
    }

    void StartGame()
    {
        CreateStartingDeck();
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


    public void RescueBystander(int number)
    {
        Debug.Log($"Player rescued {number} bystanders!");
    }

    public void DrawCard(int number, List<CardSO> deck)
    {
        if (deck.Count > 0)
        {
            GameObject card = cardsPool.transform.GetChild(0).transform.gameObject;
            RectTransform cardTransform = card.GetComponent<RectTransform>();
            Card cardScript = card.GetComponent<Card>();

            cardTransform.parent = playerHand.transform;
            cardTransform.localPosition = deckTransform.localPosition;
            card.SetActive(true);

            playerHandManager.UpdateCardsPositions();


            cardScript.cardData = deck[0];
            cardScript.PopulateCardPrefab();

            deck.RemoveAt(0);

        }
    }
    private void CheckDeckIfEmpty()
    { 
        if (deck.Count == 0) 
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
                deck.Add(discard[i]);
            }
            discard.Clear();
            gameManager.Shuffle(deck);
        }
    }

    public void CreateStartingDeck()
    { 
        for (int i = 0; i< startingDeck.cards.Count; i++) 
        {
            deck.Add(startingDeck.cards[i]);
        }
    }
}
