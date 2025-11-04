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

    private void Start()
    {
        cardsToDraw = defaultCardsToDraw;
        uiManager = gameManager.uiManager;
    }

    private void OnEnable()
    {
        EventManager.OnPlayerDrewCardsFromDeck += DrawCard;
        EventManager.OnPlayerAddsAttacks += AddAttacks;
        EventManager.OnPlayerAddsResources += AddResources;
        EventManager.OnEndPlayerTurn += DiscardHand;
        EventManager.OnEndPlayerTurn += DiscardPlayedCards;
        EventManager.OnEndPlayerTurn += ResetAttacks;
        EventManager.OnEndPlayerTurn += ResetResources;
        EventManager.OnEndPlayerTurn += DrawNewHand;
    }

    private void OnDisable()
    {
        EventManager.OnPlayerDrewCardsFromDeck -= DrawCard;
        EventManager.OnPlayerAddsAttacks -= AddAttacks;
        EventManager.OnPlayerAddsResources -= AddResources;
        EventManager.OnEndPlayerTurn -= DiscardHand;
        EventManager.OnEndPlayerTurn -= DiscardPlayedCards;
        EventManager.OnEndPlayerTurn -= ResetAttacks;
        EventManager.OnEndPlayerTurn -= ResetResources;
        EventManager.OnEndPlayerTurn -= DrawNewHand;

    }

    public void SelectCard(Card card)
    {
        if (card.selectable)
        {
            if (selectedCard != null)
            {
                DeselectCard();
            }

            cardTransform = card.transform;

            card.selectable = false;
            uiManager.EnableUseCardButton(card);
            card.selected = true;
            selectedCard = card;
            card.gameObject.transform.DOMove(new Vector3(cardTransform.position.x, cardTransform.position.y + card.selectedMoveDistance, cardTransform.position.z), 0.3f).OnComplete(() => { card.selectable = true; });
        }

    }

    public void DeselectCard()
    {
        if (selectedCard)
        {
            cardTransform = selectedCard.transform;
            selectedCard.selectable = false;
            selectedCard.selected = false;
            uiManager.DisableUseCardButton();
            cardTransform.DOMove(new Vector3(cardTransform.position.x, cardTransform.position.y - selectedCard.selectedMoveDistance, cardTransform.position.z), 0.3f).OnComplete(() =>
            {
                if (!selectedCard.played)
                {
                    selectedCard.selectable = true;
                }
                else
                {
                    selectedCard.selectable = false;
                }

                selectedCard = null;

            });
        }
    }

    public void PlayCard()
    {
        AddAttacks(selectedCard.heroAttacks);
        AddResources(selectedCard.heroResources);
        selectedCard.transform.parent = playedCards;
        uiManager.DisableUseCardButton();

        selectedCard.played = true;
        selectedCard.selectable = false;
        selectedCard.UpdateSortingLayer();
        playerHand.GetComponent<CardContainerAutoLayout>().UpdateCardsPositions();
        playedCards.GetComponent<CardContainerAutoLayout>().UpdateCardsPositions();
        gameManager.lastCardPlayed = gameObject;
        selectedCard.GetComponent<SpriteRenderer>().color = selectedCard.playedColor;
        if (selectedCard.heroSpecialAbilities.Count > 0)
        {
            for (int i = 0; i < card.heroSpecialAbilities.Count; i++)
            {
                selectedCard.heroSpecialAbilities[i].GetComponent<ISpecialAbility>().useAbility();
            }
        }

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
        EventManager.PlayerDrawCardsFromDeck(cardsToDraw);
        EventManager.PlayerDrawNewHand();
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
