using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public partial class HoverUIMgr
{
    [Header("CookInfo")]
    public Text txNameCook;
    public Text txDescCook;
    public Text txAgeCook;
    public Text codeAgeCook;
    public Transform tfEduCook;
    public Transform tfCareerCook;
    public GameObject pfRequiredCook;

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
            //Desc
            txNameCook.text = cookItem.name;
            txDescCook.text = cookItem.desc;
            //Age
            //ForSchool
            if (curCook.cookType == CookwareType.Study)
            {
                txAgeCook.text = "Recommended Age:";
            }
            //Not School
            else
            {
                txAgeCook.text = "Required Age:";
                //If retire
                if (curCook.AgeMax_real > 100)
                {
                    codeAgeCook.text = string.Format("{0}+", curCook.AgeMin_real);
                }
                //Normal
                else
                {
                    codeAgeCook.text = string.Format("{0}-{1}", curCook.AgeMin_real, curCook.AgeMax_real);
                }
            }
            //Requirement
            PublicTool.ClearChildItem(tfEduCook);
            PublicTool.ClearChildItem(tfCareerCook);
            for(int i = 0; i < curCook.eduMin; i++)
            {

            }
            for(int i = 0;i < curCook.CareerMin; i++)
            {

            }
        }
    }
}
