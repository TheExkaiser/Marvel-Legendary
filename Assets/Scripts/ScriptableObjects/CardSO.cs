using System.Collections;
using System.Collections.Generic;
using NaughtyAttributes;
using UnityEngine;

public class CardSO : ScriptableObject
{

    public string name;
    public CardType cardType;
    [ShowAssetPreview] public Sprite image;

    public enum CardType { Hero,Villain,Bystander,Wound,SchemeTwist,MasterStrike,Mastermind }



    public virtual void PlayCard(GameManager gameManager)
    {

    }
}
