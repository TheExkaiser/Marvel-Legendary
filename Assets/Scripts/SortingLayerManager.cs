using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SortingLayerManager : MonoBehaviour
{
    public string sortingLayer;
    public int sortingLayerID;
    GameObject card;
    SpriteRenderer spriteRenderer;




    public void SetSortingLayer()
    {
        for (int i = 0; i<transform.childCount; i++) 
        {
            card = transform.GetChild(i).gameObject;
            spriteRenderer = card.GetComponent<SpriteRenderer>();
            if (spriteRenderer) 
            { 
                spriteRenderer.sortingLayerName = sortingLayer; 
                spriteRenderer.sortingOrder = i;
            }
        }    
    }

    
}
