using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct EffectUIInfo
{
    public string type;
    public Vector2 pos;
    public int arg0;
    public string arg1;
    public EffectUIInfo(string type,Vector2 pos,int arg0,string arg1="")
    {
        this.type = type;
        this.pos = pos;
        this.arg0 = arg0;
        this.arg1 = arg1;
    }
}


public class EffectUIMgr : MonoBehaviour
{
    public Transform tfFloat;
    public GameObject pfLevelUp;
    public GameObject pfWarning;

    public void OnEnable()
    {
        EventCenter.Instance.AddEventListener("EffectUI",EffectUIInit);
    }

    public void OnDestroy()
    {
        EventCenter.Instance.RemoveEventListener("EffectUI", EffectUIInit);
    }

    private void EffectUIInit(object arg0)
    {
        EffectUIInfo info = (EffectUIInfo)arg0;
        switch (info.type)
        {
            case "LevelUpStudy":
                GameObject objLevelS = GameObject.Instantiate(pfLevelUp, tfFloat);
                EfLevelUpItem itemLevelS = objLevelS.GetComponent<EfLevelUpItem>();
                itemLevelS.Init(info.arg0, info.pos,ExpType.Edu);
                break;
            case "LevelUpJob":
                GameObject objLevelJ = GameObject.Instantiate(pfLevelUp, tfFloat);
                EfLevelUpItem itemLevelJ = objLevelJ.GetComponent<EfLevelUpItem>();
                itemLevelJ.Init(info.arg0, info.pos,ExpType.Career);
                break;
            case "Warning":
                GameObject objLevelW = GameObject.Instantiate(pfWarning, tfFloat);
                EfWarningItem itemLevelW = objLevelW.GetComponent<EfWarningItem>();
                itemLevelW.Init(info.arg1, info.pos);
                break;
        }
    }
}
