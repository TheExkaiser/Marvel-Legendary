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
    [SerializeField] Button playedCardsButton;
    [SerializeField] GameObject playedCardsGO;
    Player player;

    private void Start()
    {
        player = gameManager.player;
        playedCardsButton.onClick.AddListener(TogglePlayedCardsWindow);
    }

    public void UpdateResourcesText() 
    {
        playerResourcesText.text = "Resources: " + player.resources;
    }

    public void UpdateAttacksText()
    {
        playerAttacksText.text = "Attacks: " + player.attacks;
    }

    public void TogglePlayedCardsWindow() 
    { 
        if(playedCardsGO.activeSelf) {playedCardsGO.SetActive(false);}
        else { playedCardsGO.SetActive(true);}
    }
}
