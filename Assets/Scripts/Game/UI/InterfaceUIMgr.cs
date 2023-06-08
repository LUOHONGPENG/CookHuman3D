using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InterfaceUIMgr : MonoBehaviour
{
    [Header("Score")]
    public Text codeScore;
    [Header("Effort")]
    public Button btnEffort;
    public Image imgEffortFill;
    public Animator aniEffort;
    public GameObject objPurpleParticle;
    public RectTransform rtEffort;
    [Header("Speed")]
    public Button btnNormal;
    public Button btnFast;
    public Material mGray;

    private bool isInit = false;

    #region Basic
    public void Init()
    {
        btnEffort.onClick.RemoveAllListeners();
        btnEffort.onClick.AddListener(delegate ()
        {
            EventCenter.Instance.EventTrigger("ShowEffortPage", null);
        });

        btnNormal.onClick.RemoveAllListeners();
        btnNormal.onClick.AddListener(delegate ()
        {
            SetNormalSpeed();
        });

        btnFast.onClick.RemoveAllListeners();
        btnFast.onClick.AddListener(delegate ()
        {
            SetFastSpeed();
        });

        isInit = true;
    }

    public void StartGame()
    {
        SetNormalSpeed();
        RefreshScore(null);
        RefreshEffort(null);
    }

    public void OnEnable()
    {
        EventCenter.Instance.AddEventListener("RefreshScore", RefreshScore);
        EventCenter.Instance.AddEventListener("ViewAllRefresh", RefreshScore);
        EventCenter.Instance.AddEventListener("RefreshEffort", RefreshEffort);
    }

    public void OnDestroy()
    {
        EventCenter.Instance.RemoveEventListener("RefreshScore", RefreshScore);
        EventCenter.Instance.RemoveEventListener("ViewAllRefresh", RefreshScore);
        EventCenter.Instance.RemoveEventListener("RefreshEffort", RefreshEffort);
    }
    #endregion

    #region Speed

    private void SetNormalSpeed()
    {
        GameMgr.Instance.globalTimeScale = 1f;
        btnNormal.GetComponent<Image>().material = null;
        btnFast.GetComponent<Image>().material = mGray;
    }

    private void SetFastSpeed()
    {
        GameMgr.Instance.globalTimeScale = 1.8f;
        btnNormal.GetComponent<Image>().material = mGray;
        btnFast.GetComponent<Image>().material = null;
    }
    #endregion


    private void RefreshScore(object arg0)
    {
        int totalScore = 0;
        List<HumanItem> listHuman = GameMgr.Instance.mapMgr.listHumanItem;
        for (int i = 0; i < listHuman.Count; i++)
        {
            totalScore += listHuman[i].vScore;
        }

        totalScore += GameMgr.Instance.scorePenalty;

        codeScore.text = totalScore.ToString();
    }

    private void RefreshEffort(object arg0)
    {
        imgEffortFill.fillAmount = GameMgr.Instance.numEffortCharge *1f / GameMgr.Instance.maxEffortCharge;
        if (GameMgr.Instance.numEffortCharge >= GameMgr.Instance.maxEffortCharge)
        {
            if(GameMgr.Instance.listEffortGot.Count >= GameMgr.Instance.maxEffortLimit)
            {
                objPurpleParticle.SetActive(false);
            }
            else
            {
                objPurpleParticle.SetActive(true);
            }
            btnEffort.interactable = true;
            aniEffort.enabled = true;
        }
        else
        {
            objPurpleParticle.SetActive(false);
            btnEffort.interactable = false;
            aniEffort.enabled = false;
        }
    }

    private void FirstEffort()
    {

    }
}
