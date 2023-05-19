using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class EfLevelUpItem : MonoBehaviour
{
    public Transform tfEffect;
    public CanvasGroup canvasGroup;
    public Text txLevelUp;

    public void Init(int newLevel,Vector2 pos)
    {
        this.transform.localPosition = pos + new Vector2(0,50F);
        txLevelUp.text = string.Format("Level {0}", newLevel);

        canvasGroup.alpha = 0;

        Sequence seq = DOTween.Sequence();
        seq.Append(tfEffect.DOScale(0, 0));
        seq.Append(tfEffect.DOScale(1.5f, 0.5f));
        seq.Join(canvasGroup.DOFade(1f, 0.5f));
        seq.Join(tfEffect.DOLocalMoveY(50f, 0.5f));
        seq.Append(tfEffect.DOScale(1f, 0.5f));
        seq.Append(tfEffect.DOLocalMoveY(150f, 1.5f));
        seq.Join(canvasGroup.DOFade(0, 1.5f));

        Destroy(this.gameObject, 5f);
    }
}
