using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CookwareView : MonoBehaviour
{
    [Header("Human")]
    public Transform tfHumanGroup;
    public List<Transform> listTfHuman = new List<Transform>();

    [Header("UI")]
    public Canvas canvasUI;
    public Transform tfRootUI;

    [Header("NormalUI")]
    public Transform tfNormal;
    public Text txAgeNormal;


    [Header("CapacityUI")]
    public Transform tfCapacity;
    public GameObject pfCapacity;
    private List<CapacityUIItem> listCapacityUI = new List<CapacityUIItem>();

    [Header("MarriageUI")]
    public GameObject objMarriage;
    public Text txAgeMarry;
    public Image imgSexMarry;
    public List<Sprite> listSpMarry = new List<Sprite>();
    public Transform tfEduMarry;
    public Transform tfCareerMarry;
    public GameObject pfConditionMarry;

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
        
        //tfNormal
        tfNormal.localPosition = new Vector2(parent.GetItem().posxInfo, parent.GetItem().posyInfo);

        //tfCapacity
        listCapacityUI.Clear();
        PublicTool.ClearChildItem(tfCapacity);

        switch (parent.cookType)
        {
            case CookwareType.Study:
            case CookwareType.Job:
                tfNormal.gameObject.SetActive(true);
                txAgeNormal.text = parent.GetAgeString();
                objMarriage.SetActive(false);
                for (int i = 0; i < parent.cookCapacity; i++)
                {
                    GameObject objCapaUI = GameObject.Instantiate(pfCapacity, tfCapacity);
                    CapacityUIItem itemCapaUI = objCapaUI.GetComponent<CapacityUIItem>();
                    itemCapaUI.Init();
                    listCapacityUI.Add(itemCapaUI);
                }
                break;
            case CookwareType.Retire:
                tfNormal.gameObject.SetActive(true);
                txAgeNormal.text = parent.GetAgeString();
                objMarriage.SetActive(false);
                break;
            case CookwareType.Marriage:
                tfNormal.gameObject.SetActive(false);

                objMarriage.SetActive(true);
                PublicTool.ClearChildItem(tfEduMarry);
                PublicTool.ClearChildItem(tfCareerMarry);
                break;
        }


    }

    private void Update()
    {
        if (!isInit)
        {
            return;
        }
        RefreshCapacityUI();

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

    public void RefreshMarryUI()
    {
        if(parent.cookType == CookwareType.Marriage)
        {
            //Age
            txAgeMarry.text = parent.GetAgeString();
            //Sex
            if(parent.requiredSex == Sex.Female)
            {
                imgSexMarry.sprite = listSpMarry[0];
            }
            else if(parent.requiredSex == Sex.Male)
            {
                imgSexMarry.sprite = listSpMarry[1];
            }
            //
            PublicTool.ClearChildItem(tfEduMarry);
            for(int i = 0; i < parent.eduMin; i++)
            {
                GameObject objExp = GameObject.Instantiate(pfConditionMarry, tfEduMarry);
                RequireUIItem requireExp = objExp.GetComponent<RequireUIItem>();
                requireExp.Init(ExpType.Edu);
            }
            PublicTool.ClearChildItem(tfCareerMarry);
            for (int i = 0; i < parent.CareerMin; i++)
            {
                GameObject objExp = GameObject.Instantiate(pfConditionMarry, tfCareerMarry);
                RequireUIItem requireExp = objExp.GetComponent<RequireUIItem>();
                requireExp.Init(ExpType.Career);
            }
        }
    }
}
