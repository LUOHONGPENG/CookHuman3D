using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class EfWarningItem : MonoBehaviour
{
    public Text txWarning;
    public Transform tfEffect;

    public CanvasGroup canvasGroup;

    public void Init(string strWarning,Vector2 pos)
    {
        this.transform.localPosition = pos + new Vector2(0, 20F);
        txWarning.text = strWarning;
        canvasGroup.alpha = 0;

        Sequence seq = DOTween.Sequence();
        seq.Append(tfEffect.DOScale(0, 0));
        //
        seq.Append(tfEffect.DOScale(1.5f, 0.5f));
        seq.Join(canvasGroup.DOFade(1f, 0.5f));
        seq.Join(tfEffect.DOLocalMoveY(20f, 0.5f));
        //0.5f
        seq.Append(tfEffect.DOScale(1f, 0.5f));
        //1F
        seq.Append(tfEffect.DOLocalMoveY(110f, 1.5f));
        seq.Insert(1.25F, canvasGroup.DOFade(0, 1.5f));

        Destroy(this.gameObject, 5f);


    }
}
