using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using LootLocker.Requests;


public class EndUIMgr : MonoBehaviour
{
    public enum EndPageType
    {
        Human,
        Upload,
        Leaderboard,
        About
    }

    public GameObject objPopup;
    [Header("Human")]
    public GameObject objHuman;
    public Transform tfMeal;
    public GameObject pfMeal;
    public Text codeScoreHuman;
    [Header("Upload")]
    public GameObject objUpload;
    public Text txTipTypeName;
    public InputField inputName;
    public Text codeScoreUpload;
    [Header("Leaderboard")]
    public GameObject objLeaderboard;
    public Transform tfScore;
    public GameObject pfScore;

    [Header("About")]
    public GameObject objAbout;

    [Header("Button")]
    public Button btnUpload;
    public Button btnRestart;
    public Button btnBack;
    public Button btnLeaderboard;
    public Button btnAbout;
    public Text txTitle;

    private int leaderboardID = 14649;
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

        btnAbout.onClick.RemoveAllListeners();
        btnAbout.onClick.AddListener(delegate ()
        {
            ShowAbout();
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
        finalScore += GameMgr.Instance.scorePenalty;
        //Init Text
        codeScoreHuman.text = finalScore.ToString();
        codeScoreUpload.text = finalScore.ToString();
        //Basic
        GameMgr.Instance.isEndPageOn = true;
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
        objAbout.SetActive(false);

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
        btnAbout.gameObject.SetActive(true);

        pageType = EndPageType.Human;
        txTitle.text = "Summary";
    }

    public void ShowUploadPage()
    {
        objHuman.SetActive(false);
        objUpload.SetActive(true);
        objLeaderboard.SetActive(false);
        objAbout.SetActive(false);

        btnUpload.gameObject.SetActive(true);
        btnRestart.gameObject.SetActive(false);
        btnBack.gameObject.SetActive(true);
        btnLeaderboard.gameObject.SetActive(false);
        btnAbout.gameObject.SetActive(false);

        pageType = EndPageType.Upload;
        txTitle.text = "Upload";
    }

    public void ShowLeaderBoard()
    {
        objHuman.SetActive(false);
        objUpload.SetActive(false);
        objLeaderboard.SetActive(true);
        objAbout.SetActive(false);

        btnUpload.gameObject.SetActive(false);
        btnRestart.gameObject.SetActive(false);
        btnBack.gameObject.SetActive(true);
        btnLeaderboard.gameObject.SetActive(false);
        btnAbout.gameObject.SetActive(false);

        InitLeaderBoard();

        pageType = EndPageType.Leaderboard;
        txTitle.text = "Leaderboard";

    }

    public void ShowAbout()
    {
        objHuman.SetActive(false);
        objUpload.SetActive(false);
        objLeaderboard.SetActive(false);
        objAbout.SetActive(true);

        btnUpload.gameObject.SetActive(false);
        btnRestart.gameObject.SetActive(false);
        btnBack.gameObject.SetActive(true);
        btnLeaderboard.gameObject.SetActive(false);
        btnAbout.gameObject.SetActive(false);
        txTitle.text = "About";

    }

    #endregion

    #region ButtonFunction

    public void UploadScore()
    {
        if (inputName.text.Length > 0 && inputName.text.Length <= 8 && CheckAllCanASCII(inputName.text))
        {
            SetPlayerName();
            StartCoroutine(SubmitScoreRoutine(finalScore, PlayerPrefs.GetString("PlayerID")));
            isUpload = true;
            ShowHumanPage();
        }
        else
        {
            txTipTypeName.gameObject.SetActive(true);
        }
    }

    public void RestartGame()
    {
        objPopup.SetActive(false);
        GameMgr.Instance.isEndPageOn = false;
        GameMgr.Instance.StartGame();
        //SceneManager.LoadScene("Main");
    }

    #endregion


    #region AboutLeaderBoard

    public void SetPlayerName()
    {
        LootLockerSDKManager.SetPlayerName(inputName.text, (response) =>
        {
            if (response.success)
            {
                Debug.Log("Successfully set player name");
            }
            else
            {
                Debug.Log("Could not set player name");
            }
        });
    }

    public long GetASCII(string str)
    {
        long num = 0;
        for (int i = 0; i < str.Length; i++)
        {
            num = num * 100 + TranslateChar(str[i]);
        }
        return num;
    }

    public int TranslateChar(char digit)
    {
        int index = 0;
        if ('0' <= digit && digit <= '9')
        {
            index = 1 + digit - '0';
        }
        else if ('a' <= digit && digit <= 'z')
        {
            index = 11 + digit - 'a';
        }
        else if ('A' <= digit && digit <= 'Z')
        {
            index = 37 + digit - 'A';
        }
        return index;
    }

    public bool CheckAllCanASCII(string str)
    {
        for (int i = 0; i < str.Length; i++)
        {
            if ('0' <= str[i] && str[i] <= '9')
            {
                continue;
            }
            if ('a' <= str[i] && str[i] <= 'z')
            {
                continue;
            }
            if ('A' <= str[i] && str[i] <= 'Z')
            {
                continue;
            }
            return false;
        }
        return true;
    }


    [System.Obsolete]
    public IEnumerator SubmitScoreRoutine(float time, string playerName)
    {
        bool done = false;
        //string nowTime = GetUnixTime().ToString();
        string memberID = GetASCII(inputName.text).ToString();

        int score = finalScore;
        LootLockerSDKManager.SubmitScore(memberID, score, leaderboardID, inputName.text, (response) =>
        {
            if (response.success)
            {
                Debug.Log("Successfully uploaded time");
                done = true;
            }
            else
            {
                Debug.Log("Failed" + response.Error);
                done = true;
            }
        });
        yield return new WaitWhile(() => done == false);
    }

    [System.Obsolete]
    public void InitLeaderBoard()
    {
        PublicTool.ClearChildItem(tfScore);
        StartCoroutine(FetchLeaderBoardRoutine());
    }

    [System.Obsolete]

    public IEnumerator FetchLeaderBoardRoutine()
    {
        bool done = false;
        LootLockerSDKManager.GetScoreListMain(leaderboardID, 100, 0, (response) =>
        {
            if (response.success)
            {
                LootLockerLeaderboardMember[] members = response.items;

                for (int i = 0; i < members.Length; i++)
                {
                    GameObject objMember = GameObject.Instantiate(pfScore, tfScore);
                    LeaderBoardUIItem itemMember = objMember.GetComponent<LeaderBoardUIItem>();
                    itemMember.Init(i + 1, members[i].metadata, members[i].score);
                }
                done = true;
            }
            else
            {
                Debug.Log("Failed" + response.Error);
                done = true;
            }

        });
        yield return new WaitWhile(() => done == false);
    }

    #endregion
}
