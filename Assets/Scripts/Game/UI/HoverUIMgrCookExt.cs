using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public partial class HoverUIMgr
{
    [Header("CookInfo")]
    public Text txNameCook;
    public Text txNameDesc;

    private CookwareBasic curCook;


    private void ShowCookPage(object arg0)
    {
        Debug.Log("CookPage");
        CookwareBasic cookBasic = (CookwareBasic)arg0;
        if (curCook != cookBasic)
        {
            curCook = cookBasic;
        }
        objPopupCook.SetActive(true);

    }

    private void HideCookPage(object arg0)
    {
        objPopupCook.SetActive(false);
    }

    private void RefreshCookPage()
    {
        if (curCook.GetItem() != null)
        {
            CookwareExcelItem cookItem = curCook.GetItem();
            txNameCook.text = cookItem.name;
            txNameDesc.text = cookItem.desc;
        }
    }
}
