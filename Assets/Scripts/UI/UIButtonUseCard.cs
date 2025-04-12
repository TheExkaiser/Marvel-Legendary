using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIButtonUseCard : MonoBehaviour
{
    [SerializeField] GameManager gameManager;
    [SerializeField] Button button;

    // Start is called before the first frame update
    void Start()
    {
        button.onClick.AddListener(Click);
    }

    private void Update()
    {
    }

    // Update is called once per frame
    void Click() 
    {
        gameManager.UseCard();
    }
}
