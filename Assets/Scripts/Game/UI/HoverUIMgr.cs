using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class HoverUIMgr : MonoBehaviour
{
    private bool isInit = false;

    public void Init()
    {
        InitHuman();

        isInit = true;
    }

    private void Update()
    {
        if (objPopupHuman.activeSelf)
        {
            RefreshHumanPage();
        }
    }


    public void OnEnable()
    {
        EventCenter.Instance.AddEventListener("ShowHumanPage", ShowPageHuman);
        EventCenter.Instance.AddEventListener("HideHumanPage", HidePageHuman);
    }

    public void OnDestroy()
    {
        EventCenter.Instance.RemoveEventListener("ShowHumanPage", ShowPageHuman);
        EventCenter.Instance.RemoveEventListener("HideHumanPage", HidePageHuman);
    }


}
