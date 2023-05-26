using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RetireMiniUIMgr : MonoBehaviour
{
    public GameObject objBlock;


    private bool isInit = false;
    public void Init()
    {
        objBlock.SetActive(false);

        isInit = true;
    }


}
