using System.Collections;
using System.Collections.Generic;
using NaughtyAttributes;
using UnityEngine;

[CreateAssetMenu(menuName = "My Assets/CardData")]

public class CardSO : ScriptableObject
{
    [Header("Universal Stats:")]
    public string name;
    public string gameSet;
    public CardType cardType;
    [ShowAssetPreview] public Sprite image;
    [Header("")]


    [Header("Hero Stats:")]
    public string heroName;
    public List<HeroTeam> team;
    public List<HeroType> heroType;
    public int cost;
    public int recruitPoints;
    public bool recruitPointsPlus;
    public int attack;
    public bool attackPlus;
    public int heroCardsToDraw;
    public int bystandersToRescue;
    public string specialAbilityText;
    public bool hasUniqueAbility;
    [Header("")]

    [Header("Villain Stats:")]
    public string villainGroup;
    public int attacks;
    public bool hasAmbushAbility;
    public bool hasFightAbility;
    public bool hasEscapeAbility;

    public enum CardType { None,Hero,Villain,Bystander,Wound,SchemeTwist,MasterStrike,Mastermind, MastermindTactic }
    public enum HeroTeam { Shield, Avengers, XMen, SpiderFriends }
    public enum HeroType { None, Strength, Ranged, Tech, Instinct, Covert }


    public virtual void PlayCard(GameManager gameManager)
    {

    }
}
