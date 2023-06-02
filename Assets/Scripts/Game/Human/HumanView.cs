using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class HumanView : MonoBehaviour
{
    public Canvas canvasUI;
    public Transform tfRootUI;

    [Header("Age")]
    public RectTransform rtAge;
    public Text txAge;
    public Image imgAgeNormal;
    public Image imgAgeFill;
    public List<Color> listSpAgeBgColor = new List<Color>();

    [Header("Info")]
    public CanvasGroup canvasGroupInfo;
    public Image imgBg;
    public List<Sprite> listSpBg = new List<Sprite>();
    public Image imgSex;
    public List<Sprite> listSpSex = new List<Sprite>();
    public Image imgMarry;
    public Transform tfEduHuman;
    public Transform tfCareerHuman;
    public GameObject pfExpHuman;
    private List<ExpUIItem> listExpEdu = new List<ExpUIItem>();
    private List<ExpUIItem> listExpCareer = new List<ExpUIItem>();

    private int lastEduLevel = -1;
    private int lastCareerLevel = -1;

    private HumanBasic parent;
    private bool isInit = false;

    public void Init(HumanBasic parent)
    {
        this.canvasUI.worldCamera = GameMgr.Instance.uiCamera;
        this.parent = parent;

        //Edu
        PublicTool.ClearChildItem(tfEduHuman);
        for (int i = 0; i < GameGlobal.expEduLevelLimit.Count; i++)
        {
            GameObject objEdu = GameObject.Instantiate(pfExpHuman, tfEduHuman);
            ExpUIItem itemEdu = objEdu.GetComponent<ExpUIItem>();
            itemEdu.Init(ExpType.Edu);
            listExpEdu.Add(itemEdu);
        }
        //Career
        PublicTool.ClearChildItem(tfCareerHuman);
        for (int i = 0; i < GameGlobal.expCareerLevelLimit.Count; i++)
        {
            GameObject objCareer = GameObject.Instantiate(pfExpHuman, tfCareerHuman);
            ExpUIItem itemCareer = objCareer.GetComponent<ExpUIItem>();
            itemCareer.Init(ExpType.Career);
            listExpCareer.Add(itemCareer);
        }

        InitConst();
        isInit = true;
    }

    #region BasicHoverEvent
    private void OnEnable()
    {
        EventCenter.Instance.AddEventListener("ShowHumanPage", HoverEnterEvent);
        EventCenter.Instance.AddEventListener("HideHumanPage", HoverLeaveEvent);
    }

    private void OnDestroy()
    {
        EventCenter.Instance.RemoveEventListener("ShowHumanPage", HoverEnterEvent);
        EventCenter.Instance.RemoveEventListener("HideHumanPage", HoverLeaveEvent);
    }

    private void HoverEnterEvent(object arg0)
    {
        int tarID = ((HumanBasic)arg0).humanItem.HumanID;
        if (tarID != parent.humanItem.HumanID)
        {
            ShowConst();
            return;
        }
        ShowHover();
    }

    private void HoverLeaveEvent(object arg0)
    {
        if ((int)arg0 != parent.humanItem.HumanID)
        {
            return;
        }
        ShowConst();
    }

    private void InitConst()
    {
        canvasUI.sortingOrder = 0;
        canvasGroupInfo.alpha = 0;
        rtAge.localPosition = new Vector2(0, 55f);
        rtAge.localScale = new Vector2(0.55f, 0.55f);
    }

    private void ShowConst()
    {
        canvasUI.sortingOrder = 0;
        canvasGroupInfo.DOFade(0, 0.25f);
        rtAge.DOLocalMove(new Vector2(0, 55f),0.25f);
        rtAge.DOScale(new Vector2(0.55f, 0.55f), 0.25f);
    }

    private void ShowHover()
    {
        canvasUI.sortingOrder = 1;
        canvasGroupInfo.DOFade(1, 0.25f);
        rtAge.DOLocalMove(new Vector2(-131.1f, 180f), 0.25f);
        rtAge.DOScale(Vector2.one, 0.25f);
    }
    #endregion

    private void Update()
    {
        RefreshHumanUI();
        CheckLevelUp();
    }

    private void RefreshHumanUI()
    {
        if (!isInit)
        {
            return;
        }
        //UI Position
        tfRootUI.localPosition = PublicTool.CalculateUICanvasPos(parent.tfHumanHead.position, GameMgr.Instance.mapCamera);//+ new Vector3(0, 100f, 0)

        RefreshHumanAgeUI();
        RefreshHumanStatusUI();
    }

    private void RefreshHumanAgeUI()
    {
        if (!isInit)
        {
            return;
        }
        //Age Data
        txAge.text = parent.Age.ToString();
        //Age Fill Check
        switch (parent.humanState)
        {
            case HumanState.Studying:
                imgAgeNormal.gameObject.SetActive(false);
                imgAgeFill.color = listSpAgeBgColor[0];
                imgAgeFill.fillAmount = PublicTool.CalculateEduRate(parent.humanItem.expEdu);
                break;
            case HumanState.Working:
                imgAgeNormal.gameObject.SetActive(false);
                imgAgeFill.color = listSpAgeBgColor[1];
                imgAgeFill.fillAmount = PublicTool.CalculateCareerRate(parent.humanItem.expCareer);
                break;
            case HumanState.Marrying:
                imgAgeNormal.gameObject.SetActive(false);
                imgAgeFill.color = listSpAgeBgColor[2];
                imgAgeFill.fillAmount = 1f - (parent.yearMarriage / parent.maxYearMarriage);
                break;
            default:
                imgAgeNormal.gameObject.SetActive(true);
                imgAgeFill.fillAmount = 0;
                break;
        }
    }

    private void RefreshHumanStatusUI()
    {
        if (!isInit)
        {
            return;
        }
        //Sex
        switch (parent.humanItem.sex)
        {
            case Sex.Female:
                imgBg.sprite = listSpBg[0];
                imgSex.sprite = listSpSex[0];
                break;
            case Sex.Male:
                imgSex.sprite = listSpSex[1];
                imgBg.sprite = listSpBg[1];
                break;
        }
        //Marry
        if (parent.humanItem.isMarried)
        {
            imgMarry.gameObject.SetActive(true);
        }
        else
        {
            imgMarry.gameObject.SetActive(false);
        }
        //Edu
        for (int i = 0; i < listExpEdu.Count; i++)
        {
            if (i < parent.LevelEdu)
            {
                listExpEdu[i].RefreshExp(true, 1f,i);
            }
            else if (i == parent.LevelEdu)
            {
                listExpEdu[i].RefreshExp(false, parent.RateEdu,i);
            }
            else
            {
                listExpEdu[i].RefreshExp(false, 0,i);
            }
        }
        //Career
        for (int i = 0; i < listExpCareer.Count; i++)
        {
            if (i < parent.LevelCareer)
            {
                listExpCareer[i].RefreshExp(true, 1f,i);
            }
            else if (i == parent.LevelCareer)
            {
                listExpCareer[i].RefreshExp(false, parent.RateCareer,i);
            }
            else
            {
                listExpCareer[i].RefreshExp(false, 0,i);
            }
        }
    }

    private void CheckLevelUp()
    {
        if (parent.humanState == HumanState.Studying)
        {
            if (lastEduLevel < 0)
            {
                lastEduLevel = parent.LevelEdu;
            }
            else if (parent.LevelEdu > lastEduLevel)
            {
                lastEduLevel = parent.LevelEdu;

                EffectUIInfo info = new EffectUIInfo("LevelUpStudy", PublicTool.CalculateUICanvasPos(parent.tfHumanHead.position, GameMgr.Instance.mapCamera), parent.LevelEdu);
                EventCenter.Instance.EventTrigger("EffectUI", info);
            }
        }
        else if (parent.humanState == HumanState.Working)
        {
            if (lastCareerLevel < 0)
            {
                lastCareerLevel = parent.LevelCareer;
            }
            else if (parent.LevelCareer > lastCareerLevel)
            {
                lastCareerLevel = parent.LevelCareer;
                EffectUIInfo info = new EffectUIInfo("LevelUpJob", PublicTool.CalculateUICanvasPos(parent.tfHumanHead.position, GameMgr.Instance.mapCamera), parent.LevelCareer);
                EventCenter.Instance.EventTrigger("EffectUI", info);
            }
        }
    }
}
