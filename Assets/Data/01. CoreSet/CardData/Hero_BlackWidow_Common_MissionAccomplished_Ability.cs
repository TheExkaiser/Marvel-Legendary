using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hero_BlackWidow_Common_MissionAccomplished_Ability : MonoBehaviour, ISpecialAbility
{
    public void useAbility()
    {
        GameManager gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        EventManager.PlayerDrawCardsFromDeck(1);
        gameManager.player.playerHand.gameObject.GetComponent<CardContainerAutoLayout>().UpdateCardsPositions();
        Debug.Log("ABILITY DZIA£A!!!");
    }
}
