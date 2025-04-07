using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class StatePlayersTurn : MonoBehaviour
{
    [SerializeField] GameManager gameManager;
    [SerializeField] TextMeshProUGUI phaseText;
    [SerializeField] Player player;
    public string phaseName;
    Transform playerHand;
    Transform playedCards;
    [SerializeField] UIManager uiManager;


    private void Start()
    {
        player = gameManager.player;
        playerHand = player.playerHand.transform;
        playedCards = player.playedCards;
    }

    // Start is called before the first frame update
    private void OnEnable()
    {
        phaseText.text = phaseName;
    }

    public void EndTurn()
    {
        Card card;
        int cardsInHand = playerHand.childCount;
        int cardsPlayed = playedCards.childCount;

        for(int i = 0; i< cardsInHand; i++)
        {
            card = playerHand.GetChild(0).GetChild(0).GetComponent<Card>();
            card.DiscardCard();
        }

        for (int i = 0; i < cardsPlayed; i++)
        {
            card = playedCards.GetChild(0).GetChild(0).GetComponent<Card>();
            card.DiscardCard();
        }

        player.resources = 0;
        player.attacks = 0;
        uiManager.UpdateResourcesText();
        uiManager.UpdateAttacksText();
        Debug.Log("PLAYER'S TURN ENDED");
        
    }
}
