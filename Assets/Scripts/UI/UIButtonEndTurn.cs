using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIButtonEndTurn : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] Button button;
    [SerializeField] StatePlayersTurn playersTurnState;
    void Start()
    {
        button.onClick.AddListener(ClickButton);
    }

    void ClickButton() 
    {
        playersTurnState.EndTurn();
    }
}
