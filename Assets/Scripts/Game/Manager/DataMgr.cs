using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataMgr : Singleton<DataMgr>
{
    /// <summary>
    /// The data of Cookware
    /// </summary>
    public CookwareExcelData cookwareData;

    public void Init()
    {
        //Cookware
        cookwareData = ExcelManager.Instance.GetExcelData<CookwareExcelData, CookwareExcelItem>();
        //Special Value
        SpecialValueExcelItem specialItem = ExcelManager.Instance.GetExcelData<SpecialValueExcelData, SpecialValueExcelItem>().GetExcelItem(1001);
        GameGlobal.ageMinStudy = specialItem.ageMin_study;
        GameGlobal.ageMaxStudy = specialItem.ageMax_study;
        GameGlobal.expEduLevelLimit = specialItem.expEduLevel;
        GameGlobal.expCareerLevelLimit = specialItem.expCareerLevel;

        Debug.Log("DataMgrInit");
    }
}
