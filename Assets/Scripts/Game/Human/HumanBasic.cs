using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public partial class HumanBasic : MonoBehaviour
{
    [Header("Basic")]
    //Remember the original Pos
    public Transform tfHumanHead;
    public int posOriginID = 0;
    public Vector3 posCookware;
    public HumanView itemView;
    
    private bool isInit = false;

    #region Init
    public void Init(HumanItem humanItem,int posID)
    {
        this.humanItem = humanItem;
        this.posOriginID = posID;
        itemView.Init(this);

        isRetired = false;
        isDead = false;

        isInit = true;
    }
    #endregion

    #region BindCookware
    //The data of currentCookware
    public CookwareBasic curCookware;

    public void BindCookware(CookwareBasic tarCookware)
    {
        //Try to bind a cookware
        if (tarCookware.CheckHuman(this))
        {
            //Check whether this human bind a cookware
            if (curCookware != null)
            {
                curCookware.UnbindHuman(this);
            }
            //Bind each other
            tarCookware.BindHuman(this);

            curCookware = tarCookware;
            //Move
            BackCookware();
        }
        else
        {
            if (curCookware != null)
            {
                BackCookware();
            }
            else
            {
                BackOrigin();
            }
        }
    }

    public void UnBindCookware()
    {
        if (curCookware != null)
        {
            curCookware.UnbindHuman(this);
        }
        curCookware = null;
        BackOrigin();
    }

    public void BackCookware()
    {
        if (curCookware != null)
        {
            this.transform.DOMove(posCookware, 0.2f);
        }
        else
        {
            BackOrigin();
        }
    }

    public void BackOrigin()
    {
        //Back to birthplace
        this.transform.DOMove(GameGlobal.listPosHumanOrigin[posOriginID], 0.2f);
    }

    #endregion

}
