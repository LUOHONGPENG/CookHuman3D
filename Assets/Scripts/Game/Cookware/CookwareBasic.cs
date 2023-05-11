using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CookwareBasic : MonoBehaviour
{
    [Header("BasicInfo")]
    public CookwareType cookType;
    public int cookID;

    [Header("Human")]
    public Transform tfHuman;
    public HumanBasic curHuman;

    //The cookware data
    private CookwareExcelItem cookItem;

    #region Basic
    //Initialize the cookware
    public void Init(int ID)
    {
        //Load the item data
        this.cookID = ID;
        cookItem = DataMgr.Instance.cookwareData.GetExcelItem(cookID);
        cookType = cookItem.cookwareType;
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
        if (curHuman != null)
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
        curHuman = human;
    }

    //Unbind human
    public void UnbindHuman()
    {
        curHuman = null;
    }
    #endregion
}
