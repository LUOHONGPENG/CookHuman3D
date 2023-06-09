using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class RetireMiniUIMgr : MonoBehaviour
{
    public GameObject objBlock;
    public RectTransform rtInfo;
    public RectTransform rtBg;
    public Button btnClose;
    public Button btnPause;
    public Image imgTime;
    public GameObject objClick;


    [Header("Score")]
    public Text codeScore;

    [Header("Comment")]
    public Transform tfComment;
    public GameObject pfComment;
    private int numComment = 0;

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
            btnPause.interactable = false;
            imgTime.gameObject.SetActive(false);
            GameMgr.Instance.isRetirePageOn = true;
            objBlock.gameObject.SetActive(true);
            objClick.SetActive(false);
        });

        isInit = true;
    }

    public void StartGame()
    {
        objBlock.SetActive(false);
        rtInfo.anchoredPosition = new Vector2(1000f, -40f);
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

        rtBg.sizeDelta = new Vector2(400f, 272f + 40f * numComment);
        objClick.SetActive(true);

        if (GameMgr.Instance.mapMgr.listHumanBasic.Count > 1)
        {
            btnPause.interactable = true;
            DOTween.To(() => rtInfo.anchoredPosition, x => rtInfo.anchoredPosition = x, new Vector2(-25f, -40f), 0.5f);
            timerClose = 3f;
            imgTime.gameObject.SetActive(true);
        }

        human.isDead = true;
    }

    public void HidePopup()
    {
        DOTween.To(() => rtInfo.anchoredPosition, x => rtInfo.anchoredPosition = x, new Vector2(1000f, -40f), 0.5f);
        GameMgr.Instance.isRetirePageOn = false;
        objBlock.gameObject.SetActive(false);
    }


    private void Update()
    {
        if (timerClose > 0)
        {
            timerClose -= Time.deltaTime;
            imgTime.fillAmount = timerClose / 3F;
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
        numComment = listScore.Count;

        for (int i = 0; i < listScore.Count; i++)
        {
            CreateComment(listScore[i].desc, listScore[i].score);
            totalScore += listScore[i].score;
        }

        return totalScore;
    }
    #endregion
}
