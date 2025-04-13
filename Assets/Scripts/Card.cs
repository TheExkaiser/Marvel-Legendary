using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class Card : MonoBehaviour, IClickable
{
    [Header("References:")]
    public GameManager gameManager;
    public CardSO cardData;
    Transform cardsPool;
    GameObject player;
    Player playerComponent;
    GameObject playerHand;
    Transform playedCards;
    SortingLayerManager sortingLayerManager;
    HQManager hQManager;
    UIManager uiManager;
    CardContainerAutoLayout playerHandAutoLayout;
    CardContainerAutoLayout playedCardsAutoLayout;
    Transform hqSlot;

    [Header("Stats:")]
    bool played;//DOCELOWO PRIVATE
    public bool selected;//DOCELOWO PRIVATE
    public bool selectable = true;
    public float selectedMoveDistance;

    [Header("Hero Stats:")]
    public int heroCost;
    public int heroAttacks;
    public int heroResources;
    public int heroCardsToDraw;



    public enum CardLocation {None, PlayerHand, City, HQ, Discard, PlayerDeck }

    public CardLocation cardLocation;

    void Start()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        player = gameManager.player.gameObject;
        playerComponent = gameManager.player;
        playerHand = playerComponent.playerHand.gameObject;
        playedCards = playerComponent.playedCards;
        cardsPool = gameManager.cardsPool;
        uiManager = gameManager.uiManager;
        playerHandAutoLayout = playerHand.GetComponent<CardContainerAutoLayout>();
        playedCardsAutoLayout = playedCards.gameObject.GetComponent<CardContainerAutoLayout>();
        hQManager = gameManager.hQManager;
    }

    private void OnEnable()
    {
    }

    private void OnDisable()
    {
    }

    public void OnClick()
    {
        Debug.Log("CLICKED");
        if (!selected)
        {
            if (playerComponent.selectedCard != null)
            {
                playerComponent.selectedCard.DeselectCard();
            }
            
            SelectCard();
        }
        else
        { 
            DeselectCard();
        }
        
    }
    void SelectCard()
    {
        if (selectable)
        {
            selectable = false;
            uiManager.EnableUseCardButton(this);
            selected = true;
            playerComponent.selectedCard = this;
            transform.DOMove(new Vector3(transform.position.x, transform.position.y + selectedMoveDistance, transform.position.z), 0.3f).OnComplete(SetSelectableTrue);
        }
        
    }

    public void DeselectCard()
    {
        if (selectable)
        {
            selectable = false;
            selected = false;
            playerComponent.selectedCard = null;
            uiManager.DisableUseCardButton();
            transform.DOMove(new Vector3(transform.position.x, transform.position.y - selectedMoveDistance, transform.position.z), 0.3f).OnComplete(SetSelectableTrue);
        }
        
    }

    public void PopulateCardPrefab()
    {
        if (cardData)
        {
            Debug.Log("PopulateCardPrefab");
            gameObject.GetComponent<SpriteRenderer>().sprite = cardData.image;
            heroCost = cardData.heroCost;
            heroAttacks = cardData.heroAttacks;
            heroResources = cardData.heroRecruitPoints;
            heroCardsToDraw = cardData.heroCardsToDraw;
        }
    }

    public void RemovePrefab()
    {
        gameObject.transform.parent = cardsPool;
        gameObject.SetActive(false);
    }

    public void PlayCard()
    {
        Debug.Log("Karta zagrana");
        
        if (!played)
        {
            DeselectCard();
            EventManager.PlayerAddsAttacks(heroAttacks);
            EventManager.PlayerAddsResources(heroResources);
            transform.parent = playedCards;
            played = true;
            UpdateSortingLayer();
            playerHandAutoLayout.UpdateCardsPositions();
            playedCardsAutoLayout.UpdateCardsPositions();
            gameManager.lastCardPlayed = gameObject;
        }
    }
    
    public void BuyCard()
    {
        Debug.Log("Karta kupiona");
        hqSlot = transform.parent;
        DiscardCard();
        playerComponent.resources -= heroCost;
        uiManager.UpdateResourcesText();
        hQManager.UpdateSlot(hqSlot);
    }

    public void KOCard()
    { 
    
    }

    public void DiscardCard()
    {
        Card cardScript = this;
        transform.DOMoveX(10f, 0.5f);
        cardScript.played = false;
        cardScript.cardLocation = CardLocation.None;
        playerComponent.discard.Insert(0, cardData);
        cardScript.RemovePrefab();
    }

    public void UpdateSortingLayer()
    { 
        sortingLayerManager = transform.parent.GetComponent<SortingLayerManager>();
        if (sortingLayerManager)
        {
            sortingLayerManager.SetSortingLayer();
        }
    }

    void SetSelectableTrue()
    {
        selectable = true;
    }

}
