using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class StateVillainState : MonoBehaviour
{
    [SerializeField] GameManager gameManager;
    [SerializeField] TextMeshProUGUI phaseText;
    public string phaseName;
    StateManager stateManager;
    UIManager uiManager;


    private void Awake()
    {
        uiManager = gameManager.uiManager;
        stateManager = gameManager.stateManager;
    }


    // Start is called before the first frame update
    private void OnEnable()
    {
        phaseText.text = phaseName;
        uiManager.uiNewStateAnimation.PlayAnimation(phaseName);
        EventManager.OnNewStateAnimationEnd += StateLogic;
    }

    private void OnDisable()
    {
        EventManager.OnNewStateAnimationEnd -= StateLogic;
    }

    private void StateLogic()
    {
        gameManager.DrawVillainCard();
        stateManager.ChangeState(StateManager.State.PlayerTurn);
    }
}
