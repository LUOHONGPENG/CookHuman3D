using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EffortUIMgr : MonoBehaviour
{
    public GameObject objPopup;
    public Button btnClose;

    public Transform tfEffort;
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

        PublicTool.ClearChildItem(tfEffort);
        List<int> listEffortID = GameMgr.Instance.listEffortPrepared;
        for(int i = 0;i < listEffortID.Count; i++)
        {
            GameObject objEffort = GameObject.Instantiate(pfEffort, tfEffort);
            EffortUIItem itemEffort = objEffort.GetComponent<EffortUIItem>();
            itemEffort.Init(listEffortID[i],this);
        }

        objPopup.SetActive(true);
    }

    public void HidePopup()
    {
        objPopup.SetActive(false);
        GameMgr.Instance.isEffortPageOn = false;
    }
    #endregion
}
