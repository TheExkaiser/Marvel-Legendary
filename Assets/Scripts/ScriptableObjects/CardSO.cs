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
    public int heroCost;
    public int heroRecruitPoints;
    public bool heroRecruitPointsPlus;
    public int heroAttacks;
    public bool heroAttackPlus;
    public int heroCardsToDraw;
    public string specialAbilityText;
    public List<GameObject> heroSpecialAbilities;
    [Header("")]

    [Header("Villain Stats:")]
    public string villainGroup;
    public int victoryPoints;
    public int villainAttacks;
    public bool hasAmbushAbility;
    public bool hasFightAbility;
    public bool hasEscapeAbility;
    public bool hasVillainUniqueAbility;

    public enum CardType { None,Hero,Villain,Bystander,Wound,SchemeTwist,MasterStrike,Mastermind, MastermindTactic }
    public enum HeroTeam { Shield, Avengers, XMen, SpiderFriends }
    public enum HeroType { None, Strength, Ranged, Tech, Instinct, Covert }


    public virtual void PlayCard(GameManager gameManager)
    {

    }

    public virtual void FightVillain(GameManager gameManager)
    {
        Debug.Log("Launched special ability \"Fight\" of a villain: " + name);
    }

    public virtual void VillainEscapes()
    {

    }

    public virtual void VillainAmbush()
    {

    }
}
