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

    public List<GameObject> listTutObj = new List<GameObject>();
    
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
            txTip.transform.localPosition = new Vector2(thisTutorial.posXtext, thisTutorial.posYtext);
            txTip.text = thisTutorial.strTip;
            txTip.fontSize = thisTutorial.fontSize;

            ReadObj(thisTutorial.picGroup);
        }
    }

    private void ReadObj(int index)
    {
        for (int i = 0; i < listTutObj.Count; i++)
        {
            listTutObj[i].SetActive(false);
        }
        if (index >= 0 && index < listTutObj.Count)
        {
            listTutObj[index].SetActive(true);
        }
    }
}
