using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StateGameSetup : MonoBehaviour
{
    [SerializeField] GameObject titleScreen;


    // Start is called before the first frame update
    private void OnEnable()
    {
        titleScreen.SetActive(true);
    }

    private void OnDisable()
    {
        titleScreen.SetActive(false);
    }
}

