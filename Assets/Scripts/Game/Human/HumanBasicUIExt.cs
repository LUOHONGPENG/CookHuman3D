using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public partial class HumanBasic
{
    [Header("HumanUI")]
    public Transform tfHumanHead;
    public Transform tfRootUI;
    public Text txAge;
    public Image imgAgeFill;
    public List<Color> listColorFill = new List<Color>();

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
        tfRootUI.localPosition = PublicTool.CalculateUICanvasPos(tfHumanHead.position, GameMgr.Instance.mapCamera);//+ new Vector3(0, 100f, 0)
        //Age Data
        txAge.text = Age.ToString();
        //Age Fill Check
        switch (humanState)
        {
            case HumanState.Studying:
                imgAgeFill.fillAmount = PublicTool.CalculateEduRate(humanItem.expEdu);
                imgAgeFill.color = listColorFill[1];
                break;
            case HumanState.Working:
                imgAgeFill.fillAmount = PublicTool.CalculateEduRate(humanItem.expCareer);
                imgAgeFill.color = listColorFill[2];
                break;
            default:
                imgAgeFill.fillAmount = 1f;
                imgAgeFill.color = listColorFill[0];
                break;
        }
    }
}
