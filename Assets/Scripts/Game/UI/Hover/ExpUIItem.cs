using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class ExpUIItem : MonoBehaviour
{
    public Image imgExpFill;
    public Image imgIcon;
    public List<Sprite> listSpIcon = new List<Sprite>();
    public List<Color> listColor = new List<Color>();

    public void Init(ExpType expType)
    {
        switch (expType)
        {
            case ExpType.Edu:
                imgIcon.sprite = listSpIcon[0];
                imgIcon.color = listColor[0];
                imgExpFill.color = listColor[0];
                break;
            case ExpType.Career:
                imgIcon.sprite = listSpIcon[1];
                imgIcon.color = listColor[1];
                imgExpFill.color = listColor[1];
                break;
        }
    }

    public void RefreshExp(bool isFull, float rate)
    {
        if (isFull)
        {
            imgIcon.DOFade(1f, 0);
            imgExpFill.DOFade(1f, 0);
        }
        else
        {
            imgIcon.DOFade(0.5f, 0);
            imgExpFill.DOFade(0.5f, 0);
        }
        imgExpFill.fillAmount = rate;
    }
}
