using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class UIButtonDiscard : MonoBehaviour
{

    [SerializeField] Player player;
    [SerializeField] Button button;

    // Start is called before the first frame update
    void Start()
    {
        button.onClick.AddListener(player.ShuffleDiscardIntoDeck);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
