using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HumanItem
{
    //Basic Data
    public int HumanID = -1;
    public Sex sex;
    public int Age = 0;
    public float expEdu = 0;//Value about Education
    public float expCareer = 0;//Value about Job
    public bool isMarried = false;//Whether this people is married
    //Special
    public int vMarryAge = -1;
    public int vScore = 0;

    public HumanItem(int ID)
    {
        //Initial Basic data
        this.HumanID = ID;
        this.Age = 0;
        this.isMarried = false;
        this.expEdu = 0;
        this.expCareer = 0;
        //Initial Sex data
        int ranSex = Random.Range(0, 2);
        if(ranSex == 0)
        {
            sex = Sex.Male;
        }
        else if(ranSex == 1)
        {
            sex = Sex.Female;
        }
        //Initial Score
        vScore = 0;
    }

    #region Basic Record
    /// <summary>
    /// The age grow
    /// </summary>
    public void AgeGrow()
    {
        this.Age++;
    }

    public void RecordMarried()
    {
        isMarried = true;
        vMarryAge = Age;
    }
    #endregion

    #region TimeGo

    public void TimeGoRecordSchool(float yearDelta,float eduDelta)
    {
        expEdu += eduDelta;
        if (expEdu > 100f)
        {
            expEdu = 100f;
        }
    }

    public void TimeGoRecordJob(float yearDelta, float careerDelta)
    {
        expCareer += careerDelta;
        if(expCareer > 100f)
        {
            expCareer = 100f;
        }
    }
    #endregion
}
