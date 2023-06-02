using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class EfLevelUpItem : MonoBehaviour
{
    public Transform tfEffect;
    public Image imgArrow;
    public Image imgIcon;
    public Image imgRibbon;
    public List<Sprite> listSpArrow = new List<Sprite>();
    public List<Sprite> listSpIcon = new List<Sprite>();
    public List<Color> listTxColor = new List<Color>();
    public List<Color> listOutlineColor = new List<Color>();


    public CanvasGroup canvasGroup;
    public Text txLevelUp;
    public Outline outlineLevelUp;

    public void Init(int newLevel,Vector2 pos, ExpType type)
    {
        switch (type)
        {
            case ExpType.Edu:
                imgArrow.sprite = listSpArrow[0];
                txLevelUp.color = listTxColor[0];
                outlineLevelUp.effectColor = listOutlineColor[0];
                imgIcon.sprite = listSpIcon[0];
                break;
            case ExpType.Career:
                imgArrow.sprite = listSpArrow[1];
                txLevelUp.color = listTxColor[1];
                outlineLevelUp.effectColor = listOutlineColor[1];
                imgIcon.sprite = listSpIcon[1];
                break;
        }

        this.transform.localPosition = pos + new Vector2(0,50F);
        txLevelUp.text = string.Format("Level {0}", newLevel);

        canvasGroup.alpha = 0;

        Sequence seq = DOTween.Sequence();
        seq.Append(tfEffect.DOScale(0, 0));
        seq.Append(imgRibbon.DOFade(0, 0));
        //
        seq.Append(tfEffect.DOScale(1.5f, 0.5f));
        seq.Join(canvasGroup.DOFade(1f, 0.5f));
        seq.Join(tfEffect.DOLocalMoveY(50f, 0.5f));
        //
        seq.Append(tfEffect.DOScale(1f, 0.5f));
        seq.Join(imgRibbon.DOFade(1f, 0.5f));
        seq.Append(tfEffect.DOLocalMoveY(150f, 1.5f));
        seq.Join(canvasGroup.DOFade(0, 1.5f));

        seq.Insert(0f,imgArrow.transform.DOScaleX(1.3f, 0.2f));
        seq.Insert(0.2f, imgArrow.transform.DOScaleX(0.8f, 0.2f));
        seq.Insert(0.4f, imgArrow.transform.DOScaleX(1.1f, 0.2f));
        seq.Insert(0.6f, imgArrow.transform.DOScaleX(0.9f, 0.2f));
        seq.Insert(0.8f,imgArrow.transform.DOScaleX(1f, 0.5f));

        seq.Insert(0f, imgArrow.transform.DOScaleY(0.7f, 0.2f));
        seq.Insert(0.2f, imgArrow.transform.DOScaleY(1.2f, 0.2f));
        seq.Insert(0.4f, imgArrow.transform.DOScaleY(0.85f, 0.2f));
        seq.Insert(0.6f, imgArrow.transform.DOScaleY(1.1f, 0.2f));
        seq.Insert(0.8f, imgArrow.transform.DOScaleY(1f, 0.5f));


        Destroy(this.gameObject, 5f);
    }
}
