using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements.Experimental;

[CreateAssetMenu(fileName = "MastermindTactic", menuName = "Mastermind Tactic")]

public class MastermindTacticSO : CardSO
{
    public MastermindSO mastermind;
    public int victoryPoints;
    public int attacks;
    public string text;
    public virtual void Fight(GameManager gameManager)
    {

    }
}
