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

    #region BasicInputBinding
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
        touchAction.canceled += Touch_canceled;

    }

    private void DisableInput()
    {
        touchAction.performed -= Touch_performed;
        touchAction.canceled -= Touch_canceled;
        playerInput.Disable();
    }
    #endregion

    #region DragDeal

    private bool isDragging = false;
    private HumanBasic draggingHuman = null;

    //The function that returns ray
    private Ray GetMouseRay()
    {
        Vector2 screenPosition = touchPositionAction.ReadValue<Vector2>();
        Ray ray = Camera.main.ScreenPointToRay(screenPosition);
        return ray;
    }

    //Left button just pressed
    private void Touch_performed(InputAction.CallbackContext obj)
    {
        Physics.Raycast(GetMouseRay(), out RaycastHit hitData, LayerMask.GetMask("Human"));
        if (hitData.transform.parent.GetComponent<HumanBasic>() != null)
        {
            isDragging = true;
            draggingHuman = hitData.transform.parent.GetComponent<HumanBasic>();
        }
    }

    //Left button released
    private void Touch_canceled(InputAction.CallbackContext obj)
    {
        //Release Dragging
        if (isDragging && draggingHuman != null)
        {
            bool isBind = false;
            Ray ray = GetMouseRay();
            //Release human at a cookware
            if (Physics.Raycast(ray, out RaycastHit hitDataCook, 999f, LayerMask.GetMask("Cookware")))
            {
                if (hitDataCook.transform.parent.GetComponent<CookwareBasic>() != null)
                {
                    CookwareBasic tarCookware = hitDataCook.transform.parent.GetComponent<CookwareBasic>();
                    switch (tarCookware.cookType)
                    {
                        case CookwareType.Study:
                        case CookwareType.Job:
                            draggingHuman.BindCookware(tarCookware);
                            break;
                    }
                }
            }
            //Release human at empty space
            else
            {
                draggingHuman.UnBindCookware();
            }

            if (!isBind)
            {
                //Go back to original place
            }

            isDragging = false;
            draggingHuman = null;
        }
    }

    #endregion


    #region Interaction

    private void CheckRayDrag()
    {
        if (isDragging && draggingHuman != null)
        {
            Ray ray = GetMouseRay();
            if (Physics.Raycast(ray, out RaycastHit hitDataCook, 999f, LayerMask.GetMask("Cookware")))
            {
                draggingHuman.transform.position = hitDataCook.point + new Vector3(0, 0.2f, 0);
            }
            else if (Physics.Raycast(ray, out RaycastHit hitDataStatic, 999f, LayerMask.GetMask("Static")))
            {
                draggingHuman.transform.position = hitDataStatic.point + new Vector3(0, 0.2f, 0);
            }
        }
    }

    #endregion
}