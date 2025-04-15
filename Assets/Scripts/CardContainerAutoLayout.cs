using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class CardContainerAutoLayout : MonoBehaviour
{
    [SerializeField] float spacing;
    [SerializeField] float spacingModifier;
    Transform cardTransform;
    Vector2 startingPosition;
    int childrenCount;
    float currentSpacing;
    List<float> listofPositions;

    public void UpdateCardsPositions()
    {
        childrenCount = gameObject.transform.childCount;
        if (childrenCount != 0) 
        {
            CalculateCardsPositions(childrenCount);
            for (int i = 0; i < childrenCount; i++) 
            {
                gameObject.transform.GetChild(i).transform.DOMove(new Vector3(listofPositions[i], transform.position.y, transform.position.z), 0.3f);
            }
        } 
    }

    public List<float> CalculateCardsPositions(int number) 
    {
        listofPositions = new List<float>();
        cardTransform = gameObject.transform.GetChild(0).transform;
        currentSpacing = spacing;
        startingPosition = new Vector3(0.5f - (number - 1) * currentSpacing / 2, transform.position.y, transform.position.z);

        for (int i = 0; i < number; i++)
        {
            float p = startingPosition.x + i * currentSpacing;
            listofPositions.Add(p);
            
        }
        return listofPositions;
    }
}
