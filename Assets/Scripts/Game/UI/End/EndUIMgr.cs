using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class EndUIMgr : MonoBehaviour
{
    public enum EndPageType
    {
        Human,
        Upload,
        Leaderboard
    }

    public GameObject objPopup;
    [Header("Human")]
    public GameObject objHuman;
    public Transform tfMeal;
    public GameObject pfMeal;
    public Text codeScoreHuman;
    [Header("Upload")]
    public GameObject objUpload;
    public Text codeScoreUpload;
    [Header("Leaderboard")]
    public GameObject objLeaderboard;
    public Transform tfScore;
    public Transform pfScore;
    [Header("Button")]
    public Button btnUpload;
    public Button btnRestart;
    public Button btnBack;
    public Button btnLeaderboard;

    private int finalScore = 0;
    private bool isUpload = false;
    private EndPageType pageType = EndPageType.Human;

    #region Basic
    public void Init()
    {
        btnUpload.onClick.RemoveAllListeners();
        btnUpload.onClick.AddListener(delegate ()
        {
            if(pageType == EndPageType.Human)
            {
                ShowUploadPage();
            }
            else if(pageType == EndPageType.Upload)
            {
                UploadScore();
            }
        });

        btnRestart.onClick.RemoveAllListeners();
        btnRestart.onClick.AddListener(delegate () {
            RestartGame();
        });

        btnBack.onClick.RemoveAllListeners();
        btnBack.onClick.AddListener(delegate ()
        {
            ShowHumanPage();
        });

        btnLeaderboard.onClick.RemoveAllListeners();
        btnLeaderboard.onClick.AddListener(delegate ()
        {
            ShowLeaderBoard();
        });
    }

    private void OnEnable()
    {
        EventCenter.Instance.AddEventListener("EndGame", ShowEndPopup);
    }

    private void OnDestroy()
    {
        EventCenter.Instance.RemoveEventListener("EndGame", ShowEndPopup);
    }

    public void ShowEndPopup(object arg0)
    {
        finalScore = 0;
        isUpload = false;
        //CalculateFinalScore & InitHuman
        List<HumanItem> listHuman = GameMgr.Instance.mapMgr.listHumanItem;
        for (int i = 0; i < listHuman.Count; i++)
        {
            GameObject objMeal = GameObject.Instantiate(pfMeal, tfMeal);
            EndUIMealItem itemMeal = objMeal.GetComponent<EndUIMealItem>();
            itemMeal.Init(listHuman[i].vScore);
            finalScore += listHuman[i].vScore;
        }
        //Init Text
        codeScoreHuman.text = finalScore.ToString();
        codeScoreUpload.text = finalScore.ToString();
        //Basic
        GameMgr.Instance.isPageOn = true;
        objPopup.SetActive(true);
        ShowHumanPage();
    }
    #endregion

    #region ShowPage
    public void ShowHumanPage()
    {
        objHuman.SetActive(true);
        objUpload.SetActive(false);
        objLeaderboard.SetActive(false);

        if (isUpload)
        {
            btnUpload.gameObject.SetActive(false);
        }
        else
        {
            btnUpload.gameObject.SetActive(true);
        }
        btnRestart.gameObject.SetActive(true);
        btnBack.gameObject.SetActive(false);
        btnLeaderboard.gameObject.SetActive(true);

        pageType = EndPageType.Human;
    }

    public void ShowUploadPage()
    {
        objHuman.SetActive(false);
        objUpload.SetActive(true);
        objLeaderboard.SetActive(false);
        
        btnUpload.gameObject.SetActive(true);
        btnRestart.gameObject.SetActive(false);
        btnBack.gameObject.SetActive(true);
        btnLeaderboard.gameObject.SetActive(false);

        pageType = EndPageType.Upload;
    }

    public void ShowLeaderBoard()
    {
        objHuman.SetActive(false);
        objUpload.SetActive(false);
        objLeaderboard.SetActive(true);

        btnUpload.gameObject.SetActive(false);
        btnRestart.gameObject.SetActive(false);
        btnBack.gameObject.SetActive(true);
        btnLeaderboard.gameObject.SetActive(false);

        pageType = EndPageType.Leaderboard;
    }
    #endregion

    #region ButtonFunction

    public void UploadScore()
    {

    }

    public void RestartGame()
    {
        objPopup.SetActive(false);
        GameMgr.Instance.isPageOn = false;
        SceneManager.LoadScene("Main");
    }

    #endregion


}
