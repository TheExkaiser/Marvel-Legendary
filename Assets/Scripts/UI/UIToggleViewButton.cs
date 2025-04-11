using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class UIToggleViewButton : MonoBehaviour
{
    [SerializeField] UIManager uiManager;
    [SerializeField] Button button;

    // Start is called before the first frame update
    void Start()
    {
        button.onClick.AddListener(ToggleView);
    }


    private void ToggleView()
    {
        uiManager.ToggleView();
    }

}
