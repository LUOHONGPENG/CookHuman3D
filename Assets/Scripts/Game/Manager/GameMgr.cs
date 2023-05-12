using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameMgr : MonoSingleton<GameMgr>
{
    [Header("Camera")]
    public Camera mapCamera;
    public Camera uiCamera;
    [Header("Manager")]
    public MapMgr mapMgr;
    public UIMgr uiMgr;
    public LightMgr lightMgr;
    public SoundMgr soundMgr;
    public DataMgr dataMgr;

    private bool isInit = false;

    public override void Init()
    {
        dataMgr = DataMgr.Instance;
        dataMgr.Init();
        mapMgr.Init();
        isInit = true;
        Debug.Log("GameMgrEndInit");
    }

    //For UI or event
    public void Update()
    {
        if (!isInit)
        {
            return;
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
