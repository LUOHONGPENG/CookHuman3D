using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CookwareView : MonoBehaviour
{
    [Header("Human")]
    public Transform tfHumanGroup;
    public List<Transform> listTfHuman = new List<Transform>();

    [Header("UI")]
    public Canvas canvasUI;
    public Transform tfRootUI;

    [Header("CapacityUI")]
    public Transform tfCapacity;
    public GameObject pfCapacity;
    private List<CapacityUIItem> listCapacityUI = new List<CapacityUIItem>();

    private CookwareBasic parent;
    private bool isInit = false;

    public void Init(CookwareBasic parent)
    {
        this.parent = parent;

        InitUI();
        InitCapacity();

        isInit = true;
    }

    private void InitCapacity()
    {
        //Initialize capacity
        listTfHuman.Clear();
        for (int i = 0; i < parent.cookCapacity; i++)
        {
            if (i < GameGlobal.listPosHumanCookware.Count)
            {
                GameObject objNew = new GameObject("tf");
                objNew.transform.parent = tfHumanGroup;
                objNew.transform.localPosition = GameGlobal.listPosHumanCookware[i];
                listTfHuman.Add(objNew.transform);
            }
        }
    }


    private void InitUI()
    {
        this.canvasUI.worldCamera = GameMgr.Instance.uiCamera;

        tfRootUI.localPosition = PublicTool.CalculateUICanvasPos(parent.tfModel.position, GameMgr.Instance.mapCamera);//+ new Vector3(0, 100f, 0)

        //tfCapacity
        listCapacityUI.Clear();
        PublicTool.ClearChildItem(tfCapacity);

        switch (parent.cookType)
        {
            case CookwareType.Study:
            case CookwareType.Job:
                for (int i = 0; i < parent.cookCapacity; i++)
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
        for (int i = 0; i < listCapacityUI.Count; i++)
        {
            if (parent.listCurHuman.Count > i)
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
