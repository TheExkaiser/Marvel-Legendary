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

    private void Start()
    {
        player = gameManager.player;
        hQManager = gameManager.hQManager;
        uiManager = gameManager.uiManager;
        stateManager = gameManager.stateManager;             
    }

    private void OnEnable()
    {
        Debug.Log("Game setup phase");
        EventManager.OnStartGame += StartGame;
    }

    private void OnDisable()
    {
        EventManager.OnStartGame -= StartGame;
    }

    public void StartGame()
    {
        CreateShieldOfficerStartingDeck();
        hQManager.PopulateHQSlots();
        CreateStartingPlayerDeck();
        player.DrawNewHand();
        titleScreen.SetActive(false);
        Debug.Log("Dzia³a do coroutine");
        StartCoroutine(ToggleViewDelay());
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

    IEnumerator ToggleViewDelay() 
    {
        yield return new WaitForSeconds(1f);
        uiManager.ToggleView();
        stateManager.ChangeState(StateManager.State.VillainTurn);

    }
}

