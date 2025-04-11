using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HQManager : MonoBehaviour
{
    public GameManager gameManager;
    public Transform hqDeck;
    public List<Transform> hqSlots;
    public List<CardSO> hqDeckList;

    // Start is called before the first frame update
    void Start()
    {
        
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
        if (slot.childCount == 0) 
        {
            gameManager.DrawFromDeckLogic(hqDeck, hqDeckList, slot, 1);
        }
    }
}
