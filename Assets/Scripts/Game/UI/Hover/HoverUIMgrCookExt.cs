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
    public Image imgSexCook;

    private CookwareBasic curCook;


    private void ShowCookPage(object arg0)
    {
        CookwareBasic cookBasic = (CookwareBasic)arg0;
        if (curCook != cookBasic)
        {
            curCook = cookBasic;
        }
        RefreshCookPage();
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
                codeAgeCook.text = string.Format("{0}-{1}", GameGlobal.ageMinStudy, GameGlobal.ageMaxStudy);
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
                GameObject obj = GameObject.Instantiate(pfRequiredCook, tfEduCook);
                RequireUIItem item = obj.GetComponent<RequireUIItem>();
                item.Init(ExpType.Edu);
            }
            for(int i = 0;i < curCook.CareerMin; i++)
            {
                GameObject obj = GameObject.Instantiate(pfRequiredCook, tfCareerCook);
                RequireUIItem item = obj.GetComponent<RequireUIItem>();
                item.Init(ExpType.Career);
            }
            //SexRequirement
            if(curCook.cookType == CookwareType.Marriage)
            {
                switch (curCook.requiredSex)
                {
                    case Sex.Female:
                        imgSexCook.sprite = listSpSex[0];
                        break;
                    case Sex.Male:
                        imgSexCook.sprite = listSpSex[1];
                        break;
                }
                imgSexCook.gameObject.SetActive(true);
            }
            else
            {
                imgSexCook.gameObject.SetActive(false);
            }
        }
    }
}
