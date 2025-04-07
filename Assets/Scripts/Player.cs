using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] GameManager gameManager;
    [SerializeField] GameObject cardsPool;
    [SerializeField] GameObject cardSlotsPool;
    public GameObject playerHand;
    [SerializeField] CardSetSO startingDeck;
    public int resources;
    public int attacks;
    public int victoryPoints;

    public List<CardSO> deck;
    public List<CardSO> discard;
    public List<CardSO> teleport;
    public List<CardSO> cardsPlayedList;

    private void Start()
    {
        StartGame();
    }

    void StartGame()
    {
        CreateStartingDeck();
    }

    public void AddAttacks(int number) 
    {
        attacks += number;
    }

    public void AddResources(int number)
    {
        resources += number;
    }

    public void RescueBystander(int number)
    {
        Debug.Log($"Player rescued {number} bystanders!");
    }

    public void DrawCard(int number, List<CardSO> deck)
    {
        if (deck.Count > 0) 
        {
            GameObject cardSlot = cardSlotsPool.transform.GetChild(0).transform.gameObject;
            GameObject card = cardsPool.transform.GetChild(0).transform.gameObject;
            card.SetActive(true);
            cardSlot.SetActive(true);
            cardSlot.transform.parent = playerHand.transform;
            card.transform.parent = cardSlot.transform;
            card.GetComponent<RectTransform>().localPosition = new Vector3(0,0,0);
            card.GetComponent<Card>().cardData = deck[0];
            card.GetComponent<Card>().PopulateCardPrefab();
            


            deck.RemoveAt(0);
        }
        
    }

    public void ShuffleDiscardIntoDeck()
    {
        if (discard.Count > 0)
        {
            for (int i = 0; i < discard.Count; i++)
            {
                deck.Add(discard[i]);
            }
            discard.Clear();
            gameManager.Shuffle(deck);
        }
    }

    public void CreateStartingDeck()
    { 
        for (int i = 0; i< startingDeck.cards.Count; i++) 
        {
            deck.Add(startingDeck.cards[i]);
        }
    }
}
