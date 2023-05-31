using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EffortUIItem : MonoBehaviour
{
    public Button btnEffort;

    public Text txName;
    public Text txDesc;

    public GameObject objPenalty;
    public Text codePenalty;

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
                if(ID == 9999)
                {
                    EventCenter.Instance.EventTrigger("ReduceMarry", null);
                    //Cost Score
                    GameMgr.Instance.scorePenalty += GameMgr.Instance.CalculateCurPenalty();
                    GameMgr.Instance.numReduceMarry++;
                }
                else
                {
                    GameMgr.Instance.listEffortGot.Add(ID);
                }
                GameMgr.Instance.ClearEffort();
                //Refresh
                EventCenter.Instance.EventTrigger("ViewAllRefresh", null);
                parent.HidePopup();
            });
        }

        if(ID == 9999)
        {
            objPenalty.SetActive(true);
            codePenalty.text = string.Format("Score Penalty: "+ GameMgr.Instance.CalculateCurPenalty().ToString());
        }
        else
        {
            objPenalty.SetActive(false);
        }
    }
}
