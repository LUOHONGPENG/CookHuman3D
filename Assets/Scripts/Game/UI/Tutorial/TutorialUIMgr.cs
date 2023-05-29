using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TutorialUIMgr : MonoBehaviour
{
    public enum TutorialStep
    {
        Human,
        Study,
        Job,
        Marriage,
        Retire,
        End
    }

    public GameObject objPopup;
    public RectTransform rtHole;
    public Text txTip;
    public Button btnNext;
    public Button btnSkip;
    
    private TutorialStep curStep;

    public void Init()
    {
        curStep = TutorialStep.Human;

        btnNext.onClick.RemoveAllListeners();
        btnNext.onClick.AddListener(delegate ()
        {
            NextStep();
        });

        btnSkip.onClick.RemoveAllListeners();
        btnSkip.onClick.AddListener(delegate ()
        {
            SkipStep();
        });
    }

    public void NextStep()
    {
        curStep++;
        InvokeStop();
    }

    public void SkipStep()
    {
        curStep = TutorialStep.End;
        InvokeStop();
    }

    public void InvokeStop()
    {

    }
}
