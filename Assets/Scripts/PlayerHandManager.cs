using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHandManager : MonoBehaviour
{
    [SerializeField] float spacing;
    [SerializeField] RectTransform rectTransform;
    Vector2 startingPosition;
    Vector2 lastPosition;
    [SerializeField] int childrenCount;
    float cardWidth;

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
        rectTransform = gameObject.transform.GetChild(0).GetComponent<RectTransform>();
        cardWidth = rectTransform.rect.width;
        startingPosition = new Vector2(0.5f - (childrenCount - 1) * (cardWidth + spacing) / 2, 0);

        if (childrenCount >= 5) { spacing =spacing - spacing/(childrenCount-1) * -1; }


        for (int i = 0; i<childrenCount; i++)
        {
            float p = startingPosition.x + i * (cardWidth+spacing);
            gameObject.transform.GetChild(i).GetComponent<RectTransform>().DOAnchorPos(new Vector2(p, 0), 0.5f);
        }
        
    }
}
