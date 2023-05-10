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

    private void Touch_performed(InputAction.CallbackContext obj)
    {
        Vector2 screenPosition = touchPositionAction.ReadValue<Vector2>();
        Ray ray = GameMgr.Instance.mapCamera.ScreenPointToRay(screenPosition);
        //Cast Human
        Physics.Raycast(ray, out RaycastHit hitData, LayerMask.GetMask("Human"));
        if (hitData.transform.parent.GetComponent<HumanBasic>() != null)
        {
            isDragging = true;
            draggingHuman = hitData.transform.parent.GetComponent<HumanBasic>();
        }
    }


    private void Touch_canceled(InputAction.CallbackContext obj)
    {
        //Release Dragging
        if (isDragging)
        {
            isDragging = false;
            draggingHuman = null;
        }
    }

    private void CheckDrag()
    {
        /*        Vector2 screenPosition = touchPositionAction.ReadValue<Vector2>();
                Ray ray = GameMgr.Instance.mapCamera.ScreenPointToRay(screenPosition);
                Physics.Raycast(ray, out RaycastHit hitData);
                Debug.Log(hitData.transform.name);
        */

        if (isDragging && draggingHuman != null)
        {
            Vector2 screenPosition = touchPositionAction.ReadValue<Vector2>();
            Ray ray = Camera.main.ScreenPointToRay(screenPosition);
            if(Physics.Raycast(ray, out RaycastHit hitDataCook, LayerMask.GetMask("Cookware")))
            {
                Debug.Log("Cook" + hitDataCook.point);
                draggingHuman.transform.position = hitDataCook.point + new Vector3(0, 0.2f, 0);
            }
            else if (Physics.Raycast(ray, out RaycastHit hitDataStatic, LayerMask.GetMask("Static")))
            {
                Debug.Log("Static" + hitDataStatic.point);
                draggingHuman.transform.position = hitDataStatic.point + new Vector3(0, 0.2f, 0);
            }
        }
    }


    #endregion

}