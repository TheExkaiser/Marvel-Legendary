using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using DG.Tweening;


public class GameManager : MonoBehaviour
{

    public Transform cardsPool;
    public Transform cardSlotsPool;
    public Player player;
    public SpecialAbilities specialAbilities;
    public Transform hqDeck;
    public List<CardSO> hqDeckList;

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
        if (hqDeckList.Count > 0) 
        {
            Transform card = cardsPool.GetChild(0);
            RectTransform cardRect = card.GetComponent<RectTransform>();
            Card cardScript = card.gameObject.GetComponent<Card>();

            cardRect.localPosition = hqDeck.GetComponent<RectTransform>().localPosition;
            card.SetParent(slot);
            cardScript.cardData = hqDeckList[0];
            cardScript.PopulateCardPrefab();
            card.gameObject.SetActive(true);
            cardRect.DOAnchorPos(slot.localPosition, 0.5f);
            
        }
    }

}
