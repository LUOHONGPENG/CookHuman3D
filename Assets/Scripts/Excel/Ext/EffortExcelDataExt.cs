using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class EffortExcelData
{
    public List<int> GetEffortIDList()
    {
        List<int> listEffortID = new List<int>();

        for(int i = 0;i < items.Length; i++)
        {
            listEffortID.Add(items[i].id);
        }
        return listEffortID;
    }
}
