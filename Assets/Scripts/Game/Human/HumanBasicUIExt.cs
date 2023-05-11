using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public partial class HumanBasic
{
    [Header("HumanUI")]
    public Transform tfAgeUI;
    public Text txAge;

    private void Update()
    {
        RefreshHumanUI();
    }

    public void RefreshHumanUI()
    {
        if (!isInit)
        {
            return;
        }
        //UI Position
        tfAgeUI.localPosition = PublicTool.CalculateUICanvasPos(this.transform.position, GameMgr.Instance.mapCamera) + new Vector3(0, 100f, 0);
        txAge.text = Mathf.FloorToInt(humanItem.Age).ToString();
    }
}
