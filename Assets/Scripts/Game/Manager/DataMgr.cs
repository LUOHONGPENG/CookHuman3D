using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataMgr : Singleton<DataMgr>
{
    public CookwareExcelData cookwareData;

    public void Init()
    {
        cookwareData = ExcelManager.Instance.GetExcelData<CookwareExcelData, CookwareExcelItem>();

        Debug.Log("DataMgrInit");
    }
}
