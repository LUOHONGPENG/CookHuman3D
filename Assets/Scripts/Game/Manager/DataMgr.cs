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


        Debug.Log("DataMgrInit");
    }
}
