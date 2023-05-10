using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class MapMgr : MonoBehaviour
{
    [Header("Cookware")]
    public CookwareBasic cookStudy1;
    public CookwareBasic cookStudy2;

    private bool isInit = false;

    public void Init()
    {
        InitInput();

        cookStudy1.Init(1001);
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

    public void FixedUpdate()
    {
        if (!isInit)
        {
            return;
        }
        CheckDrag();
    }
}

