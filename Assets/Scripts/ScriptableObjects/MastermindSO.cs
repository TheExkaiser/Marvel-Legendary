using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;

[CreateAssetMenu(fileName = "Mastermind", menuName = "Mastermind")]
public class MastermindSO : CardSO
{
    public int victoryPoints;
    public int attacks;
    public List<MastermindTacticSO> tactics;
    public string text;

    public virtual void MasterStrike(GameManager gameManager)
    {
        
    }

    
}
