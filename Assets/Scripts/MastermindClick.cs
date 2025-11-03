using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MastermindClick : MonoBehaviour, IClickable
{
    public GameManager gameManager;
    MastermindManager mastermindManager;

    // Start is called before the first frame update
    void Start()
    {
        mastermindManager = gameManager.mastermindManager;
    }

    public void OnClick() 
    {
        mastermindManager.clickOnMastermind();
    }

    public void OnHold()
    {

    }
}
