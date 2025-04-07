using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIManager : MonoBehaviour
{ 
    [SerializeField] GameManager gameManager;
    [SerializeField] TextMeshProUGUI playerResourcesText;
    [SerializeField] TextMeshProUGUI playerAttacksText;
    Player player;

    private void Start()
    {
        player = gameManager.player;    
    }

    public void UpdateResourcesText() 
    {
        playerResourcesText.text = "Resources: " + player.resources;
    }

    public void UpdateAttacksText()
    {
        playerAttacksText.text = "Attacks: " + player.attacks;
    }
}
