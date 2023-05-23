using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class HoverUIMgr : MonoBehaviour
{
    public RectTransform rtPos;
    public GameObject objPopupHuman;
    public GameObject objPopupCook;

    private bool isInit = false;

    public void Init()
    {
        InitHuman();

        isInit = true;
    }

    private void Update()
    {
        //pos
        float width = Screen.width;
        float mouseX = GameMgr.Instance.mapMgr.GetMousePos().x;
        if (mouseX < width / 2)
        {
            rtPos.anchoredPosition = new Vector2(500f, 0);
        }
        else
        {
            rtPos.anchoredPosition = new Vector2(-500f, 0);
        }
        //Refresh
        if (objPopupHuman.activeSelf)
        {
            RefreshHumanPage();
        }
    }


    public void OnEnable()
    {
        EventCenter.Instance.AddEventListener("ShowHumanPage", ShowHumanPage);
        EventCenter.Instance.AddEventListener("HideHumanPage", HideHumanPage);
        EventCenter.Instance.AddEventListener("ShowCookPage", ShowCookPage);
        EventCenter.Instance.AddEventListener("HideCookPage", HideCookPage);
    }

    public void OnDestroy()
    {
        EventCenter.Instance.RemoveEventListener("ShowHumanPage", ShowHumanPage);
        EventCenter.Instance.RemoveEventListener("HideHumanPage", HideHumanPage);
        EventCenter.Instance.RemoveEventListener("ShowCookPage", ShowCookPage);
        EventCenter.Instance.RemoveEventListener("HideCookPage", HideCookPage);
    }


}
