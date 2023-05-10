using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HumanItem
{
    //Basic Data
    public int HumanID = -1;
    public Sex sex;
    public int Age = 0;
    public float vEdu = 0;//Value about Education
    public float vCareer = 0;//Value about Job
    public bool isMarried = false;//Whether this people is married
    //Special
    public int vMarryAge = -1;

    public HumanItem(int ID)
    {
        //Initial Basic data
        this.HumanID = ID;
        this.Age = 0;
        this.isMarried = false;
        this.vEdu = 0;
        this.vCareer = 0;
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

    public void TimeGoRecordSchool(float yearDelta,float eduDelta)
    {
        vEdu += eduDelta;
        if (vEdu > 100f)
        {
            vEdu = 100f;
        }
    }

    public void TimeGoRecordJob(float yearDelta, float careerDelta)
    {
        vCareer += careerDelta;
        if(vCareer > 100f)
        {
            vCareer = 100f;
        }
    }
}
