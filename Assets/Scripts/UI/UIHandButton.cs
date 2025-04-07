using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class UIHandButton : MonoBehaviour
{
    [SerializeField] GameObject playerHand;
    [SerializeField] Button button;

    // Start is called before the first frame update
    void Start()
    {
        button.onClick.AddListener(TogglePlayerHand);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void TogglePlayerHand()
    {
        if (!playerHand.activeSelf)
        {
            playerHand.SetActive(true);
        }
        else
        {
            playerHand.SetActive(false);
        }
    }

}
