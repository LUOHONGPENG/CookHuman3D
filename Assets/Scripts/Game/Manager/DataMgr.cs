using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataMgr : Singleton<DataMgr>
{
    /// <summary>
    /// The data of Cookware
    /// </summary>
    public CookwareExcelData cookwareData;
    public MarryConditionExcelData marryConditionData;
    public EffortExcelData effortExcelData;
    public RetireScoreExcelData retireScoreData;
    public TutorialExcelData tutorialExcelData;

    public void Init()
    {
        //Cookware
        cookwareData = ExcelManager.Instance.GetExcelData<CookwareExcelData, CookwareExcelItem>();
        marryConditionData = ExcelManager.Instance.GetExcelData<MarryConditionExcelData, MarryConditionExcelItem>();
        //Effort
        effortExcelData = ExcelManager.Instance.GetExcelData<EffortExcelData, EffortExcelItem>();
        //Score
        retireScoreData = ExcelManager.Instance.GetExcelData<RetireScoreExcelData, RetireScoreExcelItem>();
        retireScoreData.Init();
        //Tutorial
        tutorialExcelData = ExcelManager.Instance.GetExcelData<TutorialExcelData, TutorialExcelItem>();
        //Special Value
        SpecialValueExcelItem specialItem = ExcelManager.Instance.GetExcelData<SpecialValueExcelData, SpecialValueExcelItem>().GetExcelItem(1001);
        GameGlobal.ageMinStudy = specialItem.ageMin_study;
        GameGlobal.ageMaxStudy = specialItem.ageMax_study;

        GameGlobal.ageStartGap = specialItem.ageStartGap;
        GameGlobal.ageEndGap = specialItem.ageEndGap;

        GameGlobal.yearMarryM = specialItem.yearMarryM;
        GameGlobal.yearMarryF = specialItem.yearMarryF;

        GameGlobal.expEduLevelLimit = specialItem.expEduLevel;
        GameGlobal.expCareerLevelLimit = specialItem.expCareerLevel;

        Debug.Log("DataMgrInit");
    }
}
