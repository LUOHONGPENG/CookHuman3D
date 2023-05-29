using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LeaderBoardUIItem : MonoBehaviour
{
    public Text codeNum;
    public Text codeName;
    public Text codeScore;
    public Image imgBg;
    public List<Color> listColorText = new List<Color>();
    public List<Color> listColorBg = new List<Color>();

    public void Init(int num, string name, int time)
    {
        codeNum.text = num.ToString();
        codeName.text = name;
        codeScore.text = time.ToString();

        if (num < 8)
        {
            codeNum.color = listColorText[num - 1];
            codeName.color = listColorText[num - 1];
            codeScore.color = listColorText[num - 1];
            imgBg.color = listColorBg[num - 1];
        }
        else
        {
            codeNum.color = listColorText[7];
            codeName.color = listColorText[7];
            codeScore.color = listColorText[7];
            imgBg.color = listColorBg[7];
        }
    }
}
