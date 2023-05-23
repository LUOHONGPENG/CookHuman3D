using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class CookwareBasic
{
    private int ageMinMarry = 0;
    private int ageMaxMarry = 0;
    private int eduMinMarry = 0;
    private int careerMinMarry = 0;

    private MarryConditionExcelData marryConditionData;

    [HideInInspector]
    public Sex requiredSex;

    private void InvokeMarriage(HumanBasic human)
    {
        human.yearMarriage = 1f;
    }

    private void InvokeRetire(HumanBasic human)
    {
        human.isRetired = true;
    }

    private void RefreshMarryCondition()
    {
        if(cookID == 3001)
        {
            requiredSex = Sex.Female;
        }
        else if(cookID == 3002)
        {
            requiredSex = Sex.Male;
        }

        marryConditionData = GameMgr.Instance.dataMgr.marryConditionData;
        MarryConditionExcelItem marryConditionItem = marryConditionData.GetMarryItem(GameMgr.Instance.numMarry);

        if(requiredSex == Sex.Male)
        {
            ageMinMarry = marryConditionItem.ageMinM + Random.Range(-2, 2);
            ageMaxMarry = marryConditionItem.ageMaxM + Random.Range(-2, 2);
            eduMinMarry = PublicTool.GetRandomIndexIntArray(marryConditionItem.GetWeightEduM());
            careerMinMarry = PublicTool.GetRandomIndexIntArray(marryConditionItem.GetWeightCareerM());
        }
        else if(requiredSex == Sex.Female)
        {
            ageMinMarry = marryConditionItem.ageMinF + Random.Range(-2, 2);
            ageMaxMarry = marryConditionItem.ageMaxF + Random.Range(-2, 2);
            eduMinMarry = PublicTool.GetRandomIndexIntArray(marryConditionItem.GetWeightEduF());
            careerMinMarry = PublicTool.GetRandomIndexIntArray(marryConditionItem.GetWeightCareerF());
        }

        itemView.RefreshMarryUI();
    }

}
