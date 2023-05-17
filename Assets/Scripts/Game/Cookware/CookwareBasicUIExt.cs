using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class CookwareBasic
{
    [Header("UI")]
    public Canvas canvasUI;
    public Transform tfRootUI;
    public Transform tfCook;

    [Header("CapacityUI")]
    public Transform tfCapacity;
    public GameObject pfCapacity;
    private List<CapacityUIItem> listCapacityUI = new List<CapacityUIItem>();

    private void InitUI()
    {
        tfRootUI.localPosition = PublicTool.CalculateUICanvasPos(tfCook.position, GameMgr.Instance.mapCamera);//+ new Vector3(0, 100f, 0)

        //tfCapacity
        listCapacityUI.Clear();
        PublicTool.ClearChildItem(tfCapacity);

        switch (cookType)
        {
            case CookwareType.Study:
            case CookwareType.Job:
                for(int i = 0; i < cookCapacity; i++)
                {
                    GameObject objCapaUI = GameObject.Instantiate(pfCapacity, tfCapacity);
                    CapacityUIItem itemCapaUI = objCapaUI.GetComponent<CapacityUIItem>();
                    itemCapaUI.Init();
                    listCapacityUI.Add(itemCapaUI);
                }
                break;
        }
    }

    private void Update()
    {
        if (isInit)
        {
            RefreshCapacityUI();
        }
    }

    private void RefreshCapacityUI()
    {
        for(int i = 0; i < listCapacityUI.Count; i++)
        {
            if(listCurHuman.Count > i)
            {
                listCapacityUI[i].SetFull();
            }
            else
            {
                listCapacityUI[i].SetEmpty();
            }
        }
    }
}
