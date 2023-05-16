using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RetireUIMgr : MonoBehaviour
{
    [Header("BasicUI")]
    public GameObject objPopup;
    public Button btnClose;

    [Header("Score")]
    public Text codeScore;

    [Header("Comment")]
    public Transform tfComment;
    public GameObject pfComment;

    private HumanBasic storedHuman;

    public void Init()
    {
        btnClose.onClick.RemoveAllListeners();
        btnClose.onClick.AddListener(HidePopup);
    }

    public void OnEnable()
    {
        EventCenter.Instance.AddEventListener("RetirePage", ShowPopup);
    }

    public void OnDestroy()
    {
        EventCenter.Instance.RemoveEventListener("RetirePage", ShowPopup);
    }

    public void ShowPopup(object arg0)
    {
        HumanBasic human = (HumanBasic)arg0;
        this.storedHuman = human;
        HumanItem humanItem = human.humanItem;

        PublicTool.ClearChildItem(tfComment);
        int vScore = CalculateScore(humanItem);
/*        GameMgr.Instance.levelManager.totalScore += vScore;
        GameMgr.Instance.levelManager.listMealScore.Add(vScore);*/
        codeScore.text = vScore.ToString();

        objPopup.SetActive(true);
        GameMgr.Instance.isPageOn = true;
    }

    public void HidePopup()
    {
        if (storedHuman != null)
        {
            storedHuman.isDead = true;
        }
        objPopup.SetActive(false);
        GameMgr.Instance.isPageOn = false;
    }

    #region Generate Score & Comment
    public void CreateComment(string strComment, int vScore)
    {
        GameObject objComment = GameObject.Instantiate(pfComment, tfComment);
        RetireUIComment itemComment = objComment.GetComponent<RetireUIComment>();
        itemComment.Init(strComment, vScore);
    }

    public int CalculateScore(HumanItem humanItem)
    {
        string strComment = "";
        int totalScore = 0;
        int tempScore = 0;
/*        //Education
        if (humanItem.vEdu > 60)
        {
            strComment = "High Education. So clean!";
        }
        else if (humanItem.vEdu >= 20)
        {
            strComment = "Normal Education. Not bad!";
        }
        else
        {
            strComment = "Poor Education. Dirty!";
        }
        tempScore = -300 + Mathf.RoundToInt(humanModel.vEdu) * 15;
        CreateComment(strComment, tempScore);
        totalScore += tempScore;


        //Career
        if (humanModel.vCareer > 60)
        {
            strComment = "Nice Career. Smooth taste!";
        }
        else if (humanModel.vCareer >= 30)
        {
            strComment = "Normal Career. Not bad!";
        }
        else
        {
            strComment = "Poor Career. Woody taste!";
        }
        tempScore = -600 + Mathf.RoundToInt(humanModel.vCareer) * 20;
        CreateComment(strComment, tempScore);
        totalScore += tempScore;

        //Marriage
        if (humanModel.isMarried)
        {
            if (humanModel.vMarryAge >= 40)
            {
                strComment = "Too Late to Marry!";
                tempScore = -200;
            }
            else if (humanModel.vMarryAge >= 30)
            {
                strComment = "Married.";
                tempScore = 200;
            }
            else
            {
                strComment = "Quick Marry.";
                tempScore = 500;
            }
        }
        else
        {
            strComment = "Single?";
            tempScore = -1000;
        }
        CreateComment(strComment, tempScore);
        totalScore += tempScore;

        //Gap Year
        if(humanModel.vLastStudyAge > 0 && humanModel.vFirstWorkAge - humanModel.vLastStudyAge > 1)
        {
            int vGapYear = Mathf.FloorToInt(humanModel.vFirstWorkAge - humanModel.vLastStudyAge);
            strComment = string.Format("How Dare You Gap {0} Year?!", vGapYear);
            tempScore = -200 * vGapYear;
            CreateComment(strComment, tempScore);
            totalScore += tempScore;
        }

        //EarlyStudy
        if(humanModel.vFirstStudyAge < GameGlobal.ageMin_School)
        {
            int vEarlyStudy = Mathf.FloorToInt(GameGlobal.ageMin_School - humanModel.vFirstStudyAge);
            strComment = string.Format("Oh! Early Study");
            tempScore = +100 * vEarlyStudy;
            CreateComment(strComment, tempScore);
            totalScore += tempScore;
        }

        //Late Graduation
        if (humanModel.vDelayGraduationYear > 1f)
        {
            int vDelay = Mathf.FloorToInt(humanModel.vDelayGraduationYear);
            strComment = string.Format("Late Graduation?!");
            tempScore = -100 * vDelay;
            CreateComment(strComment, tempScore);
            totalScore += tempScore;
        }*/

        return totalScore;
    }


    #endregion
}
