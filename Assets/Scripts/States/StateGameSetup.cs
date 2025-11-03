using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StateGameSetup : MonoBehaviour
{
    [SerializeField] GameManager gameManager;
    
    [SerializeField] GameObject titleScreen;

    [SerializeField] CardSetSO startingPlayerDeck;
    [SerializeField] CardSetSO shieldOfficerStartingDeck;
    Player player;
    UIManager uiManager;
    HQManager hQManager;
    StateManager stateManager;

    private void Awake()
    {
        player = gameManager.player;
        hQManager = gameManager.hQManager;
        uiManager = gameManager.uiManager;
        stateManager = gameManager.stateManager;             
    }

    private void OnEnable()
    {
        titleScreen.SetActive(false);
        CreateShieldOfficerStartingDeck();
        hQManager.PopulateHQSlots();
        CreateStartingPlayerDeck();
        player.DrawNewHand();
        stateManager.ChangeState(StateManager.State.PlayerTurn);
    }

    private void OnDisable()
    {
    }

    public void CreateStartingPlayerDeck()
    {
        for (int i = 0; i < startingPlayerDeck.cards.Count; i++)
        {
            player.deckContents.Add(startingPlayerDeck.cards[i]);
        }

        gameManager.Shuffle(player.deckContents);
    }

    public void CreateShieldOfficerStartingDeck()
    {
        for (int i = 0; i < shieldOfficerStartingDeck.cards.Count; i++)
        {
            hQManager.shieldOfficerDeckList.Add(shieldOfficerStartingDeck.cards[i]);
        }
    }
}

