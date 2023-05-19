using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct EffectUIInfo
{
    public string type;
    public Vector2 pos;
    public int arg0;

    public EffectUIInfo(string type,Vector2 pos,int arg0)
    {
        this.type = type;
        this.pos = pos;
        this.arg0 = arg0;
    }
}

public class EffectUIMgr : MonoBehaviour
{
    public Transform tfFloat;
    public GameObject pfLevelUp;

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
            case "LevelUp":
                GameObject objLevel = GameObject.Instantiate(pfLevelUp, tfFloat);
                EfLevelUpItem itemLevel = objLevel.GetComponent<EfLevelUpItem>();
                itemLevel.Init(info.arg0, info.pos);
                break;
        }
    }
}
