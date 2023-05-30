using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EffortUIItem : MonoBehaviour
{
    public Button btnEffort;

    public Text txName;
    public Text txDesc;

    private EffortExcelItem effortItem;
    private EffortUIMgr parent;
    public void Init(int ID,EffortUIMgr parent)
    {
        this.parent = parent;
        effortItem = PublicTool.GetEffortItem(ID);

        if (effortItem != null)
        {
            txName.text = effortItem.name;
            txDesc.text = string.Format(effortItem.desc,effortItem.value0,effortItem.value1);

            btnEffort.onClick.RemoveAllListeners();
            btnEffort.onClick.AddListener(delegate ()
            {
                if(ID != 9999)
                {
                    GameMgr.Instance.listEffortGot.Add(ID);
                }
                GameMgr.Instance.ClearEffort();
                parent.HidePopup();
            });
        }
    }
}
