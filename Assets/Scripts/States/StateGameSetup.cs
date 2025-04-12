using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StateGameSetup : MonoBehaviour
{
    [SerializeField] GameManager gameManager;
    [SerializeField] HQManager hQManager;
    [SerializeField] GameObject titleScreen;

    [SerializeField] CardSetSO startingPlayerDeck;
    Player player;
    UIManager uiManager;
    StateManager stateManager;

    private void Start()
    {
        player = gameManager.player;
        uiManager = gameManager.uiManager;
        stateManager = gameManager.stateManager;             
    }

    private void OnEnable()
    {
        EventManager.OnStartGame += StartGame;
    }

    private void OnDisable()
    {
        EventManager.OnStartGame -= StartGame;
    }

    public void StartGame()
    {
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
    IEnumerator ToggleViewDelay() 
    {
        yield return new WaitForSeconds(1f);
        uiManager.ToggleView();
        stateManager.ChangeState(StateManager.State.PlayerTurn);

    }
}

