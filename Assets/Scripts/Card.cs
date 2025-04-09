using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Card : MonoBehaviour, IClickable
{
    public CardSO cardData;
    public Sprite cardImage;
    public GameObject player;
    public GameObject playerHand;
    public CardContainerAutoLayout playerHandManger;
    public CardContainerAutoLayout playedCardsManger;
    public List<CardSO> playerDiscard;
    public GameManager gameManager;
    public Transform cardsPool;
    public Transform cardSlotsPool;
    private GameObject cardSlot;
    public bool played;

    Transform playedCards;



    void Start()
    {
        player = GameObject.Find("Player");
        playerDiscard = player.GetComponent<Player>().discard;
        playerHand = GameObject.Find("PlayerHand");
        //playerHandManger = playerHand.GetComponent<CardContainerAutoLayout>();
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        playedCards = player.GetComponent<Player>().playedCards;
        //playedCardsManger = playedCards.GetComponent<CardContainerAutoLayout>();
        cardsPool = gameManager.cardsPool;
        cardSlotsPool = gameManager.cardSlotsPool;




    }



    private void OnEnable()
    {
        PopulateCardPrefab();
    }

    public void OnClick()
    {
        Debug.Log("CLICKED");
    }

    public void PopulateCardPrefab()
    {
        if (cardData)
        {
            cardImage = cardData.image;
        }
    }

    public void RemovePrefab()
    {
        gameObject.transform.parent = cardsPool;
        gameObject.SetActive(false);
    }

    public void PlayCard()
    {
        if (!played) 
        {
            transform.parent = playedCards;
            cardData.PlayCard(gameManager);
            played = true;
            playerHandManger.UpdateCardsPositions();
            playedCardsManger.UpdateCardsPositions();
        }
        
    }

    public void DiscardCard()
    {
        playerDiscard.Insert(0, cardData);
        RemovePrefab();
    }

}
