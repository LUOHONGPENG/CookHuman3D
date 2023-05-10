using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class HumanBasic
{
    #region BasicData
    //The data of this human
    public HumanItem humanItem;
    //The data of currentCookware
    public CookwareExcelItem curCookware; 

    public void BindCookware(CookwareExcelItem cookware)
    {
        curCookware = cookware;
    }

    public void UnBindCookware()
    {
        curCookware = null;
    }

    public bool isInSchool
    {
        get
        {
            if(curCookware.cookwareType == CookwareType.Study)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }

    public bool isInJob
    {
        get
        {
            if(curCookware.cookwareType == CookwareType.Job)
            {
                return true;
            }
            else
            {
                return false;

            }
        }
    }
    #endregion

    #region Time
    private float yearGrow = 1f;


    public void TimeGo()
    {
        TimeGoData();
    }

    public void TimeGoData()
    {
        float yearDelta = Time.deltaTime / GameGlobal.timeOneYear;
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
        if (isInSchool)
        {
            float eduDelta = curCookware.growRate * yearDelta;
            humanItem.TimeGoRecordSchool(yearDelta, eduDelta);
        }

        if (isInJob)
        {
            float careerDelta = curCookware.growRate * yearDelta;
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
