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
            if (villainDeckContents[0].cardType == CardSO.CardType.Bystander)
            {
                for (int i = 0; i < citySpaces.Count; i++)
                {
                    Debug.Log("Sprawdza pole " + citySpaces[i].name);
                    if (citySpaces[i].childCount != 0)
                    {
                        VillainCatchesBystander(citySpaces[i]);
                        break;
                    }
                    else if (i == citySpaces.Count - 1)
                    {
                        MastermindCatchesBystander();
                        break;
                    }
                }
            }
            else
            {
                if (CheckCitySpaceIfEmpty(citySpaces[0]))
                {

                    gameManager.DrawFromDeck(villainDeckTransform, villainDeckContents, citySpaces[0], 1, Card.CardLocation.City);
                }
                else
                {
                    moveVillains();
                    gameManager.DrawFromDeck(villainDeckTransform, villainDeckContents, citySpaces[0], 1, Card.CardLocation.City);
                }
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
                VillainEscapes(cardTransform.gameObject.GetComponent<Card>());
                break;
            }

            if (CheckCitySpaceIfEmpty(citySpaces[i]))
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

    bool CheckCitySpaceIfEmpty(Transform slot)
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

    void VillainEscapes(Card cardScript)
    {
        gameManager.escapedVillains.Add(cardScript.cardData);
        cardTransform = cardScript.gameObject.transform;
        cardTransform.DOMove(new Vector3(cardTransform.position.x-4f, cardTransform.position.y+10f, cardTransform.position.z), 0.5f).OnComplete(() => { cardScript.RemovePrefab(); });
        
    }

    void VillainCatchesBystander(Transform citySpace)
    {
        Debug.Log("Villain catches bystander");
        gameManager.DrawFromDeck(villainDeckTransform, villainDeckContents, villainDeckTransform, 1, Card.CardLocation.City);
        GameObject bystanderCard = villainDeckTransform.GetChild(0).gameObject;
        citySpace.GetChild(0).gameObject.GetComponent<Card>().AssignCard(bystanderCard);
    }

    void MastermindCatchesBystander()
    {
        Debug.Log("Mastermind catches bystander");
    }
}
