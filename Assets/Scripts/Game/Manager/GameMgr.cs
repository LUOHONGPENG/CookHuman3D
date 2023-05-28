using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

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
    public bool isPageOn = false;

    public int numMarry = 0;

    public override void Init()
    {
        dataMgr = DataMgr.Instance;
        dataMgr.Init();
        mapMgr.Init();
        uiMgr.Init();
        soundMgr.Init();
        isPageOn = false;
        isInit = true;
        numMarry = 0;
        Debug.Log("GameMgrEndInit");
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
            Time.timeScale = 1f;
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
}
