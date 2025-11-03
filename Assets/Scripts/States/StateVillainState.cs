using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class StateVillainState : MonoBehaviour
{
    [SerializeField] GameManager gameManager;
    [SerializeField] TextMeshProUGUI phaseText;
    [SerializeField] float delayBeforeDrawingVillainCard;
    [SerializeField] float delayBeforeEndOfPhase;
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
        uiManager.uiNewStateAnimation.OnNewStateAnimationEnd += gameManager.FocusBoard;
        gameManager.OnBoardFocused += StateLogic;
    }

    private void OnDisable()
    {
        uiManager.uiNewStateAnimation.OnNewStateAnimationEnd -= gameManager.FocusBoard;
        gameManager.OnBoardFocused -= StateLogic;
    }



    private void StateLogic()
    {
        StartCoroutine(StateLogicCoroutine());
    }

    IEnumerator StateLogicCoroutine()
    {
        yield return new WaitForSeconds(delayBeforeDrawingVillainCard);
        gameManager.villainManager.DrawVillainCard();
        StartCoroutine(DelayBeforeEndOfPhase());
    }

    IEnumerator DelayBeforeEndOfPhase()
    {
        yield return new WaitForSeconds(delayBeforeEndOfPhase);
        stateManager.ChangeState(StateManager.State.PlayerTurn);

    }
}
