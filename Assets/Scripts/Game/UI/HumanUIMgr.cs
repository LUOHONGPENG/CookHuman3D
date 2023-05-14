using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HumanUIMgr : MonoBehaviour
{
    public GameObject objPopup;
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
    private bool isInit = false;

    #region Basic
    public void Init()
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

    public void OnEnable()
    {
        EventCenter.Instance.AddEventListener("ShowHumanPage", ShowPage);
        EventCenter.Instance.AddEventListener("HideHumanPage", HidePage);
    }

    public void OnDestroy()
    {
        EventCenter.Instance.RemoveEventListener("ShowHumanPage", ShowPage);
        EventCenter.Instance.RemoveEventListener("HideHumanPage", HidePage);
    }

    public void ShowPage(object arg0)
    {
        HumanBasic humanBasic = (HumanBasic)arg0;
        if (curHuman != humanBasic)
        {
            curHuman = humanBasic;
            InitHumanPage();
        }
        objPopup.SetActive(true);
    }

    public void HidePage(object arg0)
    {
        objPopup.SetActive(false);
    }
    #endregion

    public void InitHumanPage()
    {
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

}
