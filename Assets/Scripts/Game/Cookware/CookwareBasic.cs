using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public partial class CookwareBasic : MonoBehaviour
{
    [Header("BasicInfo")]
    public Transform tfModel;
    public CookwareView itemView;

    public CookwareType cookType;
    [HideInInspector]
    public int cookID;
    [HideInInspector]
    public int cookCapacity;
    [HideInInspector]
    public List<HumanBasic> listCurHuman = new List<HumanBasic>();



    private bool isInit = false;
    //The cookware data
    private CookwareExcelItem cookItem;

    #region Basic
    //Initialize the cookware
    public void Init(int ID)
    {
        //Load the item data
        this.cookID = ID;
        cookItem = DataMgr.Instance.cookwareData.GetExcelItem(cookID);
        this.cookType = cookItem.cookwareType;
        this.cookCapacity = cookItem.capacity;

        //InitView
        listCurHuman.Clear();
        itemView.Init(this);

        //Initialize Marriage
        if (cookType == CookwareType.Marriage)
        {
            RefreshMarryCondition();
        }

        isInit = true;
    }

    //Get the item data
    public CookwareExcelItem GetItem()
    {
        return cookItem;
    }
    #endregion

    #region Bind
    //Check whether the dragging human meet the condition
    public bool CheckHuman(HumanBasic human)
    {
        //Full
        if (listCurHuman.Count >= cookCapacity)
        {
            PublicTool.PlaySound(SoundType.NoSpace);
            return false;
        }
        else if(human.Age < AgeMin_real)
        {
            PublicTool.PlaySound(SoundType.TooYoung);
            return false;
        }
        else if (human.Age > AgeMax_real)
        {
            PublicTool.PlaySound(SoundType.TooOld);
            return false;
        }
        else if (human.LevelEdu < eduMin)
        {
            PublicTool.PlaySound(SoundType.MoreEdu);
            return false;
        }
        else if (human.LevelCareer < CareerMin)
        {
            PublicTool.PlaySound(SoundType.MoreCareer);
            return false;
        }
        else if (cookType == CookwareType.Marriage)
        {
            if (human.humanItem.isMarried)
            {
                PublicTool.PlaySound(SoundType.Married);
                return false;
            }
            else if (human.humanItem.sex != requiredSex)
            {
                return false;
            }
            else if (GameMgr.Instance.mapMgr.listHumanBasic.Count >= 6)
            {
                return false;
            }
        }
        return true;
    }

    //Bind human
    public void BindHuman(HumanBasic human)
    {
        listCurHuman.Add(human);
        SetHumanPos();
        //Invoke Special
        switch (cookType)
        {
            case CookwareType.Study:
                PublicTool.PlaySound(SoundType.Study);
                break;
            case CookwareType.Job:
                PublicTool.PlaySound(SoundType.Job);
                break;
            case CookwareType.Marriage:
                InvokeMarriage(human);
                break;
            case CookwareType.Retire:
                PublicTool.PlaySound(SoundType.Retire);
                InvokeRetire(human);
                break;
        }
    }

    //Unbind human
    public void UnbindHuman(HumanBasic human)
    {
        listCurHuman.Remove(human);
        SetHumanPos();
        if (cookType == CookwareType.Marriage)
        {
            RefreshMarryCondition();
        }
    }

    public void SetHumanPos()
    {
        for(int i = 0;i < listCurHuman.Count; i++)
        {
            //listCurHuman[i].transform.DOMove(listTfHuman[i].position,0.5f);
            listCurHuman[i].transform.position = itemView.listTfHuman[i].position;
            listCurHuman[i].posCookware = itemView.listTfHuman[i].position;
        }
    }
    #endregion
}
