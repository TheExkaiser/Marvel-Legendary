using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements.Experimental;
using static Card;

public class Player : MonoBehaviour
{
    [SerializeField] GameManager gameManager;
    [SerializeField] UIManager uiManager;
    public Transform playerHand;
    public Transform playerDeck;
    public List<CardSO> deckContents;

    public Transform playedCards;
    public Card selectedCard;
    public int defaultCardsToDraw;
    public int cardsToDraw;

    public int resources;
    public int attacks;
    public int victoryPoints;

    public List<CardSO> discard;
    public List<CardSO> teleport;
    public List<CardSO> cardsPlayedList;
    public List<CardSO> victoryPool;

    Card card;
    Transform cardTransform;
    int playerHandCardsCount;
    int playedCardsCount;
    Card previouslySelectedCard;

    public event Action OnDrawNewHandEnd;
    public event Action OnAddsAttacks;
    public event Action OnAddsRecources;

    private void Start()
    {
        cardsToDraw = defaultCardsToDraw;
        uiManager = gameManager.uiManager;
    }

    private void OnEnable()
    {
    }

    private void OnDisable()
    {
    }

    public void SelectCard(Card card)
    {
        if (card.selectable)
        {
            gameManager.DisableInteractions();

            DeselectCard();

            cardTransform = card.transform;

            card.selectable = false;
            uiManager.EnableUseCardButton(card);
            selectedCard = card;
            card.gameObject.transform.DOMove(new Vector3(cardTransform.position.x, cardTransform.position.y + card.selectedMoveDistance, cardTransform.position.z), 0.3f).OnComplete(() => 
            { 
                card.selected = true;
                gameManager.EnableInteractions(); 
            });
        }

    }

    public void DeselectCard()
    {
        if (selectedCard)
        {
            gameManager.DisableInteractions();
            selectedCard.selected = false;
            previouslySelectedCard = selectedCard;
            cardTransform = previouslySelectedCard.transform;
            previouslySelectedCard.selectable = false;
            previouslySelectedCard.selected = false;
            uiManager.DisableUseCardButton();
            cardTransform.DOMove(new Vector3(cardTransform.position.x, cardTransform.position.y - previouslySelectedCard.selectedMoveDistance, cardTransform.position.z), 0.3f).OnComplete(() =>
            {
                if (!previouslySelectedCard.played)
                {
                    previouslySelectedCard.selectable = true;
                }
                else
                {
                    previouslySelectedCard.selectable = false;
                }

                previouslySelectedCard = null;
                selectedCard = null;

                gameManager.EnableInteractions();

            });
        }
    }

    public void PlayCard()
    {
        selectedCard.transform.parent = playedCards;
        uiManager.DisableUseCardButton();
  
        selectedCard.played = true;
        selectedCard.selectable = false;
        selectedCard.UpdateSortingLayer();
        playerHand.GetComponent<CardContainerAutoLayout>().UpdateCardsPositions();
        playedCards.GetComponent<CardContainerAutoLayout>().UpdateCardsPositions();
        gameManager.lastCardPlayed = gameObject;
        selectedCard.GetComponent<SpriteRenderer>().color = selectedCard.playedColor;

        AddAttacks(selectedCard.heroAttacks);
        OnAddsAttacks?.Invoke();
        AddResources(selectedCard.heroResources);
        OnAddsRecources?.Invoke();
        if (selectedCard.heroSpecialAbility) selectedCard.heroSpecialAbility.useAbility();


        selectedCard = null;
    }

    public void DiscardCard(Card card)
    {
        card.played = false;
        card.transform.parent = null;
        card.cardLocation = CardLocation.None;
        discard.Insert(0, card.cardData);
        card.transform.DOMoveX(10f, 0.2f).onComplete = card.RemovePrefab;
    }

    public void DrawCard(int number) 
    {
        if (number > 0)
        {
            gameManager.DrawFromDeck(playerDeck, deckContents, playerHand, number, Card.CardLocation.PlayerHand);
        }
        
    }

    public void DrawNewHand()
    {
        gameManager.OnLastCardDrawn += OnDrawNewHandEndInvoke;
        DrawCard(cardsToDraw);

    }

    public void OnDrawNewHandEndInvoke()
    {
        OnDrawNewHandEnd?.Invoke();
        gameManager.OnLastCardDrawn -= OnDrawNewHandEndInvoke;
    }

    public void DiscardHand()
    {
        playerHandCardsCount = playerHand.childCount;
        for (int i = 0; i < playerHandCardsCount; i++)
        {
            card = playerHand.GetChild(0).GetComponent<Card>();
            DiscardCard(card);
        }
    }

    public void DiscardPlayedCards()
    {
        playedCardsCount = playedCards.childCount;
        for (int i = 0; i < playedCardsCount; i++)
        {
            card = playedCards.GetChild(0).GetComponent<Card>();
            DiscardCard(card); ;
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

    public void CheckDeckIfEmpty()
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
