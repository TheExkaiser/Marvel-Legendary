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

    public UINewStateAnimation uiNewStateAnimation;


    Player player;
    MastermindManager mastermindManager;
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
        mastermindManager = gameManager.mastermindManager;
        playedCardsGO = player.playedCards.gameObject;
        boardZoneDefaultPosition = boardZone.transform.position;
        playerZoneDefaultPosition = playerZone.transform.position;
    }

    private void Update()
    {
    }

    private void OnEnable()
    {
        EventManager.OnCardBought += UpdateResourcesText;
        EventManager.OnCardBought += DisableUseCardButton;

    }

    private void OnDisable()
    {
        EventManager.OnCardBought -= UpdateResourcesText;
        EventManager.OnCardBought -= DisableUseCardButton;
    }

    public void UpdateResourcesText() 
    {
        playerResourcesText.text = "Resources: " + player.resources;
    }
    public void UpdateResourcesText(Card card)
    {
        UpdateResourcesText();
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

    public void EnableUseCardButtonFightMastermind()
    {
        useCardButton.gameObject.SetActive(true);
        useCardButtonText.text = "Fight Mastermind (" + mastermindManager.tacticsDeck[0].villainAttacks + ")";
    }

    public void EnableUseCardButton(Card card)
    {

        if (card.cardLocation == Card.CardLocation.PlayerHand)
        {
            useCardButton.gameObject.SetActive(true);
            useCardButtonText.text = "Play";
        }
        else if (card.cardLocation == Card.CardLocation.HQ && card.heroCost <= player.resources)
        {
            useCardButton.gameObject.SetActive(true);
            useCardButtonText.text = "Buy (" + card.heroCost + ")";
        }

        else if (card.cardLocation == Card.CardLocation.City && card.villainAttacks <= player.attacks)
        {
            useCardButton.gameObject.SetActive(true);
            useCardButtonText.text = "Fight (" + card.villainAttacks + ")";
        }
    }
    
    public void DisableUseCardButton()
    {
        useCardButtonText.text = "";
        useCardButton.gameObject.SetActive(false);
    }
    public void DisableUseCardButton(Card card)
    {
        DisableUseCardButton();
    }

}
