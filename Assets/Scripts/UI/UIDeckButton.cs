using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class UIDeckButton : MonoBehaviour
{
    [SerializeField] List<CardSO> deck;
    [SerializeField] Player player;
    [SerializeField] TextMeshProUGUI buttonText;
    [SerializeField] Button button;

    // Start is called before the first frame update
    void Start()
    {
        deck = player.deck;
    }

    // Update is called once per frame
    void Update()
    {
        UpdateButtonText(); 
       
    }

    private void UpdateButtonText()
    {
        buttonText.text = $"{player.deck.Count}";
    }

    
}
