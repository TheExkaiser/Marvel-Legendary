using System.Collections;
using System.Collections.Generic;
using UnityEditor.Tilemaps;
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

    // Start is called before the first frame update
    private void OnEnable()
    {
        titleScreen.SetActive(true);
    }

    private void OnDisable()
    {
        titleScreen.SetActive(false);
    }

    public void StartGame()
    {
        hQManager.PopulateHQSlots();
        CreateStartingPlayerDeck();
        player.DrawNewHand();
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
        Debug.Log("Wawiting for start");
        yield return new WaitForSeconds(1f);
        Debug.Log("Started");
        uiManager.ToggleView();
        stateManager.ChangeState(StateManager.State.PlayerTurn);

    }
}

