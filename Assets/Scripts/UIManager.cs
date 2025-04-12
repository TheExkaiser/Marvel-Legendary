using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using DG.Tweening;

public class UIManager : MonoBehaviour
{ 
    [SerializeField] GameManager gameManager;
    [SerializeField] TextMeshProUGUI playerResourcesText;
    [SerializeField] TextMeshProUGUI playerAttacksText;
    [SerializeField] TextMeshProUGUI toggleViewButtonText;
    [Header("   ")]
    [SerializeField] Transform boardZone;
    [SerializeField] Vector3 boardZoneHiddenPosition;
    [SerializeField] GameObject boardZoneRaycastBlocker;
    [Header("   ")]
    [SerializeField] Transform playerZone;
    [SerializeField] Vector3 playerZoneHiddenPosition;
    [SerializeField] GameObject playerZoneRaycastBlocker;


    Player player;
    Vector3 boardZoneDefaultPosition;
    Vector3 boardZoneTargetPosition;
    Vector3 playerZoneDefaultPosition;
    Vector3 playerZoneTargetPosition;
    GameObject playedCardsGO;
    bool boardZoneActive=true;
    bool playerZoneActive = false;
    public Button useCardButton;
    public TextMeshProUGUI useCardButtonText;

    private void Start()
    {
        player = gameManager.player;
        playedCardsGO = player.playedCards.gameObject;
        boardZoneDefaultPosition = boardZone.transform.position;
        playerZoneDefaultPosition = playerZone.transform.position;
    }

    private void Update()
    {
    }

    public void UpdateResourcesText() 
    {
        playerResourcesText.text = "Resources: " + player.resources;
    }

    public void UpdateAttacksText()
    {
        playerAttacksText.text = "Attacks: " + player.attacks;
    }

    public void ToggleView() 
    {
        if (!playerZoneActive && boardZoneActive) 
        {
            //Show Player
            playerZone.DOMove(playerZoneDefaultPosition, 1f);
            boardZone.DOMove(boardZoneHiddenPosition, 1f);
            boardZoneRaycastBlocker.gameObject.SetActive(true);
            playerZoneRaycastBlocker.gameObject.SetActive(false);
            boardZoneActive = false;
            playerZoneActive = true;
            toggleViewButtonText.text = "Board";
        }
        else
        {
            //Show Board
            boardZone.DOMove(boardZoneDefaultPosition, 1f);
            playerZone.DOMove(playerZoneHiddenPosition, 1f);
            playerZoneRaycastBlocker.gameObject.SetActive(true);
            boardZoneRaycastBlocker.gameObject.SetActive(false);
            playerZoneActive = false;
            boardZoneActive = true;
            toggleViewButtonText.text = "Player";

        }
    }

    public void EnableUseCardButton(Card.CardLocation cardLocation)
    {
        useCardButton.gameObject.SetActive(true);
        if (cardLocation == Card.CardLocation.PlayerHand)
        {
            useCardButtonText.text = "Play";
        }
        else if (cardLocation == Card.CardLocation.HQ)
        {
            useCardButtonText.text = "Buy";
        }
    }
    
    public void DisableUseCardButton()
    {
        if (!player.selectedCard)
        {
            useCardButtonText.text = "";
            useCardButton.gameObject.SetActive(false);
        }
    }

}
