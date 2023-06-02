using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class EffortUIItem : MonoBehaviour
{
    public Button btnEffort;

    public Text txName;
    public Text txDesc;

    public GameObject objPenalty;
    public Text codePenalty;

    public Image imgBG;
    public List<Sprite> listSpBg = new List<Sprite>();
    public Image imgNameBG;
    public List<Color> listColor = new List<Color>();

    private EffortExcelItem effortItem;
    private EffortUIMgr parent;
    public void Init(int ID,EffortUIMgr parent)
    {
        this.parent = parent;
        effortItem = PublicTool.GetEffortItem(ID);

        if (effortItem != null)
        {
            txName.text = effortItem.name;
            txDesc.text = string.Format(effortItem.desc,effortItem.value0,effortItem.value1);

            btnEffort.onClick.RemoveAllListeners();
            btnEffort.onClick.AddListener(delegate ()
            {
                if(ID == 9999)
                {
                    EventCenter.Instance.EventTrigger("ReduceMarry", null);
                    //Cost Score
                    GameMgr.Instance.scorePenalty += GameMgr.Instance.CalculateCurPenalty();
                    GameMgr.Instance.numReduceMarry++;

                    PublicTool.PlaySound(SoundType.Please);

                }
                else
                {
                    GameMgr.Instance.listEffortGot.Add(ID);
                    PublicTool.PlaySound(SoundType.Hurry);
                }
                GameMgr.Instance.ClearEffort();
                //Refresh
                EventCenter.Instance.EventTrigger("ViewAllRefresh", null);
                parent.HidePopup();
            });
        }

        if(ID == 9999)
        {
            objPenalty.SetActive(true);
            imgBG.sprite = listSpBg[1];
            imgNameBG.color = listColor[1];
            imgNameBG.DOFade(0.5f, 0);
            txDesc.color = listColor[1];

            codePenalty.text = string.Format("Score Penalty: "+ GameMgr.Instance.CalculateCurPenalty().ToString());
        }
        else
        {
            objPenalty.SetActive(false);
            imgBG.sprite = listSpBg[0];
            imgNameBG.color = listColor[0];
            imgNameBG.DOFade(0.5f, 0);
            txDesc.color = listColor[0];


        }
    }
}
