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

    public override void Init()
    {
        Debug.Log("GameMgrInit");

        mapMgr.Init();
    }
}
