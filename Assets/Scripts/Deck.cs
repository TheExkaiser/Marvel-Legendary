using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class Deck : MonoBehaviour,IClickable
{
    [SerializeField] GameManager gameManager;
    
    public void OnClick() 
    {
        Debug.Log("Deck click");
        gameManager.DrawFromDeck(name);    
    }

    
}
