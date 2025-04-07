using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization;
using UnityEngine;
using UnityEngine.XR;

public class StateManager : MonoBehaviour
{
    public GameObject statePlayerTurn;
    public GameObject stateGameSetup;
    public GameObject stateVillainTurn;


    public enum State {PlayerTurn, GameSetup, VillainTurn};

    private void Start()
    {
        ChangeState(State.GameSetup);
    }

    public void ChangeState(State state)
    {
        for (int i = 0; i < gameObject.transform.childCount; i++)
        { 
            gameObject.transform.GetChild(i).gameObject.SetActive(false);
        }

        if (state == State.GameSetup)
        {
            stateGameSetup.SetActive(true);
        }
        else if(state == State.VillainTurn) 
        {
            stateVillainTurn.SetActive(true);
        }
        else if(state == State.PlayerTurn) 
        { 
            statePlayerTurn.SetActive(true);
        }
    }
    
}
