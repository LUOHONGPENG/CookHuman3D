using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class CookwareBasic : MonoBehaviour
{
    [Header("BasicInfo")]
    public CookwareType cookType;
    private int cookID;
    private int cookCapacity;


    [Header("Human")]
    public List<Transform> listTfHuman = new List<Transform>();
    public List<HumanBasic> listCurHuman = new List<HumanBasic>();


    //The cookware data
    private CookwareExcelItem cookItem;

    #region Basic
    //Initialize the cookware
    public void Init(int ID)
    {
        //Load the item data
        this.cookID = ID;
        listCurHuman.Clear();
        cookItem = DataMgr.Instance.cookwareData.GetExcelItem(cookID);
        this.cookType = cookItem.cookwareType;
        this.cookCapacity = cookItem.capacity;
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
            return false;
        }
        else
        {
            return true;
        }
    }

    //Bind human
    public void BindHuman(HumanBasic human)
    {
        listCurHuman.Add(human);
        SetHumanPos();
    }

    //Unbind human
    public void UnbindHuman(HumanBasic human)
    {
        listCurHuman.Remove(human);
        SetHumanPos();
    }

    public void SetHumanPos()
    {
        for(int i = 0;i < listCurHuman.Count; i++)
        {
            //listCurHuman[i].transform.DOMove(listTfHuman[i].position,0.5f);
            listCurHuman[i].transform.position = listTfHuman[i].position;
            listCurHuman[i].posCookware = listTfHuman[i].position;
        }
    }
    #endregion
}
