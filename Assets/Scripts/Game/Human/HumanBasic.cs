using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public partial class HumanBasic : MonoBehaviour
{
    [Header("Basic")]
    public Vector3 posOrigin;
    
    private bool isInit = false;


    #region Init
    public void Init(HumanItem humanItem,Vector3 pos)
    {
        this.humanItem = humanItem;
        this.posOrigin = pos;

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
                curCookware.UnbindHuman();
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
            curCookware.UnbindHuman();
        }
        curCookware = null;
        BackOrigin();
    }

    public void BackCookware()
    {
        if (curCookware != null)
        {
            //Back to cookware
            this.transform.DOMove(curCookware.tfHuman.position,0.2f);
        }
        else
        {
            BackOrigin();
        }
    }

    public void BackOrigin()
    {
        //Back to birthplace
        this.transform.DOMove(posOrigin, 0.2f);
    }

    #endregion

}
