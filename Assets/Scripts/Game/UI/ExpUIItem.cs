using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ExpUIItem : MonoBehaviour
{
    public Image imgExpFill;
    public Image imgIcon;
    public List<Sprite> listSpIcon = new List<Sprite>();

    public void Init(ExpType expType)
    {
        switch (expType)
        {
            case ExpType.Edu:
                imgIcon.sprite = listSpIcon[0];
                break;
            case ExpType.Career:
                imgIcon.sprite = listSpIcon[1];
                break;
        }
    }

    public void SetFill(float rate)
    {
        imgExpFill.fillAmount = rate;
    }
}
