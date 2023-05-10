using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public partial class MapMgr
{
    private PlayerInput playerInput;
    private InputAction touchAction;
    private InputAction touchPositionAction;

    private void InitInput()
    {
        playerInput = new PlayerInput();
        touchAction = playerInput.Gameplay.Touch;
        touchPositionAction = playerInput.Gameplay.TouchPosition;
    }

    private void EnableInput()
    {
        playerInput.Enable();
        touchAction.performed += Touch_performed;
    }

    private void DisableInput()
    {
        touchAction.performed -= Touch_performed;
        playerInput.Disable();
    }

    private void Touch_performed(InputAction.CallbackContext obj)
    {
        Vector2 screenPosition = touchPositionAction.ReadValue<Vector2>();
        Ray ray = Camera.main.ScreenPointToRay(screenPosition);
        if (Physics.Raycast(ray, out RaycastHit hitData) && hitData.transform.tag == "Human")
        {
            Debug.Log("PressHuman");
        }
    }
}