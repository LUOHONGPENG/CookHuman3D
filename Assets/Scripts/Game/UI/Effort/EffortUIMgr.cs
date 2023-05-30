using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EffortUIMgr : MonoBehaviour
{
    public GameObject objPopup;
    public Button btnClose;

    public Transform tfEffortNormal;
    public Transform tfEffortSpecial;
    public GameObject pfEffort;

    #region Basic
    public void Init()
    {
        btnClose.onClick.RemoveAllListeners();
        btnClose.onClick.AddListener(delegate ()
        {
            HidePopup();
        });
    }

    private void OnEnable()
    {
        EventCenter.Instance.AddEventListener("ShowEffortPage", ShowPopup);
    }

    private void OnDestroy()
    {
        EventCenter.Instance.RemoveEventListener("ShowEffortPage", ShowPopup);
    }

    public void ShowPopup(object arg0)
    {
        GameMgr.Instance.isEffortPageOn = true;
        //Normal
        PublicTool.ClearChildItem(tfEffortNormal);
        List<int> listEffortID = GameMgr.Instance.listEffortPrepared;
        for(int i = 0;i < listEffortID.Count; i++)
        {
            GameObject objEffort = GameObject.Instantiate(pfEffort, tfEffortNormal);
            EffortUIItem itemEffort = objEffort.GetComponent<EffortUIItem>();
            itemEffort.Init(listEffortID[i],this);
        }
        //Special
        PublicTool.ClearChildItem(tfEffortSpecial);
        GameObject objSpecial = GameObject.Instantiate(pfEffort, tfEffortSpecial);
        EffortUIItem itemSpecial = objSpecial.GetComponent<EffortUIItem>();
        itemSpecial.Init(9999, this);

        objPopup.SetActive(true);
    }

    public void HidePopup()
    {
        objPopup.SetActive(false);
        GameMgr.Instance.isEffortPageOn = false;
    }
    #endregion
}
