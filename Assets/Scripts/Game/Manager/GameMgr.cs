using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameMgr : MonoSingleton<GameMgr>
{
    public Camera mapCamera;
    public Camera uiCamera;

    public MapMgr mapMgr;
    public UIMgr uiMgr;
    public LightMgr lightMgr;
    public SoundMgr soundMgr;

    public override void Init()
    {
        Debug.Log("GameMgrInit");


    }
}
