using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CookwareBasic : MonoBehaviour
{
    [Header("BasicInfo")]
    public CookwareType cookType;
    public int cookID;

    private CookwareExcelItem thisItem;

    public void Init(int ID)
    {
        //Load the item data
        this.cookID = ID;
        thisItem = DataMgr.Instance.cookwareData.GetExcelItem(cookID);


    }
}
