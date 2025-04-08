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
    public List<CardSO> playerDiscard;
    public Button button;
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
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        playedCards = player.GetComponent<Player>().playedCards;
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
        }
        
    }

    public void DiscardCard()
    {
        playerDiscard.Insert(0, cardData);
        RemovePrefab();
    }
}
