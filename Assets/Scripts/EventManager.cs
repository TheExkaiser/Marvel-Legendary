using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class EventManager : MonoBehaviour
{
    //PLAYER EVENTS:
    public static event Action<int> OnPlayerDrewCardsFromDeck;
    public static event Action OnPlayerDrewNewHand;
    public static event Action<int> OnPlayerAddsAttacks;
    public static event Action<int> OnPlayerAddsResources;


    //STATE LOGIC EVENTS:
    public static event Action OnStartGame;
    public static event Action OnFirstTurnStart;
    public static event Action OnEndPlayerTurn;



    //=== INVOKE METHODS:                   <= te metody s¹ wywo³ywane przez inne skrypty i uruchamiaj¹ eventy
    public static void PlayerDrawCardsFromDeck(int number)
    {
        Debug.Log("Invoked Event PlayerDrawCardsFromDeck");
        OnPlayerDrewCardsFromDeck?.Invoke(number);
    }

    public static void PlayerDrawNewHand()
    {
        Debug.Log("Invoked Event PlayerDrawNewHand");
        OnPlayerDrewNewHand?.Invoke();
    }

    public static void PlayerAddsAttacks(int number)
    {
        OnPlayerAddsAttacks?.Invoke(number);
    }

    public static void PlayerAddsResources(int number)
    {
        OnPlayerAddsResources?.Invoke(number);
    }

    public static void StartGame()
    {
        Debug.Log("Invoked Event StartGame");
        OnStartGame?.Invoke();
    }

    public static void FirstTurnStart()
    {
        Debug.Log("Invoked Event FirstTurnStart");
        OnFirstTurnStart?.Invoke();
    }

    public static void EndPlayerTurn()
    {
        Debug.Log("Invoked Event EndPlayerTurn");
        OnEndPlayerTurn?.Invoke();
    }

    


}
