using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIStartGameButton : MonoBehaviour
{
    [SerializeField] Button thisButton;
    [SerializeField] StateManager stateManager;

    // Start is called before the first frame update
    void Start()
    {
        thisButton.onClick.AddListener(PressButton);
    }

    void PressButton()
    {
        stateManager.stateGameSetup.GetComponent<StateGameSetup>().StartGame();
    }
}
