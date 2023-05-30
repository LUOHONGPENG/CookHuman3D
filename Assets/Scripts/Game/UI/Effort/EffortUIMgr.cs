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

    #region
    public void Init()
    {
        btnClose.onClick.RemoveAllListeners();
        btnClose.onClick.AddListener(delegate ()
        {
            objPopup.SetActive(false);
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
        objPopup.SetActive(true);
    }
    #endregion
}
