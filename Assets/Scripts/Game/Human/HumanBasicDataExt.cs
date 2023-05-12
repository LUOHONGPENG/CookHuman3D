using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public partial class HumanBasic
{
    #region BasicData
    //The data of this human
    public HumanItem humanItem;
    //Check and get the human's state
    public HumanState humanState
    {
        get
        {
            if (curCookware == null)
            {
                return HumanState.Rest;
            }
            else
            {
                if (curCookware.cookType == CookwareType.Study)
                {
                    return HumanState.Studying;
                }
                else if(curCookware.cookType == CookwareType.Job)
                {
                    return HumanState.Working;
                }
                return HumanState.Rest;
            }
        }
    }
    #endregion

    #region TimeGo
    private float yearGrow = 1f;

    public void TimeGo()
    {
        TimeGoData();
    }

    private void TimeGoData()
    {
        float yearDelta = Time.fixedDeltaTime / GameGlobal.timeOneYear;
        yearGrow -= yearDelta;
        if (yearGrow < 0)
        {
            yearGrow = 1f;
            AgeGrow();
        }
        DataGrow(yearDelta);
    }

    //The data of education and career will grow according to the year passed.
    private void DataGrow(float yearDelta)
    {
        if (humanState == HumanState.Studying)
        {
            float eduDelta = curCookware.GetItem().growRate * yearDelta;
            humanItem.TimeGoRecordSchool(yearDelta, eduDelta);
        }

        if (humanState == HumanState.Working)
        {
            float careerDelta = curCookware.GetItem().growRate * yearDelta;
            humanItem.TimeGoRecordJob(yearDelta, careerDelta);
        }
    }

    //The event of this human age grow
    private void AgeGrow()
    {
        humanItem.AgeGrow();
    }

    #endregion
}