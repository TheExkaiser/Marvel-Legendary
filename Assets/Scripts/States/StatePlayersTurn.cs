using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class StatePlayersTurn : MonoBehaviour
{
    [SerializeField] GameManager gameManager;
    [SerializeField] TextMeshProUGUI phaseText;
    Player player;
    public string phaseName;
    UIManager uiManager;
    StateManager stateManager;

    private void Awake()
    {
        stateManager = gameManager.stateManager;
        player = gameManager.player;
        uiManager = gameManager.uiManager;
        phaseText.text = phaseName;
    }

    // Start is called before the first frame update
    private void OnEnable()
    {
        gameManager.OnPlayerFocused += uiManager.ShowGameViewButtons;
        gameManager.OnPlayerFocused += gameManager.EnableInteractions;
        uiManager.uiNewStateAnimation.OnNewStateAnimationEnd += gameManager.FocusPlayer;
        uiManager.uiNewStateAnimation.PlayAnimation(phaseName);
        Debug.Log(phaseName);
    }

    private void OnDisable()
    {
        uiManager.uiNewStateAnimation.OnNewStateAnimationEnd -= gameManager.FocusPlayer;
        gameManager.OnPlayerFocused -= uiManager.ShowGameViewButtons;
        gameManager.OnPlayerFocused -= gameManager.EnableInteractions;

    }

    public void EndTurn()
    {
        uiManager.HideGameViewButtons();
        gameManager.DisableInteractions();
        player.DiscardHand();
        player.DiscardPlayedCards();
        player.ResetAttacks();
        player.ResetResources();
        player.DrawNewHand();
        player.OnDrawNewHandEnd += EndTurnEnd;
    }

    public void EndTurnEnd()
    {
        stateManager.ChangeState(StateManager.State.VillainTurn);
        player.OnDrawNewHandEnd -= EndTurnEnd;
    }
}
