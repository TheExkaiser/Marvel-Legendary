using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using DG.Tweening;
using static UnityEngine.GraphicsBuffer;
using static Card;
using System;
using JetBrains.Annotations;


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
    [Header("")]

    [Header("Focus View")]
    [SerializeField] Transform boardZone;
    [SerializeField] GameObject boardZoneRaycastBlocker;
    [SerializeField] Vector3 boardZoneUnfocusedPosition;
    [SerializeField] Vector3 boardZoneFocusedPosition;
    public bool boardZoneFocused;
    public event Action OnBoardFocused;

    [SerializeField] Transform playerZone;
    [SerializeField] GameObject playerZoneRaycastBlocker;
    [SerializeField] Vector3 playerZoneUnfocusedPosition;
    [SerializeField] Vector3 playerZoneFocusedPosition;
    public bool playerZoneFocused;
    public event Action OnPlayerFocused;
    [Header("")]


    Card selectedCard;
    ClickHandler clickHandler;

    public event Action OnLastCardDrawn;

    private void Awake()
    {
        clickHandler = GetComponent<ClickHandler>();
        EnableInteractions();
        
    }

    public void Shuffle(List<CardSO> deck)
    {
        // Knuth shuffle algorithm :: courtesy of Wikipedia :)
        for (int t = 0; t < deck.Count; t++)
        {
            CardSO tmp = deck[t];
            int r = UnityEngine.Random.Range(t, deck.Count);
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
                if (i == numberOfCards - 1)
                {
                    OnLastCardDrawn?.Invoke();
                }

            }

        }
    }

    public void EnableInteractions()
    {
        clickHandler.enabled = true;

    }

    public void DisableInteractions() 
    {
        clickHandler.enabled = false;
    }

    public void FocusBoard()
    {
        DisableInteractions();
        boardZoneRaycastBlocker.gameObject.SetActive(false);
        UnFocusPlayer();
        boardZone.DOMove(boardZoneFocusedPosition, 1f).OnComplete(()=>
            {
                boardZoneFocused = true;
                OnBoardFocused?.Invoke();
                EnableInteractions();
            });
    }

    public void UnFocusBoard()
    {
        DisableInteractions();
        boardZoneRaycastBlocker.gameObject.SetActive(true);
        boardZoneFocused = false;
        boardZone.DOMove(boardZoneUnfocusedPosition, 1f).OnComplete(()=>EnableInteractions());
    }

    public void FocusPlayer()
    {
        DisableInteractions();
        playerZoneRaycastBlocker.gameObject.SetActive(false);
        UnFocusBoard();
        playerZone.DOMove(playerZoneFocusedPosition, 1f).OnComplete(() => 
            {
                playerZoneFocused = true;
                OnPlayerFocused?.Invoke(); 
                EnableInteractions();
            });
    }

    public void UnFocusPlayer()
    {
        DisableInteractions();
        playerZoneRaycastBlocker.gameObject.SetActive(true);
        playerZoneFocused = false;
        playerZone.DOMove(playerZoneUnfocusedPosition, 1f).OnComplete(() => EnableInteractions());
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
                player.PlayCard();
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
