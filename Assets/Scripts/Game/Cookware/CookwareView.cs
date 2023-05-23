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
    public GameObject objCondition;
    public Transform tfEduNormal;

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
    public GameObject pfCondition;

    private CookwareBasic parent;
    private bool isInit = false;

    public void Init(CookwareBasic parent)
    {
        this.parent = parent;

        InitUI();
        InitCapacity();

        isInit = true;
    }

    public void OnEnable()
    {
        EventCenter.Instance.AddEventListener("ShowCookPage", HoverEnterEvent);
        EventCenter.Instance.AddEventListener("HideCookPage", HoverLeaveEvent);

    }

    public void OnDestroy()
    {
        EventCenter.Instance.RemoveEventListener("ShowCookPage", HoverEnterEvent);
        EventCenter.Instance.RemoveEventListener("HideCookPage", HoverLeaveEvent);
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

        switch (parent.cookType)
        {
            case CookwareType.Study:
            case CookwareType.Job:
            case CookwareType.Retire:
                InitNormalUI();
                break;
            case CookwareType.Marriage:
                InitMarryUI();
                break;
        }
    }

    private void InitNormalUI()
    {
        objMarriage.SetActive(false);
        tfNormal.gameObject.SetActive(true);
        //Age
        txAgeNormal.text = parent.GetAgeString();

        //Capa
        listCapacityUI.Clear();
        PublicTool.ClearChildItem(tfCapacity);
        if(parent.cookType == CookwareType.Study || parent.cookType == CookwareType.Job)
        {
            for (int i = 0; i < parent.cookCapacity; i++)
            {
                GameObject objCapaUI = GameObject.Instantiate(pfCapacity, tfCapacity);
                CapacityUIItem itemCapaUI = objCapaUI.GetComponent<CapacityUIItem>();
                itemCapaUI.Init();
                listCapacityUI.Add(itemCapaUI);
            }
        }

        //Condition
        PublicTool.ClearChildItem(tfEduNormal);
        for (int i = 0; i < parent.eduMin; i++)
        {
            GameObject objExp = GameObject.Instantiate(pfCondition, tfEduNormal);
            RequireUIItem requireExp = objExp.GetComponent<RequireUIItem>();
            requireExp.Init(ExpType.Edu,Color.black);
        }
        objCondition.SetActive(false);
    }

    private void InitMarryUI()
    {
        objMarriage.SetActive(true);
        tfNormal.gameObject.SetActive(false);

        PublicTool.ClearChildItem(tfEduMarry);
        PublicTool.ClearChildItem(tfCareerMarry);
    }

    #region Refresh

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
                GameObject objExp = GameObject.Instantiate(pfCondition, tfEduMarry);
                RequireUIItem requireExp = objExp.GetComponent<RequireUIItem>();
                requireExp.Init(ExpType.Edu, Color.black);
            }
            PublicTool.ClearChildItem(tfCareerMarry);
            for (int i = 0; i < parent.CareerMin; i++)
            {
                GameObject objExp = GameObject.Instantiate(pfCondition, tfCareerMarry);
                RequireUIItem requireExp = objExp.GetComponent<RequireUIItem>();
                requireExp.Init(ExpType.Career, Color.black);
            }
        }
    }
    #endregion

    #region Hover

    private void HoverEnterEvent(object arg0)
    {
        int tarID = ((CookwareBasic)arg0).cookID;
        if (tarID != parent.cookID)
        {
            objCondition.SetActive(false);
            return;
        }

        if(parent.eduMin > 0)
        {
            objCondition.SetActive(true);
        }
    }

    private void HoverLeaveEvent(object arg0)
    {
        if ((int)arg0 != parent.cookID)
        {
            return;
        }

        objCondition.SetActive(false);
    }


    #endregion

}
