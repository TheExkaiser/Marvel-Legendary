using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIButtonEndTurn : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        gameObject.GetComponent<Button>().onClick.AddListener(Click);
    }

    // Update is called once per frame
    void Click()
    {
        EventManager.EndPlayerTurn();
    }
}
