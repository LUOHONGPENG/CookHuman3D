using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIMgr : MonoBehaviour
{
    //public HoverUIMgr hoverUIMgr;
    //public RetireUIMgr retireUIMgr;
    public InterfaceUIMgr interfaceUIMgr;
    public RetireMiniUIMgr retireMiniUIMgr;
    public EffortUIMgr effortUIMgr;
    public EndUIMgr endUIMgr;
    public TutorialUIMgr tutorialUIMgr;
    public StartUIMgr startUIMgr;


    public void Init()
    {
        //hoverUIMgr.Init();
        //retireUIMgr.Init();
        interfaceUIMgr.Init();
        retireMiniUIMgr.Init();
        effortUIMgr.Init();
        endUIMgr.Init();
        tutorialUIMgr.Init();
        startUIMgr.Init();
    }

    public void StartGame()
    {
        interfaceUIMgr.StartGame();
        retireMiniUIMgr.StartGame();
        tutorialUIMgr.StartTutorial();
    }
}
