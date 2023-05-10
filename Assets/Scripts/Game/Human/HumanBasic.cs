using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class HumanBasic : MonoBehaviour
{
    [Header("Basic")]
    public Transform tfItem;

    private bool isInit = false;


    #region Init
    public void Init(HumanItem humanItem)
    {
        this.humanItem = humanItem;
    }
    #endregion
}
