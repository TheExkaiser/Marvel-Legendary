using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hero_BlackWidow_Common_MissionAccomplished_Ability : HeroSpecialAbility
{
    public override void useAbility()
    {
        GameManager gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        Debug.Log("ABILITY DZIA£A!!!");
    }
}
