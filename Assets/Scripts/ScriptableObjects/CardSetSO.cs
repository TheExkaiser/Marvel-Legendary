using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "CardSet", menuName = "Card Set")]
public class CardSetSO : ScriptableObject
{
    public List<CardSO> cards;
}
