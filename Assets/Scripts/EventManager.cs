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

    //HQ EVENTS:
    public static event Action<Card> OnCardBought;

    //STATE LOGIC EVENTS:
    public static event Action OnStartGame;
    public static event Action OnFirstTurnStart;
    public static event Action OnEndPlayerTurn;



    //=== INVOKE METHODS:                   <= te metody s¹ wywo³ywane przez inne skrypty i uruchamiaj¹ eventy
    public static void PlayerDrawCardsFromDeck(int number)
    {
        OnPlayerDrewCardsFromDeck?.Invoke(number);
    }

    public static void PlayerDrawNewHand()
    {
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
        OnStartGame?.Invoke();
    }

    public static void FirstTurnStart()
    {
        OnFirstTurnStart?.Invoke();
    }

    public static void EndPlayerTurn()
    {
        OnEndPlayerTurn?.Invoke();
    }

    public static void BuyCard(Card card)
    {
        OnCardBought?.Invoke(card);
    }

    


}
