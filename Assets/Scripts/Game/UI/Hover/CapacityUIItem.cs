using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CapacityUIItem : MonoBehaviour
{
    public Image imgFill;

    public void Init()
    {

    }

    public void SetFull()
    {
        imgFill.gameObject.SetActive(true);
    }
    public void SetEmpty()
    {
        imgFill.gameObject.SetActive(false);
    }
}
