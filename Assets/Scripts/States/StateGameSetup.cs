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

    private void Start()
    {
        player = gameManager.player;

        
        
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
    }

    public void CreateStartingPlayerDeck()
    {
        for (int i = 0; i < startingPlayerDeck.cards.Count; i++)
        {
            player.deckContents.Add(startingPlayerDeck.cards[i]);
        }

        gameManager.Shuffle(player.deckContents);
    }
}

