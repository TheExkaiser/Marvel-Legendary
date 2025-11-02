using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

public class ClickHandler : MonoBehaviour
{
    public InputAction clickAction;
    Card selectedCard;
    Player player;

    private void Awake()
    {
        player = gameObject.GetComponent<GameManager>().player;
        clickAction = new InputAction(type: InputActionType.Button, binding: "<Pointer>/press");
        clickAction.performed += OnClick;
    }
    private void OnEnable() => clickAction.Enable();
    private void OnDisable() => clickAction.Disable();

    private void OnClick(InputAction.CallbackContext ctx)
    {
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
        
        

}
