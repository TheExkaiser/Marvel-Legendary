using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TESTBUTTON : MonoBehaviour
{
    [SerializeField] GameManager gameManager;

    [SerializeField] Transform slot;

    // Start is called before the first frame update
    void Start()
    {
        gameObject.GetComponent<Button>().onClick.AddListener(clickButton);    
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void clickButton()
    {
        gameManager.AddCardToHQ(slot);
            
    }
}
