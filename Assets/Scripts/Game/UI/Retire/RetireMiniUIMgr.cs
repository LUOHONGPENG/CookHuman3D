using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class RetireMiniUIMgr : MonoBehaviour
{
    public GameObject objBlock;
    public RectTransform rtInfo;
    public Button btnClose;
    public Button btnPause;

    [Header("Score")]
    public Text codeScore;

    [Header("Comment")]
    public Transform tfComment;
    public GameObject pfComment;

    //Inner Data
    private float timerClose = 5f;
    private bool isInit = false;

    #region Basic

    public void Init()
    {
        objBlock.SetActive(false);

        rtInfo.anchoredPosition = new Vector2(1000f, 0);

        btnClose.onClick.RemoveAllListeners();
        btnClose.onClick.AddListener(delegate () {
            timerClose = 0;
        });

        btnPause.onClick.RemoveAllListeners();
        btnPause.onClick.AddListener(delegate ()
        {
            btnPause.gameObject.SetActive(false);
            GameMgr.Instance.isPageOn = true;
            objBlock.gameObject.SetActive(true);
        });

        isInit = true;
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
        HumanItem humanItem = human.humanItem;

        DOTween.To(() => rtInfo.anchoredPosition, x => rtInfo.anchoredPosition = x, new Vector2(0, 0), 0.5f);

        PublicTool.ClearChildItem(tfComment);
        //CalculateScore
        int vScore = CalculateScore(humanItem);
        codeScore.text = vScore.ToString();
        human.humanItem.vScore = vScore;

        timerClose = 5f;
        btnPause.gameObject.SetActive(true) ;

        human.isDead = true;
    }

    public void HidePopup()
    {
        DOTween.To(() => rtInfo.anchoredPosition, x => rtInfo.anchoredPosition = x, new Vector2(1000f, 0), 0.5f);
        GameMgr.Instance.isPageOn = false;
        objBlock.gameObject.SetActive(false);
    }


    private void Update()
    {
        if (timerClose > 0)
        {
            timerClose -= Time.deltaTime;
        }
        else
        {
            HidePopup();
        }
    }

    #endregion

    #region Generate Score & Comment
    public void CreateComment(string strComment, int vScore)
    {
        GameObject objComment = GameObject.Instantiate(pfComment, tfComment);
        RetireMiniCommentUI itemComment = objComment.GetComponent<RetireMiniCommentUI>();
        itemComment.Init(strComment, vScore);
    }

    public int CalculateScore(HumanItem humanItem)
    {
        RetireScoreExcelData scoreData = GameMgr.Instance.dataMgr.retireScoreData;

        int totalScore = 0;

        ScoreInfo scoreEdu = scoreData.CalculateScore(RetireScoreType.Edu, Mathf.FloorToInt(humanItem.expEdu));
        CreateComment(scoreEdu.desc, scoreEdu.score);
        totalScore += scoreEdu.score;

        ScoreInfo scoreCareer = scoreData.CalculateScore(RetireScoreType.Career, Mathf.FloorToInt(humanItem.expCareer));
        CreateComment(scoreCareer.desc, scoreCareer.score);
        totalScore += scoreCareer.score;

        ScoreInfo scoreMarriage = scoreData.CalculateScore(RetireScoreType.Marriage, humanItem.vMarryAge);
        CreateComment(scoreMarriage.desc, scoreMarriage.score);
        totalScore += scoreMarriage.score;

        return totalScore;
    }
    #endregion
}
