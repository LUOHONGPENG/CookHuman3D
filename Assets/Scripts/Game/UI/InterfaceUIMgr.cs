using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InterfaceUIMgr : MonoBehaviour
{
    public Text codeScore;

    public void Init()
    {

    }

    public void StartGame()
    {
        RefreshScore(0);
    }

    public void OnEnable()
    {
        EventCenter.Instance.AddEventListener("RefreshScore", RefreshScore);
    }

    public void OnDestroy()
    {
        EventCenter.Instance.RemoveEventListener("RefreshScore", RefreshScore);
    }

    private void RefreshScore(object arg0)
    {
        int score = (int)arg0;
        codeScore.text = score.ToString();
    }
}
