using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

public class ClickHandler : MonoBehaviour
{
    public InputActionAsset controlsInputAsset;
    InputAction clickAction;
    InputAction holdAction;
    public InputActionMap cardSelectingActionMap;
    Card selectedCard;
    Player player;

    private void Awake()
    {
        player = gameObject.GetComponent<GameManager>().player;
        cardSelectingActionMap = controlsInputAsset.FindActionMap("CardSelecting");

        clickAction = cardSelectingActionMap.FindAction("Click");
        holdAction = cardSelectingActionMap.FindAction("Hold");


    }
    private void OnEnable()
    {
        clickAction.canceled += OnClick;
        holdAction.performed += OnHold;
    }
    private void OnDisable()
    {
        clickAction.canceled -= OnClick;
        holdAction.performed -= OnHold;
    }

    private void OnClick(InputAction.CallbackContext ctx)
    {

        Debug.Log("Click OK");
        selectedCard = player.selectedCard;

        if (Camera.main == null || Pointer.current == null)
            return;

        Ray ray = Camera.main.ScreenPointToRay(Pointer.current.position.ReadValue());
        RaycastHit2D hit = Physics2D.GetRayIntersection(ray);
        if (hit)
        {
            IClickable clickable = hit.collider.GetComponent<IClickable>();
            // Debug.Log(hit.collider.name);
            clickable?.OnClick();
            
        }
        else
        {
            if (selectedCard != null)
            {
                selectedCard.DeselectCard();
            }
            
        }
        
        
    }

    private void OnHold(InputAction.CallbackContext ctx)
    {
        Debug.Log("Hold OK");

        Ray ray = Camera.main.ScreenPointToRay(Pointer.current.position.ReadValue());
        RaycastHit2D hit = Physics2D.GetRayIntersection(ray);
        if (hit)
        {
            IClickable clickable = hit.collider.GetComponent<IClickable>();
            // Debug.Log(hit.collider.name);
            clickable?.OnHold();

        }
    }
        
        

}
