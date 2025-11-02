using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization;
using UnityEngine;
using UnityEngine.XR;

public class StateManager : MonoBehaviour
{
    public StatePlayersTurn statePlayerTurn;
    public StateGameSetup stateGameSetup;
    public StateVillainState stateVillainTurn;


    public enum State {PlayerTurn, GameSetup, VillainTurn};

    private void Start()
    {
        ChangeState(State.GameSetup);
    }

    public void ChangeState(State state)
    {
        if (state == State.PlayerTurn)
        {
            ClearStates();
            statePlayerTurn.enabled = true;
        }
        else if (state == State.GameSetup)
        {
            ClearStates();
            stateGameSetup.enabled = true;
        }
        else if (state == State.VillainTurn)
        {
            ClearStates();
            stateVillainTurn.enabled = true;
        }
    }

    void ClearStates()
    {
        stateVillainTurn.enabled = false;
        stateGameSetup.enabled = false;
        statePlayerTurn.enabled = false;
    }
    
}
