using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Unity.VisualScripting;

public class UITextDiscard : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI text;
    [SerializeField] GameObject player;
    List<CardSO> discardList;


    private void Start()
    {
        discardList = player.GetComponent<Player>().discard;    
    }

    // Update is called once per frame
    void Update()
    {
        text.text = $"Discard: {discardList.Count}";
    }
}
