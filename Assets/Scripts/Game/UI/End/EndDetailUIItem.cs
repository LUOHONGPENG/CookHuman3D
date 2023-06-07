using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EndDetailUIItem : MonoBehaviour
{
    public Image imgBg;
    public List<Sprite> listSpBg = new List<Sprite>();
    public Text txComment;
    public Text codeScore;
    public List<Color> listColor = new List<Color>();

    public void Init(ScoreInfo scoreInfo)
    {
        txComment.text = scoreInfo.desc;
        codeScore.text = scoreInfo.score.ToString();

        if (scoreInfo.score >= 0)
        {
            imgBg.sprite = listSpBg[1];
            txComment.color = listColor[1];
            codeScore.color = listColor[1];
        }
        else
        {
            imgBg.sprite = listSpBg[0];
            txComment.color = listColor[0];
            codeScore.color = listColor[0];
        }
    }
}
