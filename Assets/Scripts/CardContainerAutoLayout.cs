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

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void UpdateCardsPositions()
    {
        childrenCount = gameObject.transform.childCount;
        if (childrenCount != 0) 
        {
            cardTransform = gameObject.transform.GetChild(0).transform;
            currentSpacing = spacing;

            if (childrenCount >= 5) { currentSpacing = currentSpacing + currentSpacing * childrenCount / spacingModifier; }

            startingPosition = new Vector3(0.5f - (childrenCount - 1) * currentSpacing / 2, transform.position.y,transform.position.z);




            for (int i = 0; i < childrenCount; i++)
            {
                float p = startingPosition.x + i * currentSpacing;
                gameObject.transform.GetChild(i).transform.DOMove(new Vector3(p, transform.position.y, transform.position.z), 0.5f);
            }

        }
        
        
    }
}
