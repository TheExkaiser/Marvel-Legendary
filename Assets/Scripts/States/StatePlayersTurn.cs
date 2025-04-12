using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class StatePlayersTurn : MonoBehaviour
{
    [SerializeField] GameManager gameManager;
    [SerializeField] TextMeshProUGUI phaseText;
    [SerializeField] Player player;
    public string phaseName;
    [SerializeField] UIManager uiManager;


    private void Start()
    {
        player = gameManager.player;
    }

    // Start is called before the first frame update
    private void OnEnable()
    {
        phaseText.text = phaseName;
    }

    public void EndTurn()
    {
        EventManager.EndPlayerTurn();
        
    }
}
