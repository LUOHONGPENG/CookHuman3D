using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIMgr : MonoBehaviour
{
    public HoverUIMgr hoverUIMgr;
    public RetireUIMgr retireUIMgr;
    public RetireMiniUIMgr retireMiniUIMgr;
    public InterfaceUIMgr interfaceUIMgr;
    public EndUIMgr endUIMgr;

    public void Init()
    {
        hoverUIMgr.Init();
        retireUIMgr.Init();
        retireMiniUIMgr.Init();
        interfaceUIMgr.Init();
        endUIMgr.Init();
    }
}
