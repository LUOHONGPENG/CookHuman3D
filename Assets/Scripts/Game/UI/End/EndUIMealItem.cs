using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EndUIMealItem : MonoBehaviour
{
    public Text txMealScore;
    public Image imgBg;
    public Button btnShow;

    private EndUIMgr parent;

    public void Init(int vScore,EndUIMgr parent)
    {
        this.parent = parent;
        txMealScore.text = vScore.ToString();

        btnShow.onClick.RemoveAllListeners();
        btnShow.onClick.AddListener(delegate ()
        {
            


        });
    }

    public void Show()
    {
        imgBg.gameObject.SetActive(true);
    }

    public void Hide()
    {
        imgBg.gameObject.SetActive(false);
    }
}
