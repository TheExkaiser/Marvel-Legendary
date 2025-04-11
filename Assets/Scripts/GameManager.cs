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
    SortingLayerManager sortingLayerManager;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }


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

    public void DrawFromDeckLogic(Transform deck, List<CardSO> deckContents, Transform target, int numberOfCards)
    {
        for (int i = 0; i < numberOfCards; i++)
        {
            Debug.Log("drawcardlogic dzia³a " + numberOfCards);
            if (deckContents.Count > 0)
            {
                GameObject card = cardsPool.transform.GetChild(0).transform.gameObject;
                Card cardScript = card.GetComponent<Card>();


                card.transform.parent = target;
                card.transform.position = deck.position;
                card.SetActive(true);

                cardScript.cardData = deckContents[0];
                cardScript.PopulateCardPrefab();

                CardContainerAutoLayout containerLayout = target.gameObject.GetComponent<CardContainerAutoLayout>();

                if (containerLayout)
                {
                    containerLayout.UpdateCardsPositions();
                }
                else
                {
                    card.transform.DOMove(target.position, 0.5f);
                }

                deckContents.RemoveAt(0);

                if (i == numberOfCards - 1) { cardScript.UpdateSortingLayer(); }
            }

        }
            }

}
