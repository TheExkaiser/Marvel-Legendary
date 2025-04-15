using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class VillainManager : MonoBehaviour
{
    public GameManager gameManager;
    public GameObject villainDeck;
    public List<CardSO> villainDeckContents;
    public List<Transform> citySpaces;
    public bool drawAnotherCard = false;
    Transform villainDeckTransform;
    Transform cardTransform;
    List<Transform> villainsToMove;
    

    private void Awake()
    {
        villainDeckTransform = villainDeck.transform;
    }

    public void DrawVillainCard()
    {
        StartCoroutine(DrawVillainCardCoroutine());
    }

    IEnumerator DrawVillainCardCoroutine()
    {
        do
        {
            
            if (checkCitySpaceIfEmpty(citySpaces[0]))
            {
                
                gameManager.DrawFromDeck(villainDeckTransform, villainDeckContents, citySpaces[0], 1, Card.CardLocation.City);
            }
            else
            {
                moveVillains();
                gameManager.DrawFromDeck(villainDeckTransform, villainDeckContents, citySpaces[0], 1, Card.CardLocation.City);
            }
            yield return new WaitForSeconds(1f);
        }
        while (drawAnotherCard);     
    }

    void moveVillains()
    {
        for (int i = 1; i<citySpaces.Count+1; i++) 
        {
            if (i == citySpaces.Count) 
            {
                cardTransform = citySpaces[i - 1].GetChild(0);
                villainEscapes(cardTransform.gameObject.GetComponent<Card>());
                break;
            }

            if (checkCitySpaceIfEmpty(citySpaces[i]))
            {
                cardTransform = citySpaces[i - 1].GetChild(0);
                cardTransform.DOMove(citySpaces[i].position, 0.5f);
                cardTransform.parent = citySpaces[i];
                break;
            }
            cardTransform = citySpaces[i - 1].GetChild(0);
            cardTransform.DOMove(citySpaces[i].position, 0.5f);
            cardTransform.parent = citySpaces[i];
        }   
        
    }

    bool checkCitySpaceIfEmpty(Transform slot)
    {
        if (slot.childCount == 0)
        {
            return true;
        }
        else
        {
            return false;
        }
        
    }

    void villainEscapes(Card cardScript)
    {
        gameManager.escapedVillains.Add(cardScript.cardData);
        cardTransform = cardScript.gameObject.transform;
        cardTransform.DOMove(new Vector3(cardTransform.position.x-4f, cardTransform.position.y+10f, cardTransform.position.z), 0.5f).OnComplete(() => { cardScript.RemovePrefab(); });
        
    }
}
