using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class RetireMiniUIMgr : MonoBehaviour
{
    public GameObject objBlock;
    public RectTransform rtInfo;
    public Button btnClose;

    [Header("Score")]
    public Text codeScore;

    [Header("Comment")]
    public Transform tfComment;
    public GameObject pfComment;

    //Inner Data
    private float timerClose = 5f;
    private bool isInit = false;

    public void Init()
    {
        objBlock.SetActive(false);

        isInit = true;
    }

    public void ShowPopup()
    {
        timerClose = 5f;
    }


    private void Update()
    {
        if (timerClose > 0)
        {
            timerClose -= Time.deltaTime;
        }
    }
}
