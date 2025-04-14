using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HQManager : MonoBehaviour
{
    public GameManager gameManager;
    public Transform hqDeck;
    public List<Transform> hqSlots;
    public List<CardSO> hqDeckList;
    public Transform shieldOfficerSlot;
    public List<CardSO> shieldOfficerDeckList;
    

    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void OnEnable()
    {
        EventManager.OnCardBought += UpdateSlot;
    }

    private void OnDisable()
    {
        EventManager.OnCardBought -= UpdateSlot;
    }

    public void PopulateHQSlots()
    {
        for (int i = 0; i < hqSlots.Count; i++)
        {
            
            UpdateSlot(hqSlots[i]);
        }
    }

    public void UpdateSlot(Transform slot)
    {
        
        if (slot != shieldOfficerSlot)
        {
            gameManager.DrawFromDeckLogic(hqDeck, hqDeckList, slot, 1, Card.CardLocation.HQ);
        }
        else
        {
            gameManager.DrawFromDeckLogic(shieldOfficerSlot, shieldOfficerDeckList, slot, 1, Card.CardLocation.HQ);
        }

    }

    public void UpdateSlot(Card card)
    {
        Transform slot = card.transform.parent;
        
        UpdateSlot(slot);
    }
}
