using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public partial class HumanBasic
{
    [Header("HumanUI")]
    public Canvas canvasUI;
    public Transform tfHumanHead;
    public Transform tfRootUI;
    public Text txAge;
    public Image imgAgeFill;
    public List<Color> listColorFill = new List<Color>();

    private int lastEduLevel = -1;
    private int lastCareerLevel = -1;


    private void Update()
    {
        RefreshHumanUI();
        CheckLevelUp();
    }

    private void RefreshHumanUI()
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
            case HumanState.Marrying:
                imgAgeFill.fillAmount = 1f - (yearMarriage / 1f);
                imgAgeFill.color = listColorFill[3];
                break;
            default:
                imgAgeFill.fillAmount = 1f;
                imgAgeFill.color = listColorFill[0];
                break;
        }
    }

    private void CheckLevelUp()
    {
        if(humanState == HumanState.Studying)
        {
            if (lastEduLevel < 0)
            {
                lastEduLevel = LevelEdu;
            }
            else if (LevelEdu > lastEduLevel)
            {
                lastEduLevel = LevelEdu;

                EffectUIInfo info = new EffectUIInfo("LevelUp",PublicTool.CalculateUICanvasPos(tfHumanHead.position,GameMgr.Instance.mapCamera),LevelEdu);
                EventCenter.Instance.EventTrigger("EffectUI", info);
            }
        }
        else if(humanState == HumanState.Working)
        {
            if (lastCareerLevel < 0)
            {
                lastCareerLevel = LevelCareer;
            }
            else if (LevelCareer > lastCareerLevel)
            {
                lastCareerLevel = LevelCareer;
                EffectUIInfo info = new EffectUIInfo("LevelUp", PublicTool.CalculateUICanvasPos(tfHumanHead.position, GameMgr.Instance.mapCamera), LevelCareer);
                EventCenter.Instance.EventTrigger("EffectUI", info);
            }
        }
    }
}
