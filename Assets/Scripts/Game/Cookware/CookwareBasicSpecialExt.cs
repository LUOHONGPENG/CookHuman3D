using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class CookwareBasic
{
    private int ageMinMarry = 0;
    private int ageMaxMarry = 0;
    private int eduMinMarry = 0;
    private int careerMinMarry = 0;
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
        ageMinMarry = 18 + Random.Range(-2, 2);
        ageMaxMarry = 40 + Random.Range(-2, 2);
        eduMinMarry = Random.Range(0, 2);
        careerMinMarry = Random.Range(0, 2);
    }

}
