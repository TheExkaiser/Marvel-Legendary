using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Card : MonoBehaviour
{
    public CardSO cardData;
    public Image cardImage;
    public GameObject player;
    public GameObject playerHand;
    public GameObject playedCards;
    public List<CardSO> playerDiscard;
    public Button button;
    public GameManager gameManager;
    public Transform cardsPool;
    public Transform cardSlotsPool;
    private GameObject cardSlot;
    public bool played;



    void Start()
    {
        player = GameObject.Find("Player");
        playedCards = GameObject.Find("PlayedCards");
        playerDiscard = player.GetComponent<Player>().discard;
        playerHand = GameObject.Find("PlayerHand");
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();

        cardsPool = gameManager.cardsPool;
        cardSlotsPool = gameManager.cardSlotsPool;


        button.onClick.AddListener(PlayCard);

        

    }

    

    private void OnEnable()
    {
        PopulateCardPrefab();
    }

    public void PopulateCardPrefab()
    {
        if (cardData)
        {
            cardImage.sprite = cardData.image;
        }
    }

    public void RemovePrefab()
    {
        cardSlot = gameObject.transform.parent.gameObject;
        
        cardSlot.transform.parent = cardSlotsPool;
        gameObject.transform.parent = cardsPool;
        gameObject.SetActive(false);
        cardSlot.SetActive(false);
    }

    public void PlayCard()
    {
        if (!played) 
        {
            cardData.PlayCard(gameManager);
            played = true;
        }
        
        if (gameObject.transform.parent.parent == playerHand.transform)
        {
            transform.parent.parent = playedCards.transform;
        }



        else if (transform.parent.parent == playedCards.transform)
        {
            DiscardCard();
        }
    }

    public void DiscardCard()
    {
        playerDiscard.Add(cardData);
        RemovePrefab();
    }
}
