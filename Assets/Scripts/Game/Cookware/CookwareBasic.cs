using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public partial class CookwareBasic : MonoBehaviour
{
    [Header("BasicInfo")]
    public Transform tfCenter;
    public CookwareView itemView;
    [HideInInspector]
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
        //Temp
        this.itemView.transform.position = tfCenter.position;

        itemView.Init(this);

        isInit = true;
    }

    public void StartGame()
    {

        listCurHuman.Clear();

        itemView.StartGame();

        //Initialize Marriage
        if (cookType == CookwareType.Marriage)
        {
            RefreshMarryCondition();
        }
    }

    public void OnEnable()
    {
        EventCenter.Instance.AddEventListener("ReduceMarry", ReduceMarryCondition);
    }

    public void OnDestroy()
    {
        EventCenter.Instance.RemoveEventListener("ReduceMarry", ReduceMarryCondition);
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
        if (listCurHuman.Contains(human))
        {
            return false;
        }

        Vector3 headPos = human.tfHumanHead.position;
        //Full
        if (listCurHuman.Count >= cookCapacity)
        {
            PublicTool.WarningTip("No Space", headPos);
            PublicTool.PlaySound(SoundType.NoSpace);
            return false;
        }
        else if(human.Age < AgeMin_real)
        {
            PublicTool.WarningTip("Too Young", headPos);
            PublicTool.PlaySound(SoundType.TooYoung);
            return false;
        }
        else if (human.Age > AgeMax_real)
        {
            PublicTool.WarningTip("Too Old", headPos);
            PublicTool.PlaySound(SoundType.TooOld);
            return false;
        }
        else if (human.LevelEdu < eduMin)
        {
            PublicTool.WarningTip("Need Education", headPos);
            PublicTool.PlaySound(SoundType.MoreEdu);
            return false;
        }
        else if (human.LevelCareer < CareerMin)
        {
            PublicTool.WarningTip("Need Career", headPos);
            PublicTool.PlaySound(SoundType.MoreCareer);
            return false;
        }
        else if (cookType == CookwareType.Marriage)
        {
            if (human.humanItem.isMarried)
            {
                PublicTool.WarningTip("Married", headPos);
                PublicTool.PlaySound(SoundType.Married);
                return false;
            }
            else if (human.humanItem.sex != requiredSex)
            {
                if(requiredSex == Sex.Female)
                {
                    PublicTool.WarningTip("For Women", headPos);
                    PublicTool.PlaySound(SoundType.Gay);
                }
                else if(requiredSex == Sex.Male)
                {
                    PublicTool.WarningTip("For Men", headPos);
                    PublicTool.PlaySound(SoundType.Lesbian);
                }
                return false;
            }
            else if (GameMgr.Instance.mapMgr.listHumanBasic.Count >= 6)
            {
                PublicTool.WarningTip("No more than 6 humans", headPos);
                PublicTool.PlaySound(SoundType.NoSpace);
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
