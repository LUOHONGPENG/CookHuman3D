using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class MapMgr : MonoBehaviour
{
    private bool isInit = false;

    public void Init()
    {
        InitInput();
        InitCookware();
        isInit = true;
    }

    public void StartGame()
    {
        StartInput();
        StartHuman();
        StartCookware();
    }


    public void OnEnable()
    {
        EnableInput();
        EventCenter.Instance.AddEventListener("CreateBaby", CreateBaby);
    }

    public void OnDisable()
    {
        DisableInput();
        EventCenter.Instance.RemoveEventListener("CreateBaby", CreateBaby);

    }


    public void FixedTimeGo()
    {
        //Check Human Time and destory
        TimeGoCheckAllHuman();

        //Ban Ray Check When Page On
        if (GameMgr.Instance.isPageOn)
        {
            return;
        }
        CheckGraphicRay();
        CheckRayHover();
        //Dragging
        CheckRayDrag();
        DefaultRelease();
    }
}

