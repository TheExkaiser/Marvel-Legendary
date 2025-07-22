using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using DG.Tweening;
using static UnityEngine.GraphicsBuffer;
using static Card;


public class GameManager : MonoBehaviour
{
    public UIManager uiManager;
    public HQManager hQManager;
    public VillainManager villainManager;
    public MastermindManager mastermindManager;
    public StateManager stateManager;
    public Transform cardsPool;
    public Transform cardSlotsPool;
    public Player player;
    public SpecialAbilities specialAbilities;
    [HideInInspector] public GameObject lastCardPlayed;
    public List<CardSO> escapedVillains;

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

    public GameObject DrawCardNoTransform(Transform deckTransform, List<CardSO> deckContents)
    {
        GameObject card = cardsPool.transform.GetChild(0).transform.gameObject;
        Card cardScript = card.GetComponent<Card>();

        card.transform.parent = null;
        card.transform.position = deckTransform.position;
        card.SetActive(true);
        cardScript.cardData = deckContents[0];
        deckContents.RemoveAt(0);
        cardScript.PopulateCardPrefab();

        return card;
    }

    public void DrawFromDeck(Transform deck, List<CardSO> deckContents, Transform target, int numberOfCards, Card.CardLocation cardLocation)
    {
        StartCoroutine(DrawFromDeckLogic(deck, deckContents, target, numberOfCards, cardLocation));
    }

    IEnumerator DrawFromDeckLogic(Transform deck, List<CardSO> deckContents, Transform target, int numberOfCards, Card.CardLocation cardLocation)
    {
        CardContainerAutoLayout containerLayout = target.gameObject.GetComponent<CardContainerAutoLayout>();
        

            for (int i = 0; i < numberOfCards; i++)
        {
            if (deckContents.Count > 0)
            {
                GameObject card = cardsPool.transform.GetChild(0).transform.gameObject;
                Card cardScript = card.GetComponent<Card>();

                card.transform.parent = target;
                if (i == numberOfCards - 1) { cardScript.UpdateSortingLayer(); }
                card.transform.position = deck.position;
                card.SetActive(true);

                cardScript.cardData = deckContents[0];
                deckContents.RemoveAt(0);
                cardScript.cardLocation = cardLocation;
                cardScript.PopulateCardPrefab();


                if (containerLayout)
                {
                    List<float> listofPosition = containerLayout.CalculateCardsPositions(numberOfCards);
                    card.transform.DOMove(new Vector3(listofPosition[i], target.position.y, target.position.z), 0.5f).OnComplete(() => { });
                    card.transform.DOScale(1.2f, 0.3f).OnComplete(() => { card.transform.DOScale(1f, 0.2f); });
                }
                else
                {
                    card.transform.DOMove(target.position, 0.5f);
                    card.transform.DOScale(1.2f, 0.3f).OnComplete(() => { card.transform.DOScale(1f, 0.2f); });
                }

                if (cardLocation == Card.CardLocation.PlayerHand)
                {
                    player.CheckDeckIfEmpty();
                    cardScript.selectable = true;
                }

                
                yield return new WaitForSeconds(0.2f);
            }

        }
    }

    public void UseCard()
    {
        selectedCard = player.selectedCard;

        if (mastermindManager.mastermindSelected)
        {
            mastermindManager.FightMastermind();
        }
        else if (selectedCard)
        {
            if (selectedCard.cardLocation == Card.CardLocation.PlayerHand)
            {
                selectedCard.PlayCard();
            }
            else if (selectedCard.cardLocation == Card.CardLocation.HQ)
            {
                selectedCard.BuyCard();
            }
            else if (selectedCard.cardLocation == Card.CardLocation.City)
            {
                selectedCard.FightCard();
            }

        }

    }

    public void GameWon()
    {
        Debug.Log("You won the game!!!");
    }

}
