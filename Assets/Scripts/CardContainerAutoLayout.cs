using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class CardContainerAutoLayout : MonoBehaviour
{
    [SerializeField] float spacing;
    [SerializeField] float spacingModifier;
    RectTransform rectTransform;
    Vector2 startingPosition;
    Vector2 lastPosition;
    int childrenCount;
    float cardWidth;
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
            rectTransform = gameObject.transform.GetChild(0).GetComponent<RectTransform>();
            cardWidth = rectTransform.rect.width;
            currentSpacing = spacing;

            if (childrenCount >= 5) { currentSpacing = currentSpacing + currentSpacing * childrenCount / spacingModifier; }

            startingPosition = new Vector2(0.5f - (childrenCount - 1) * (cardWidth + currentSpacing) / 2, 0);




            for (int i = 0; i < childrenCount; i++)
            {
                float p = startingPosition.x + i * (cardWidth + currentSpacing);
                gameObject.transform.GetChild(i).GetComponent<RectTransform>().DOAnchorPos(new Vector2(p, 0), 0.5f);
            }

        }
        
        
    }
}
