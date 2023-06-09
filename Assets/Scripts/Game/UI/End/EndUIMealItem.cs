using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class EndUIMealItem : MonoBehaviour
{
    public Text txMealScore;
    public Image imgBg;
    public Button btnShow;

    private EndUIMgr parent;
    [HideInInspector]
    public int humanID;

    public void Init(int ID,int vScore,EndUIMgr parent)
    {
        this.parent = parent;
        this.humanID = ID;
        txMealScore.text = vScore.ToString();

        btnShow.onClick.RemoveAllListeners();
        btnShow.onClick.AddListener(delegate ()
        {
            parent.ShowHumanComment(humanID);
        });
    }

    public void Show()
    {
        //imgBg.gameObject.SetActive(true);
        imgBg.color = new Color(0, 0, 0, 0.1f);
    }

    public void Hide()
    {
        //imgBg.gameObject.SetActive(false);
        imgBg.color = new Color(0, 0, 0, 0);
    }
}
