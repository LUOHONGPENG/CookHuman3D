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
        InitHuman();

        isInit = true;
    }

    public void OnEnable()
    {
        EnableInput();
    }

    public void OnDisable()
    {
        DisableInput();
    }

    public void FixedTimeGo()
    {
        //Check Human Time and destory
        TimeGoCheckAllHuman();
        //
        CheckRayHover();
        //Dragging
        CheckRayDrag();

    }
}

