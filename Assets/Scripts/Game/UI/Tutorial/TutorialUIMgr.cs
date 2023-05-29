using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TutorialUIMgr : MonoBehaviour
{
    public GameObject objPopup;
    public RectTransform rtHole;
    public Text txTip;
    public Button btnNext;
    public Button btnSkip;
    
    private TutorialExcelItem[] arrayTutorial;
    private int curID = 0;

    public void Init()
    {
        arrayTutorial = GameMgr.Instance.dataMgr.tutorialExcelData.items;

        btnNext.onClick.RemoveAllListeners();
        btnNext.onClick.AddListener(delegate ()
        {
            NextStep();
        });

        btnSkip.onClick.RemoveAllListeners();
        btnSkip.onClick.AddListener(delegate ()
        {
            EndTutorial();
        });
    }

    public void StartTutorial()
    {
        curID = 0;
        ReadTutorial();
        objPopup.SetActive(true);
        GameMgr.Instance.isPageOn = true;
    }

    private void NextStep()
    {
        curID++;
        if (curID < arrayTutorial.Length)
        {
            ReadTutorial();
        }
        else
        {
            EndTutorial();
        }
    }

    private void EndTutorial()
    {
        objPopup.SetActive(false);
        GameMgr.Instance.isPageOn = false;
    }

    private void ReadTutorial()
    {
        if(curID < arrayTutorial.Length)
        {
            TutorialExcelItem thisTutorial = arrayTutorial[curID];
            rtHole.anchoredPosition = new Vector2(thisTutorial.posx, thisTutorial.posy);
            txTip.text = thisTutorial.strTip;
        }
    }
}
