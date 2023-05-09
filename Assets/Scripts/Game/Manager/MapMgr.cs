using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapMgr : MonoBehaviour
{
    [Header("Cookware")]
    public CookwareBasic cookStudy1;
    public CookwareBasic cookStudy2;

    public void Init()
    {
        cookStudy1.Init(1001);
    }
}
