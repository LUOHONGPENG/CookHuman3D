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
        GameMgr.Instance.isTutorialPageOn = true;
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
        GameMgr.Instance.isTutorialPageOn = false;
    }

    private void ReadTutorial()
    {
        if(curID < arrayTutorial.Length)
        {
            TutorialExcelItem thisTutorial = arrayTutorial[curID];
            rtHole.anchoredPosition = new Vector2(thisTutorial.posX, thisTutorial.posY);
            rtHole.sizeDelta = new Vector2(thisTutorial.sizeX, thisTutorial.sizeY);
            txTip.text = thisTutorial.strTip;
        }
    }
}
