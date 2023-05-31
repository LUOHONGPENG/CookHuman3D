using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public partial class HumanBasic
{
    #region BasicData
    //The data of this human
    [Header("Status")]
    [HideInInspector]
    public HumanItem humanItem;
    [HideInInspector]
    public bool isRetired = false;
    [HideInInspector]
    public bool isDead = false;
    [HideInInspector]
    public float yearMarriage = 0;
    [HideInInspector]
    public float maxYearMarriage = 0;
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
                else if(curCookware.cookType == CookwareType.Marriage)
                {
                    return HumanState.Marrying;
                }
                return HumanState.Rest;
            }
        }
    }

    public int Age { get { return humanItem.Age; } }

    public int LevelEdu { get { return PublicTool.CalculateEduLevel(humanItem.expEdu); } }
    public int LevelCareer { get { return PublicTool.CalculateCareerLevel(humanItem.expCareer); } }
    public float RateEdu { get { return PublicTool.CalculateEduRate(humanItem.expEdu); } }
    public float RateCareer { get { return PublicTool.CalculateCareerRate(humanItem.expCareer); } }


    #endregion

    #region TimeGo
    private float yearGrow = 1f;

    public void TimeGo()
    {
        TimeGoYear();
    }

    private void TimeGoYear()
    {
        float yearDelta = Time.fixedDeltaTime / GameGlobal.timeOneYear;
        //YearGrow
        yearGrow -= yearDelta;
        if (yearGrow < 0)
        {
            yearGrow = 1f;
            AgeGrow();
        }
        //DataGrow
        DataGrow(yearDelta);
        //MarriageCost
        if (yearMarriage > 0)
        {
            yearMarriage -= yearDelta;
        }
        else if(curCookware!=null && curCookware.cookType == CookwareType.Marriage)
        {
            humanItem.RecordMarried();
            EventCenter.Instance.EventTrigger("CreateBaby", null);
            PublicTool.PlaySound(SoundType.Marriage);
            UnBindCookware();
        }
    }

    //The data of education and career will grow according to the year passed.
    private void DataGrow(float yearDelta)
    {
        if (humanState == HumanState.Studying)
        {
            float eduDelta = curCookware.GetItem().ReadGrowRate * yearDelta;
            humanItem.TimeGoGrowEdu(eduDelta);

            if (PublicTool.CheckWhetherEffortGot(1004) && Age >= 16)
            {
                float extraCareerDelta = PublicTool.GetEffortItem(1004).value1 * yearDelta;
                humanItem.TimeGoGrowCareer(extraCareerDelta);
            }
        }

        if (humanState == HumanState.Working)
        {
            float careerDelta = curCookware.GetItem().ReadGrowRate * yearDelta;
            humanItem.TimeGoGrowCareer(careerDelta);
            //Send yearDelta
        }
    }

    //The event of this human age grow
    private void AgeGrow()
    {
        humanItem.AgeGrow();
    }

    #endregion
}
