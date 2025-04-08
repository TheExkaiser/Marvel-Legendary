using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class DOTweenTest : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        gameObject.GetComponent<RectTransform>().DOAnchorPos(new Vector2(100, 0), 1);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
