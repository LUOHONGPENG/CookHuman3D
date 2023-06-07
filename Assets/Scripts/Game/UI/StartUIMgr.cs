using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StartUIMgr : MonoBehaviour
{
    public GameObject objPopup;

    public Button btnStart;

    public void Init()
    {
        btnStart.onClick.RemoveAllListeners();
        btnStart.onClick.AddListener(delegate ()
        {
            ClickStart();
        });

        if (!GameGlobal.isStart)
        {
            objPopup.SetActive(true);
            GameGlobal.isStart = true;
        }
    }

    public void ClickStart()
    {
        objPopup.SetActive(false);
    }
}
