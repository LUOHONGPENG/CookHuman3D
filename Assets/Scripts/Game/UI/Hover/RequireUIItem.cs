using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RequireUIItem : MonoBehaviour
{
    public Image imgIcon;
    public List<Sprite> listSp = new List<Sprite>();

    public void Init(ExpType expType,Color color)
    {
        imgIcon.color = color;
        switch (expType)
        {
            case ExpType.Edu:
                imgIcon.sprite = listSp[0];
                break;
            case ExpType.Career:
                imgIcon.sprite = listSp[1];
                break;
        }
    }
}
