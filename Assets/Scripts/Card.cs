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
    public Color playedColor;
    float assignedCardOffsetX;
    float assignedCardOffsetY;
    public List<GameObject> assignedCards;


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
    public List<GameObject> heroSpecialAbilities;

    [Header("Villain Stats:")]
    public int victoryPoints;
    public int villainAttacks;
    public bool villainHasAmbushAbility;
    public bool villainHasFightAbility;
    public bool villainHasEscapeAbility;
    public bool HasVillainUniqueAbility;



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

        assignedCardOffsetX = 0.1f;
        assignedCardOffsetY = 0.1f;
    }

    private void OnEnable()
    {
    }

    private void OnDisable()
    {
    }

    public void OnClick()
    {
        if (!selected)
        {
            playerComponent.SelectCard(this);
        }
        else
        {
            playerComponent.DeselectCard();
        }
        
    }

    public void OnHold()
    {
        gameManager.uiManager.PopulateCardInfoPanel(cardData);
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
            villainAttacks = cardData.villainAttacks;
            villainHasFightAbility = cardData.hasAmbushAbility;
            villainHasEscapeAbility = cardData.hasEscapeAbility;
            villainHasFightAbility = cardData.hasFightAbility;
            HasVillainUniqueAbility = cardData.hasVillainUniqueAbility;
            victoryPoints = cardData.victoryPoints;

            for (int i = 0; i < cardData.heroSpecialAbilities.Count; i++) 
            {
                heroSpecialAbilities.Add(cardData.heroSpecialAbilities[i]);
            }
            
        }
    }

    public void RemovePrefab()
    {
        gameObject.transform.parent = cardsPool;
        if (assignedCards.Count > 0)
        {
            for (int i = 0; i < assignedCards.Count; i++)
            {
                RemovePrefab();
            }
        }
        spriteRenderer.color = Color.white;
        gameObject.GetComponent<SpriteRenderer>().sprite = null;
        heroCost = 0;
        heroAttacks = 0;
        heroResources = 0;
        heroCardsToDraw = 0;
        villainAttacks = 0;
        villainHasFightAbility = false;
        villainHasEscapeAbility = false;
        villainHasFightAbility = false;
        HasVillainUniqueAbility = false;
        heroSpecialAbilities.Clear();
        victoryPoints = 0;
        gameObject.SetActive(false);
    }
    
    public void BuyCard()
    {
        playerComponent.resources -= heroCost;
        EventManager.BuyCard(this);
        playerComponent.DiscardCard(this);          
    }

    public void FightCard()
    {
        playerComponent.attacks -= villainAttacks;
        EventManager.FightCard(this);
        cardData.FightVillain(gameManager);
        playerComponent.victoryPool.Add(cardData);
        if (assignedCards.Count > 0) 
        { 
            for (int i = 0; i < assignedCards.Count; i++) 
            {
                playerComponent.victoryPool.Add(assignedCards[i].GetComponent<Card>().cardData);
            }
        }
        transform.DOMoveX(20, 0.5f).onComplete = RemovePrefab;
        transform.DORotate(new Vector3(0, 0, 360), 0.2f, RotateMode.FastBeyond360)
                 .SetEase(Ease.Linear)
                 .SetLoops(-1, LoopType.Restart);
    }

    public void KOCard()
    { 
    
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
        assignedCards.Add(card);
        card.transform.parent = gameObject.transform;
        assignedCardSpriteRenderer.sortingLayerName = spriteRenderer.sortingLayerName;
        Debug.Log("Ma mieæ sorting layer " + (spriteRenderer.sortingOrder - (1 * assignedCards.Count)));
        assignedCardSpriteRenderer.sortingOrder = spriteRenderer.sortingOrder-(1*assignedCards.Count);
        card.transform.DOMove(new Vector3(transform.position.x + assignedCardOffsetX * assignedCards.Count, transform.position.y+assignedCardOffsetY*assignedCards.Count, transform.position.z), 0.5f);
    }

}
