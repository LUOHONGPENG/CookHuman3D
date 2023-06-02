using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class CookwareBasic
{
    public int AgeMin_real
    {
        get
        {
            if (cookItem != null)
            {
                switch (cookType)
                {
                    case CookwareType.Study:
                        if (PublicTool.CheckWhetherEffortGot(1002))
                        {
                            return Mathf.FloorToInt(PublicTool.GetEffortItem(1002).value0);
                        }
                        else
                        {
                            return cookItem.ageMin_real;
                        }
                    case CookwareType.Job:
                        if (PublicTool.CheckWhetherEffortGot(1005))
                        {
                            int tempAge = Mathf.FloorToInt(PublicTool.GetEffortItem(1005).value0);
                            if(cookItem.ageMin_real >= tempAge)
                            {
                                return tempAge;
                            }
                        }
                        return cookItem.ageMin_real;
                    case CookwareType.Marriage:
                        return ageMinMarry;
                    default:
                        return cookItem.ageMin_real;
                }
            }
            return 0;
        }
    }

    public int AgeMax_real
    {
        get
        {
            if (cookItem != null)
            {
                switch (cookType)
                {
                    case CookwareType.Marriage:
                        return ageMaxMarry;
                    default:
                        return cookItem.ageMax_real;
                }
            }
            return 0;
        }
    }

    public int eduMin
    {
        get
        {
            if (cookItem != null)
            {
                if (cookType == CookwareType.Marriage)
                {
                    return eduMinMarry;
                }
                else
                {
                    if(cookType == CookwareType.Job)
                    {
                        if (PublicTool.CheckWhetherEffortGot(1007))
                        {
                            if (cookItem.eduMin > 1)
                            {
                                return cookItem.eduMin-1;
                            }
                        }

                    }
                    return cookItem.eduMin;
                }
            }
            return 0;
        }
    }

    public int CareerMin
    {
        get
        {
            if(cookItem != null)
            {
                if (cookType == CookwareType.Marriage)
                {
                    return careerMinMarry;
                }
            }
            return 0;
        }
    }

    public string GetAgeString()
    {
        switch (cookType)
        {
            case CookwareType.Retire:
                return string.Format("{0}+", AgeMin_real);
            default:
                return string.Format("{0}-{1}", AgeMin_real, AgeMax_real);
        }
    }

    public string GetDesc()
    {
        return cookItem.desc;
    }
}
