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
        EventManager.OnEndPlayerTurn += EndTurn;
    }

    // Start is called before the first frame update
    private void OnEnable()
    {
        uiManager.uiNewStateAnimation.OnNewStateAnimationEnd += gameManager.FocusPlayer;
        uiManager.uiNewStateAnimation.PlayAnimation(phaseName);
        Debug.Log(phaseName);
    }

    private void OnDisable()
    {
        uiManager.uiNewStateAnimation.OnNewStateAnimationEnd -= gameManager.FocusPlayer;
    }

    public void EndTurn()
    {
        stateManager.ChangeState(StateManager.State.VillainTurn);
    }
}
