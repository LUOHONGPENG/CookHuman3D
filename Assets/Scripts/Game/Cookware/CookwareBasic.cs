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
    public HumanBasic bindHuman;

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
    public bool BindHuman(HumanBasic humanBasic)
    {
        if (bindHuman != null)
        {
            return false;
        }
        else
        {
            //Find that target is not empty
            if (humanBasic.bindCookware!=null)
            {
                humanBasic.UnBindCookware();
            }
            //Bind this target
            humanBasic.BindCookware(this);
            bindHuman = humanBasic;
            return true;
        }
    }

    public void UnbindHuman()
    {
        if (bindHuman.bindCookware != null)
        {
            bindHuman.UnBindCookware();
        }
        bindHuman = null;
    }
    #endregion
}
