using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public partial class HoverUIMgr
{
    public GameObject objPopupHuman;
    [Header("Info")]
    public Text txAge;
    public Image imgSex;
    public List<Sprite> listSpSex = new List<Sprite>();

    [Header("Exp")]
    public Transform tfEdu;
    public Transform tfCareer;
    public GameObject pfExp;
    private List<ExpUIItem> listExpEdu = new List<ExpUIItem>();
    private List<ExpUIItem> listExpCareer = new List<ExpUIItem>();

    private HumanBasic curHuman;

    #region Basic
    private void InitHuman()
    {
        //Edu
        PublicTool.ClearChildItem(tfEdu);
        for (int i = 0; i < GameGlobal.expEduLevelLimit.Count; i++)
        {
            GameObject objEdu = GameObject.Instantiate(pfExp, tfEdu);
            ExpUIItem itemEdu = objEdu.GetComponent<ExpUIItem>();
            itemEdu.Init(ExpType.Edu);
            listExpEdu.Add(itemEdu);
        }
        //Career
        PublicTool.ClearChildItem(tfCareer);
        for (int i = 0; i < GameGlobal.expCareerLevelLimit.Count; i++)
        {
            GameObject objCareer = GameObject.Instantiate(pfExp, tfCareer);
            ExpUIItem itemCareer = objCareer.GetComponent<ExpUIItem>();
            itemCareer.Init(ExpType.Career);
            listExpCareer.Add(itemCareer);
        }
        isInit = true;
    }


    private void ShowPageHuman(object arg0)
    {
        HumanBasic humanBasic = (HumanBasic)arg0;
        if (curHuman != humanBasic)
        {
            curHuman = humanBasic;
            if (curHuman.humanItem != null)
            {
                //Sex
                switch (curHuman.humanItem.sex)
                {
                    case Sex.Female:
                        imgSex.sprite = listSpSex[0];
                        break;
                    case Sex.Male:
                        imgSex.sprite = listSpSex[1];
                        break;
                }
            }
        }
        objPopupHuman.SetActive(true);
    }

    private void HidePageHuman(object arg0)
    {
        objPopupHuman.SetActive(false);
    }
    #endregion



    private void RefreshHumanPage()
    {
        if (curHuman !=null)
        {
            //Age
            txAge.text = curHuman.humanItem.Age.ToString();
            //Edu
            for(int i = 0; i < listExpEdu.Count; i++)
            {
                if(i < curHuman.LevelEdu)
                {
                    listExpEdu[i].RefreshExp(true, 1f);
                }
                else if (i == curHuman.LevelEdu)
                {
                    listExpEdu[i].RefreshExp(false, curHuman.RateEdu);
                }
                else
                {
                    listExpEdu[i].RefreshExp(false, 0);
                }
            }
            //Career
            for (int i = 0; i < listExpCareer.Count; i++)
            {
                if (i < curHuman.LevelCareer)
                {
                    listExpCareer[i].RefreshExp(true, 1f);
                }
                else if (i == curHuman.LevelCareer)
                {
                    listExpCareer[i].RefreshExp(false, curHuman.RateCareer);
                }
                else
                {
                    listExpCareer[i].RefreshExp(false, 0);
                }
            }
        }
    }


}
