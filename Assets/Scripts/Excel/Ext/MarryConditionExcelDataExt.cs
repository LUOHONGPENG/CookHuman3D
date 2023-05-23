using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class MarryConditionExcelItem
{
    public int[] GetWeightEduM()
    {
        int[] array = new int[] { wEduM0, wEduM1, wEduM2, wEduM3, wEduM4, wEduM5 };
        return array;
    }

    public int[] GetWeightEduF()
    {
        int[] array = new int[] { wEduF0, wEduF1, wEduF2, wEduF3, wEduF4, wEduF5 };
        return array;
    }

    public int[] GetWeightCareerM()
    {
        int[] array = new int[] { wCareerM0, wCareerM1, wCareerM2, wCareerM3, wCareerM4, wCareerM5 };
        return array;
    }

    public int[] GetWeightCareerF()
    {
        int[] array = new int[] { wCareerF0, wCareerF1, wCareerF2, wCareerF3, wCareerF4, wCareerF5 };
        return array;
    }
}

public partial class MarryConditionExcelData
{
    public MarryConditionExcelItem GetMarryItem(int num)
    {
        for(int i = 0;i < items.Length - 1; i++)
        {
            if(num >= items[i].numMarriage && num < items[i+1].numMarriage)
            {
                return items[i];
            }
        }
        return items[items.Length - 1];
    }
}