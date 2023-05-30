using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class CookwareExcelData
{

}

public partial class CookwareExcelItem
{
    public float ReadGrowRate
    {
        get
        {
            if(cookwareType == CookwareType.Study)
            {
                if (PublicTool.CheckWhetherEffortGot(1001))
                {
                    return growRate + PublicTool.GetEffortItem(1001).value0;
                }
            }
            return growRate;
        }
    }
}