using System.Collections;
using System.Collections.Generic;
using NaughtyAttributes;
using UnityEngine;

public class MastermindManager : MonoBehaviour
{
    [SerializeField] GameManager gameManager;
    public int masterMindHP;
    public CardSetSO masterMindDeck;
    public List<CardSO> tacticsDeck;
    [SerializeField] GameObject mastermindGO;
    // Start is called before the first frame update
    void Start()
    {
        SetupMasterind();
    }

    public void SetupMasterind()
    {
        mastermindGO.GetComponent<SpriteRenderer>().sprite = masterMindDeck.cards[0].image;
        CreateTacticsDeck();
        UpdateMastermindHP();
    }

    public void CreateTacticsDeck()
    {
        for (int i = 1; i<masterMindDeck.cards.Count; i++) 
        {
            tacticsDeck.Add(masterMindDeck.cards[i]);   
        }
        gameManager.Shuffle(tacticsDeck);
    }

    public void UpdateMastermindHP()
    {
        masterMindHP = tacticsDeck.Count;
    }
}
