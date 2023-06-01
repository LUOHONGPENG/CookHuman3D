using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.EventSystems;

public partial class MapMgr
{
    private PlayerInput playerInput;
    private InputAction touchAction;
    private InputAction touchPositionAction;

    private bool isInitInput = false;
    public void StartInput()
    {
        isDragging = false;
        draggingHuman = null;

        recordEnterCook = -1;
        recordEnterHuman = -1;
    }

    #region BasicInputBinding
    private void InitInput()
    {
        if (!isInitInput)
        {
            playerInput = new PlayerInput();
            touchAction = playerInput.Gameplay.Touch;
            touchPositionAction = playerInput.Gameplay.TouchPosition;
            isInitInput = true;
        }
    }

    private void EnableInput()
    {
        if (playerInput == null)
        {
            InitInput();
        }

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

    #region Drag&DropDeal

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
        if (GameMgr.Instance.isPageOn)
        {
            return;
        }

        Physics.Raycast(GetMouseRay(), out RaycastHit hitData, LayerMask.GetMask("Human"));
        if (hitData.transform == null)
        {
            Debug.Log("NoHit");
            return;
        }
        if (hitData.transform.parent.parent.GetComponent<HumanBasic>() != null)
        {
            isDragging = true;
            Debug.Log("StartDragHuman");
            draggingHuman = hitData.transform.parent.parent.GetComponent<HumanBasic>();
        }
    }

    //Left button released(Drop Human)
    private void Touch_canceled(InputAction.CallbackContext obj)
    {
        if (GameMgr.Instance.isPageOn)
        {
            return;
        }
        //Release Dragging
        if (isDragging && draggingHuman != null)
        {
            //UIRay
            foreach (RaycastResult item in raycastResults)
            {
                if (item.gameObject.tag == "CookwareUI")
                {
                    if (item.gameObject.transform.parent.parent.parent.parent.GetComponent<CookwareView>() != null)
                    {
                        CookwareView cookwareView = item.gameObject.transform.parent.parent.parent.parent.GetComponent<CookwareView>();
                        CookwareBasic tarCook = cookwareView.GetBasic();
                        draggingHuman.BindCookware(tarCook);
                        EndDragHuman();
                        return;
                    }
                }
            }


            Ray ray = GetMouseRay();
            //Release human at a cookware
            if (Physics.Raycast(ray, out RaycastHit hitDataCook, 999f, LayerMask.GetMask("Cookware")))
            {
                if (hitDataCook.transform.parent.parent.parent.GetComponent<CookwareBasic>() != null)
                {
                    CookwareBasic tarCook = hitDataCook.transform.parent.parent.parent.GetComponent<CookwareBasic>();
                    draggingHuman.BindCookware(tarCook);
                }
            }
            //Release human at empty space
            else
            {
                draggingHuman.UnBindCookware();
            }
            EndDragHuman();
        }
    }

    private void EndDragHuman()
    {
        isDragging = false;
        draggingHuman = null;
        Debug.Log("EndDragHuman");
    }

    #endregion

    #region RayCheck

    private int recordEnterHuman = -1;
    private int recordEnterCook = -1;
    private List<RaycastResult> raycastResults = new List<RaycastResult>();

    private void CheckGraphicRay()
    {
        //Mouse
        PointerEventData pointerEventData = new PointerEventData(EventSystem.current);
        pointerEventData.position = GetMousePos();
        EventSystem.current.RaycastAll(pointerEventData, raycastResults);
    }

    private void CheckRayHover()
    {
        //UIRay
        foreach (RaycastResult item in raycastResults)
        {
            if (item.gameObject.tag == "CookwareUI")
            {
                if (item.gameObject.transform.parent.parent.parent.parent.GetComponent<CookwareView>() != null)
                {
                    CookwareView cookwareView = item.gameObject.transform.parent.parent.parent.parent.GetComponent<CookwareView>();
                    CookwareBasic tarCook = cookwareView.GetBasic();
                    recordEnterCook = tarCook.cookID;
                    EventCenter.Instance.EventTrigger("ShowCookPage", tarCook);
                    if (isDragging)
                    {
                        EventCenter.Instance.EventTrigger("ShowHumanPage", draggingHuman);
                        recordEnterHuman = draggingHuman.humanItem.HumanID;
                    }
                    return;
                }
            }
        }

        if (!isDragging)
        {
            //Object Raycast
            Ray ray = GetMouseRay();
            //Mouse above Human
            if (Physics.Raycast(ray, out RaycastHit hitDataHuman, 999f, LayerMask.GetMask("Human")))
            {
                CloseCookPage();
                if (hitDataHuman.transform.parent.parent.GetComponent<HumanBasic>() != null)
                {
                    HumanBasic tarHuman = hitDataHuman.transform.parent.parent.GetComponent<HumanBasic>();
                    recordEnterHuman = tarHuman.humanItem.HumanID;
                    EventCenter.Instance.EventTrigger("ShowHumanPage", tarHuman);
                }
            }
            //Mouse above Cookware
            else if (Physics.Raycast(ray, out RaycastHit hitDataCook, 999f, LayerMask.GetMask("Cookware")))
            {
                CloseHumanPage();
                if (hitDataCook.transform.parent.parent.parent.GetComponent<CookwareBasic>() != null)
                {
                    CookwareBasic tarCook = hitDataCook.transform.parent.parent.parent.GetComponent<CookwareBasic>();
                    recordEnterCook = tarCook.cookID;
                    EventCenter.Instance.EventTrigger("ShowCookPage", tarCook);
                }
            }
            else
            {
                CloseHumanPage();
                CloseCookPage();
            }
        }
        else
        {
            //Mouse above Cookware when dragging human
            Ray ray = GetMouseRay();
            if (Physics.Raycast(ray, out RaycastHit hitDataCook, 999f, LayerMask.GetMask("Cookware")))
            {
                if (hitDataCook.transform.parent.parent.parent.GetComponent<CookwareBasic>() != null)
                {
                    CookwareBasic tarCook = hitDataCook.transform.parent.parent.parent.GetComponent<CookwareBasic>();
                    EventCenter.Instance.EventTrigger("ShowCookPage", tarCook);
                    recordEnterCook = tarCook.cookID;
                    EventCenter.Instance.EventTrigger("ShowHumanPage", draggingHuman);
                    recordEnterHuman = draggingHuman.humanItem.HumanID;
                }
            }
            else
            {
                CloseHumanPage();
                CloseCookPage();
            }
        }
    }

    private void CloseHumanPage()
    {
        EventCenter.Instance.EventTrigger("HideHumanPage", recordEnterHuman);

    }

    private void CloseCookPage()
    {
        EventCenter.Instance.EventTrigger("HideCookPage", recordEnterCook);
    }



    //Check item position when dragging
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
                //draggingHuman.transform.position = hitDataStatic.point + new Vector3(0, 0.2f, 0);
                draggingHuman.transform.position = hitDataStatic.point + hitDataStatic.normal * 0.2f;
            }
        }
    }

    public Vector2 GetMousePos()
    {
        Vector2 screenPosition = touchPositionAction.ReadValue<Vector2>();
        return screenPosition;
    }

    public Vector2 GetMousePosUI()
    {
        Vector2 screenPos = touchPositionAction.ReadValue<Vector2>();
        screenPos = new Vector2(screenPos.x * 1920f / Screen.width, screenPos.y * 1080f / Screen.height);
        Vector2 targetPos = new Vector2(screenPos.x - 1920f / 2, screenPos.y - 1080f / 2);
        return targetPos;
    }

    #endregion
}