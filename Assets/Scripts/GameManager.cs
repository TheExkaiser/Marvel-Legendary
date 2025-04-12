using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using DG.Tweening;


public class GameManager : MonoBehaviour
{
    public UIManager uiManager;
    public HQManager hQManager;
    public StateManager stateManager;
    public Transform cardsPool;
    public Transform cardSlotsPool;
    public Player player;
    public SpecialAbilities specialAbilities;
    [HideInInspector] public GameObject lastCardPlayed;

    Card selectedCard;

    public void Shuffle(List<CardSO> deck)
    {
        // Knuth shuffle algorithm :: courtesy of Wikipedia :)
        for (int t = 0; t < deck.Count; t++)
        {
            CardSO tmp = deck[t];
            int r = Random.Range(t, deck.Count);
            deck[t] = deck[r];
            deck[r] = tmp;
        }
    }

    public void AddCardToHQ(Transform slot) 
    {
        Debug.Log("AddCardToHQ dzia³a");
    }

    public void DrawFromDeck(string name) 
    {
        if (name == "hqDeck")
        {
            
        }
                
    }

    public void DrawFromDeckLogic(Transform deck, List<CardSO> deckContents, Transform target, int numberOfCards, Card.CardLocation cardLocation)
    {
        CardContainerAutoLayout containerLayout = target.gameObject.GetComponent<CardContainerAutoLayout>();
        for (int i = 0; i < numberOfCards; i++)
        {
            if (deckContents.Count > 0)
            {
                GameObject card = cardsPool.transform.GetChild(0).transform.gameObject;
                Card cardScript = card.GetComponent<Card>();

                card.transform.parent = target;
                card.transform.position = deck.position;
                card.SetActive(true);

                cardScript.cardData = deckContents[0];
                cardScript.cardLocation = cardLocation;
                cardScript.PopulateCardPrefab();

                if (containerLayout)
                {
                    containerLayout.UpdateCardsPositions();
                }
                else
                {
                    card.transform.DOMove(target.position, 0.5f);
                }

                deckContents.RemoveAt(0);

                if (cardLocation == Card.CardLocation.PlayerHand)
                {
                    player.CheckDeckIfEmpty();
                }

                if (i == numberOfCards - 1) { cardScript.UpdateSortingLayer(); }
            }

        }
            }

    public void UseCard()
    {
        selectedCard = player.selectedCard;
        if (selectedCard)
        {
            if (selectedCard.cardLocation == Card.CardLocation.PlayerHand)
            {
                selectedCard.PlayCard();
            }
            else if (selectedCard.cardLocation == Card.CardLocation.HQ)
            {
                selectedCard.BuyCard();
            }

        }

    }

}
