using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using Unity.VisualScripting.FullSerializer;
using UnityEngine;
using UnityEngine.UI;
using static UnityEngine.RuleTile.TilingRuleOutput;

public enum HeroTeam {Shield, Avengers, XMen, SpiderFriends}
public enum HeroType {Strength, Ranged, Tech, Instinct, Covert}


[CreateAssetMenu(fileName = "HeroCard", menuName ="Card Data")]
public class HeroCardSO : CardSO
{
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


    public override void PlayCard(GameManager gameManager)
    {
        gameManager.player.AddAttacks(attack);
        gameManager.player.AddResources(recruitPoints);
        if (hasUniqueAbility ) {gameManager.specialAbilities.UseSpecialAbility(gameSet, name);}
        
    }
}
    
