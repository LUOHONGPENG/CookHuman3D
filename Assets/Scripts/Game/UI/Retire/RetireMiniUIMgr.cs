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
    private float timerClose = 3f;
    private bool isInit = false;

    #region Basic

    public void Init()
    {
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

    public void StartGame()
    {
        objBlock.SetActive(false);
        rtInfo.anchoredPosition = new Vector2(1000f, 0);
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

        PublicTool.ClearChildItem(tfComment);
        //CalculateScore
        int vScore = CalculateScore(humanItem);
        codeScore.text = vScore.ToString();
        human.humanItem.vScore = vScore;

        if (GameMgr.Instance.mapMgr.listHumanBasic.Count > 1)
        {
            DOTween.To(() => rtInfo.anchoredPosition, x => rtInfo.anchoredPosition = x, new Vector2(0, 0), 0.5f);
            timerClose = 3f;
            btnPause.gameObject.SetActive(true);
        }

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
        List<ScoreInfo> listScore = humanItem.GetCommentList();

        int totalScore = 0;

        for(int i = 0; i < listScore.Count; i++)
        {
            CreateComment(listScore[i].desc, listScore[i].score);
            totalScore += listScore[i].score;
        }

        return totalScore;
    }
    #endregion
}
