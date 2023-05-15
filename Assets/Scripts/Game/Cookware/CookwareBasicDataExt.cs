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
                if(cookType == CookwareType.Marriage)
                {

                }
                else
                {
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
                if (cookType == CookwareType.Marriage)
                {

                }
                else
                {
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

                }
                else
                {
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

                }
            }
            return 0;
        }
    }
}
