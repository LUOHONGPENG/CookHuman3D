using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class ExpUIItem : MonoBehaviour
{
    public Image imgExpFill;
    public Image imgIcon;
    public Image imgLight;
    public List<Sprite> listSpIcon = new List<Sprite>();
    

    public void Init(ExpType expType)
    {
        switch (expType)
        {
            case ExpType.Edu:
                imgIcon.sprite = listSpIcon[0];
                imgExpFill.sprite = listSpIcon[0];
                break;
            case ExpType.Career:
                imgIcon.sprite = listSpIcon[1];
                imgExpFill.sprite = listSpIcon[1];
                break;
        }
    }

    public void RefreshExp(bool isFull, float rate)
    {
        if (rate <= 0)
        {
            imgIcon.gameObject.SetActive(false);
            imgExpFill.gameObject.SetActive(false);
        }
        else
        {
            imgIcon.gameObject.SetActive(true);
            imgExpFill.gameObject.SetActive(true);
        }


        if (isFull)
        {
            this.transform.localScale = Vector2.one;
            imgIcon.DOFade(1f, 0);
            imgExpFill.DOFade(1f, 0);
            imgLight.gameObject.SetActive(true);
        }
        else
        {
            imgLight.gameObject.SetActive(false);
            this.transform.localScale = new Vector2(0.75f,0.75f);
            imgIcon.DOFade(0.5f, 0);
            imgExpFill.DOFade(0.5f, 0);
        }
        imgExpFill.fillAmount = rate;
    }
}
