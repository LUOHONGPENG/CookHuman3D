using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HumanView : MonoBehaviour
{
    public Canvas canvasUI;
    public Transform tfRootUI;
    [Header("HumanConstantUI")]
    public GameObject objConst;
    public Text txAgeConst;
    public Image imgAgeFillConst;
    public List<Color> listColorFill = new List<Color>();

    [Header("HumanHoverUI")]
    public GameObject objHover;
    public Text txAgeHover;
    public Image imgAgeFillHover;
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

        ShowConst();
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

    private void ShowConst()
    {
        canvasUI.sortingOrder = 0;
        objConst.SetActive(true);
        objHover.SetActive(false);
    }

    private void ShowHover()
    {
        canvasUI.sortingOrder = 1;
        objConst.SetActive(false);
        objHover.SetActive(true);
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
        txAgeConst.text = parent.Age.ToString();
        txAgeHover.text = parent.Age.ToString();
        //Age Fill Check
        switch (parent.humanState)
        {
            case HumanState.Studying:
                imgAgeFillConst.fillAmount = PublicTool.CalculateEduRate(parent.humanItem.expEdu);
                imgAgeFillConst.color = listColorFill[1];
                imgAgeFillHover.fillAmount = PublicTool.CalculateEduRate(parent.humanItem.expEdu);
                imgAgeFillHover.color = listColorFill[1];
                break;
            case HumanState.Working:
                imgAgeFillConst.fillAmount = PublicTool.CalculateCareerRate(parent.humanItem.expCareer);
                imgAgeFillConst.color = listColorFill[2];
                imgAgeFillHover.fillAmount = PublicTool.CalculateCareerRate(parent.humanItem.expCareer);
                imgAgeFillHover.color = listColorFill[2];
                break;
            case HumanState.Marrying:
                imgAgeFillConst.fillAmount = 1f - (parent.yearMarriage / 1f);
                imgAgeFillConst.color = listColorFill[3];

                imgAgeFillHover.fillAmount = 1f - (parent.yearMarriage / 1f);
                imgAgeFillHover.color = listColorFill[3];
                break;
            default:
                imgAgeFillConst.fillAmount = 1f;
                imgAgeFillConst.color = listColorFill[0];
                imgAgeFillHover.fillAmount = 1f;
                imgAgeFillHover.color = listColorFill[0];
                break;
        }
    }

    private void RefreshHumanStatusUI()
    {
        if (!isInit)
        {
            return;
        }
        //Edu
        for (int i = 0; i < listExpEdu.Count; i++)
        {
            if (i < parent.LevelEdu)
            {
                listExpEdu[i].RefreshExp(true, 1f);
            }
            else if (i == parent.LevelEdu)
            {
                listExpEdu[i].RefreshExp(false, parent.RateEdu);
            }
            else
            {
                listExpEdu[i].RefreshExp(false, 0);
            }
        }
        //Career
        for (int i = 0; i < listExpCareer.Count; i++)
        {
            if (i < parent.LevelCareer)
            {
                listExpCareer[i].RefreshExp(true, 1f);
            }
            else if (i == parent.LevelCareer)
            {
                listExpCareer[i].RefreshExp(false, parent.RateCareer);
            }
            else
            {
                listExpCareer[i].RefreshExp(false, 0);
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

                EffectUIInfo info = new EffectUIInfo("LevelUp", PublicTool.CalculateUICanvasPos(parent.tfHumanHead.position, GameMgr.Instance.mapCamera), parent.LevelEdu);
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
                EffectUIInfo info = new EffectUIInfo("LevelUp", PublicTool.CalculateUICanvasPos(parent.tfHumanHead.position, GameMgr.Instance.mapCamera), parent.LevelCareer);
                EventCenter.Instance.EventTrigger("EffectUI", info);
            }
        }
    }
}
