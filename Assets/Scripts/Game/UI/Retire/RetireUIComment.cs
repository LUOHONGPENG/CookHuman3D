using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RetireUIComment : MonoBehaviour
{
    public Text txRetireComment;
    public Text txScore;

    public List<Color> listColor = new List<Color>();

    public void Init(string strComment,int vScore)
    {
        txRetireComment.text = strComment;
        if (vScore >= 0)
        {
            txScore.text = vScore.ToString();
            txScore.color = listColor[1];
        }
        else
        {
            txScore.text = string.Format("{0}", vScore.ToString());
            txScore.color = listColor[0];
        }
    }
}
