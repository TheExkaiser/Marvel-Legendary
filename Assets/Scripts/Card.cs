using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Tracing;
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
    SpriteRenderer spriteRenderer;

    [Header("Other:")]
    [SerializeField] Color playedColor;

    [Header("Stats:")]
    public bool played;//DOCELOWO PRIVATE
    public bool selected;//DOCELOWO PRIVATE
    public bool selectable = true;
    public float selectedMoveDistance;

    [Header("Hero Stats:")]
    public int heroCost;
    public int heroAttacks;
    public int heroResources;
    public int heroCardsToDraw;
    public int heroHasUnigueAbility;

    [Header("Villain Stats:")]
    public int victoryPoints;
    public int villainAttacks;
    public bool villainHasAmbushAbility;
    public bool villainHasFightAbility;
    public bool villainHasEscapeAbility;
    public bool villainHasVillainUniqueAbility;



    public enum CardLocation {None, PlayerHand, City, HQ, Discard, PlayerDeck}

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
        spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
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
            if (playerComponent.selectedCard != null)
            {
                playerComponent.selectedCard.DeselectCard();
            }

            selectable = false;
            uiManager.EnableUseCardButton(this);
            selected = true;
            playerComponent.selectedCard = this;
            transform.DOMove(new Vector3(transform.position.x, transform.position.y + selectedMoveDistance, transform.position.z), 0.3f).OnComplete(() => { selectable = true; } );
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
            transform.DOMove(new Vector3(transform.position.x, transform.position.y - selectedMoveDistance, transform.position.z), 0.3f).OnComplete(() => { if (!played) { selectable = true; } else { selectable = false; } });
        }
    }

    public void PopulateCardPrefab()
    {
        if (cardData)
        {
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
        spriteRenderer.color = Color.white;
        gameObject.SetActive(false);
    }

    public void PlayCard()
    {
        if (!played)
        {
            DeselectCard();
            EventManager.PlayerAddsAttacks(heroAttacks);
            EventManager.PlayerAddsResources(heroResources);
            transform.parent = playedCards;
            played = true;
            selectable = false;
            UpdateSortingLayer();
            playerHandAutoLayout.UpdateCardsPositions();
            playedCardsAutoLayout.UpdateCardsPositions();
            gameManager.lastCardPlayed = gameObject;
            spriteRenderer.color = playedColor;
        }
    }
    
    public void BuyCard()
    {
        playerComponent.resources -= heroCost;
        EventManager.BuyCard(this);
        DiscardCard();          
    }

    public void KOCard()
    { 
    
    }

    public void DiscardCard()
    {
        Card cardScript = this;
        cardScript.played = false;
        gameObject.transform.parent = null;
        cardScript.cardLocation = CardLocation.None;
        playerComponent.discard.Insert(0, cardData);
        transform.DOMoveX(10f, 0.2f).onComplete = RemovePrefab;
    }

    public void UpdateSortingLayer()
    { 
        sortingLayerManager = transform.parent.GetComponent<SortingLayerManager>();
        if (sortingLayerManager)
        {
            sortingLayerManager.SetSortingLayer();
        }
    }

    public void AssignCard(GameObject card)
    {
        SpriteRenderer assignedCardSpriteRenderer = card.GetComponent<SpriteRenderer>();
        card.transform.parent = gameObject.transform;
        assignedCardSpriteRenderer.sortingLayerName = spriteRenderer.sortingLayerName;
        assignedCardSpriteRenderer.sortingOrder = spriteRenderer.sortingOrder-1;
    }

}
