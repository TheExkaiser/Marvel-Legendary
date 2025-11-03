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
    

    public UINewStateAnimation uiNewStateAnimation;


    Player player;
    MastermindManager mastermindManager;
    
    GameObject playedCardsGO;
    bool boardZoneActive=true;
    bool playerZoneActive = false;
    public Button useCardButton;
    public TextMeshProUGUI useCardButtonText;

    [Header("Card Info Panel")]
    [SerializeField] GameObject cardInfoPanel;
    [SerializeField] Image cardInfoImage;

    private void Start()
    {
        player = gameManager.player;
        mastermindManager = gameManager.mastermindManager;
        playedCardsGO = player.playedCards.gameObject;
        
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
        if (gameManager.playerZoneFocused)
        {
            toggleViewButtonText.text = "Player";
            gameManager.FocusBoard();
        }
        else
        {
            gameManager.FocusPlayer();
            toggleViewButtonText.text = "Board";
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

    public void PopulateCardInfoPanel(CardSO cardSO)
    {
        cardInfoImage.sprite = cardSO.image;
        cardInfoPanel.SetActive(true);
    }

    public void CloseCardInfoPanel()
    {
        cardInfoPanel.SetActive(false);
    }

}
