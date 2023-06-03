using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using LootLocker.Requests;


public class GameMgr : MonoSingleton<GameMgr>
{
    [Header("Camera")]
    public Camera mapCamera;
    public Camera uiCamera;
    public EventSystem eventSystem;
    [Header("Manager")]
    public MapMgr mapMgr;
    public LightMgr lightMgr;
    public EffectUIMgr effectUIMgr;
    public UIMgr uiMgr;
    public SoundMgr soundMgr;
    public DataMgr dataMgr;

    private bool isInit = false;

    public bool isPageOn
    {
        get
        {
            if(isEffortPageOn || isTutorialPageOn || isRetirePageOn || isEndPageOn)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
    public bool isEffortPageOn = false;
    public bool isTutorialPageOn = false;
    public bool isRetirePageOn = false;
    public bool isEndPageOn = false;
    public float globalTimeScale = 1f;

    #region GameData

    public int numMarry = 0;
    //Effort
    public int numEffortCharge = 0;
    public int maxEffortCharge = 0;
    public int maxEffortLimit = 0;
    public int numReduceMarry = 0;
    public int scorePenalty = 0;
    public List<int> listEffortGot = new List<int>();
    public List<int> listEffortPrepared = new List<int>();

    private void InitGameData()
    {
        numMarry = 0;
        numEffortCharge = 0;
        maxEffortCharge = 3;
        maxEffortLimit = 3;
        numReduceMarry = 0;
        scorePenalty = 0;
        listEffortGot.Clear();
        listEffortPrepared.Clear();
    }

    public void ChargeEffort()
    {
        if (numEffortCharge < maxEffortCharge)
        {
            numEffortCharge++;
            EventCenter.Instance.EventTrigger("RefreshEffort", null);
            if(numEffortCharge >= maxEffortCharge)
            {
                listEffortPrepared.Clear();
                if (listEffortGot.Count < maxEffortLimit)
                {
                    DrawEffort();
                }
            }
        }
    }

    public void ClearEffort()
    {
        numEffortCharge = 0;
        EventCenter.Instance.EventTrigger("RefreshEffort", null);
    }

    public void DrawEffort()
    {
        listEffortPrepared = PublicTool.DrawTwo(dataMgr.effortExcelData.GetEffortIDList(), listEffortGot);
    }

    public int CalculateCurPenalty()
    {
        int penalty = Mathf.FloorToInt(-500 * Mathf.Pow(2, numReduceMarry));

        //500 /1000 /2000 /4000 /8000 /16000 /32000

        if(penalty < -20000)
        {
            return -20000;
        }
        else
        {
            return penalty;
        }

        //return numReduceMarry * -500 - 500;
    }
    #endregion

    public override void Init()
    {
        StartCoroutine(LoginRoutine());

        dataMgr = DataMgr.Instance;
        dataMgr.Init();
        mapMgr.Init();
        uiMgr.Init();
        soundMgr.Init();
        isInit = true;
        Debug.Log("GameMgrEndInit");

        StartGame();
    }

    public void StartGame()
    {
        InitGameData();
        mapMgr.StartGame();
        uiMgr.StartGame();
    }

    //For UI or event
    public void Update()
    {
        if (!isInit)
        {
            return;
        }

        if (isPageOn)
        {
            Time.timeScale = 0;
        }
        else
        {
            Time.timeScale = globalTimeScale;
        }
    }

    //For Drag and Data
    public void FixedUpdate()
    {
        if (!isInit)
        {
            return;
        }
        mapMgr.FixedTimeGo();
    }

    #region LootLocker
    IEnumerator LoginRoutine()
    {
        bool done = false;
        LootLockerSDKManager.StartGuestSession((response) =>
        {
            if (response.success)
            {
                Debug.Log("Player was logged in");
                PlayerPrefs.SetString("PlayerID", response.player_id.ToString());
                done = true;
            }
            else
            {
                Debug.Log("Could not start session");
                done = true;
            }
        });
        yield return new WaitWhile(() => done == false);
    }
    #endregion
}
