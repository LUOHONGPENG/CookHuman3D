using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class CookwareView : MonoBehaviour
{
    [Header("Human")]
    public Transform tfHumanGroup;
    public List<Transform> listTfHuman = new List<Transform>();

    [Header("UI")]
    public Canvas canvasUI;
    public Transform tfRootUI;

    [Header("NormalUI")]
    public GameObject objNormal;
    public RectTransform rtBgNormal;
    public Text txAgeNormal;
    public RectTransform rtBgNormalCover;
    public Image imgBgNormalCover;
    [Header("CapacityUI")]
    public Transform tfCapacity;
    public GameObject pfCapacity;
    private List<CapacityUIItem> listCapacityUI = new List<CapacityUIItem>();

    [Header("ConditionUI")]
    public CanvasGroup canvasGroupCondition;
    public Transform tfEduNormal;

    [Header("DescUI")]
    public CanvasGroup canvasGroupDesc;
    public Text txDesc;
    public Transform tfGrow;
    public Image imgSlice;
    public GameObject pfArrow;



    [Header("MarriageUI")]
    public GameObject objMarriage;
    public Text txAgeMarry;
    public Image imgSexMarry;
    public List<Sprite> listSpSex = new List<Sprite>();
    public RectTransform rtBgMarry;
    public Image imgBgMarry;
    public List<Sprite> listBgSex = new List<Sprite>();
    public Transform tfEduMarry;
    public Transform tfCareerMarry;
    public GameObject pfCondition;
    public Image imgBgMarryCover;

    private CookwareBasic parent;
    private bool isInit = false;
    private bool isStartGame = false;

    public CookwareBasic GetBasic()
    {
        return parent;
    }

    public void Init(CookwareBasic parent)
    {
        this.parent = parent;
        CookwareExcelItem item = parent.GetItem();
        tfHumanGroup.transform.localPosition = new Vector3(item.posxHuman, 0, item.poszHuman);
        isInit = true;
    }

    public void StartGame()
    {
        InitUI();
        RefreshCapacityPhysics();
    }

    public void OnEnable()
    {
        EventCenter.Instance.AddEventListener("ShowCookPage", HoverEnterEvent);
        EventCenter.Instance.AddEventListener("HideCookPage", HoverLeaveEvent);
        EventCenter.Instance.AddEventListener("ViewAllRefresh", ViewAllRefresh);
        EventCenter.Instance.AddEventListener("ConditionShine", ConditionShine);
    }

    public void OnDestroy()
    {
        EventCenter.Instance.RemoveEventListener("ShowCookPage", HoverEnterEvent);
        EventCenter.Instance.RemoveEventListener("HideCookPage", HoverLeaveEvent);
        EventCenter.Instance.RemoveEventListener("ViewAllRefresh", ViewAllRefresh);
        EventCenter.Instance.RemoveEventListener("ConditionShine", ConditionShine);
    }

    private void ViewAllRefresh(object arg0)
    {
        RefreshCapacityPhysics();
        RefreshNormalInfoUI();
        RefreshMarryUI();
    }


    public void RefreshCapacityPhysics()
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

        tfRootUI.localPosition = PublicTool.CalculateUICanvasPos(parent.tfCenter.position, GameMgr.Instance.mapCamera);//+ new Vector3(0, 100f, 0)

        //Set the position of the Normal UI
        objNormal.transform.localPosition = new Vector2(parent.GetItem().posxInfo, parent.GetItem().posyInfo);
        rtBgMarry.transform.localPosition = new Vector2(parent.GetItem().posxInfo, parent.GetItem().posyInfo);

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
        objNormal.gameObject.SetActive(true);


        RefreshNormalInfoUI();
        InitNormalCookInfo();
    }

    private void RefreshNormalInfoUI()
    {
        //Init Age Info
        txAgeNormal.text = parent.GetAgeString();




        RefreshDescUI();
        RefreshCapacityUI();
        RefreshConditionUI();
    }

    private void RefreshDescUI()
    {
        txDesc.text = parent.GetDesc();
        //Init Grow Info
        PublicTool.ClearChildItem(tfGrow);
        int numArrow = 0;
        switch (parent.cookID)
        {
            case 1001:
                if (PublicTool.CheckWhetherEffortGot(1001))
                {
                    numArrow = 3;
                }
                else
                {
                    numArrow = 2;
                }
                break;
            case 2001:
                numArrow = 1;
                break;
            case 2002:
                numArrow = 2;
                break;
            case 2003:
                numArrow = 3;
                break;
        }
        if (PublicTool.CheckWhetherEffortGot(1008) && parent.cookType == CookwareType.Job)
        {
            numArrow++;
        }
        if (numArrow > 0)
        {
            tfGrow.gameObject.SetActive(true);
            txDesc.gameObject.SetActive(false);
            imgSlice.gameObject.SetActive(false);
            GameObject objRe = GameObject.Instantiate(pfCondition, tfGrow);
            RequireUIItem itemRe = objRe.GetComponent<RequireUIItem>();
            switch (parent.cookType)
            {
                case CookwareType.Study:
                    itemRe.Init(ExpType.Edu);
                    break;
                case CookwareType.Job:
                    itemRe.Init(ExpType.Career);
                    break;
            }
            for (int i = 0; i < numArrow; i++)
            {
                GameObject.Instantiate(pfArrow, tfGrow);
            }

            if (PublicTool.CheckWhetherEffortGot(1004) && parent.cookType == CookwareType.Study)
            {
                GameObject objWS = GameObject.Instantiate(pfCondition, tfGrow);
                RequireUIItem itemWS = objWS.GetComponent<RequireUIItem>();
                itemWS.Init(ExpType.Career);
                GameObject.Instantiate(pfArrow, tfGrow);
            }

            if (PublicTool.CheckWhetherEffortGot(1009) && parent.cookType == CookwareType.Job)
            {
                GameObject objNL = GameObject.Instantiate(pfCondition, tfGrow);
                RequireUIItem itemNL = objNL.GetComponent<RequireUIItem>();
                itemNL.Init(ExpType.Edu);
                GameObject.Instantiate(pfArrow, tfGrow);
            }
        }
        else
        {
            tfGrow.gameObject.SetActive(false);
            txDesc.gameObject.SetActive(false);
            imgSlice.gameObject.SetActive(true);
        }
    }


    private void RefreshCapacityUI()
    {
        //Init Capacity Info
        listCapacityUI.Clear();
        PublicTool.ClearChildItem(tfCapacity);
        if (parent.cookType == CookwareType.Study || parent.cookType == CookwareType.Job)
        {
            for (int i = 0; i < parent.cookCapacity; i++)
            {
                GameObject objCapaUI = GameObject.Instantiate(pfCapacity, tfCapacity);
                CapacityUIItem itemCapaUI = objCapaUI.GetComponent<CapacityUIItem>();
                itemCapaUI.Init();
                listCapacityUI.Add(itemCapaUI);
            }
        }
    }

    private void RefreshConditionUI()
    {
        //Init Condition Info
        PublicTool.ClearChildItem(tfEduNormal);
        for (int i = 0; i < parent.eduMin; i++)
        {
            GameObject objExp = GameObject.Instantiate(pfCondition, tfEduNormal);
            RequireUIItem requireExp = objExp.GetComponent<RequireUIItem>();
            requireExp.Init(ExpType.Edu);
        }
    }


    private void InitMarryUI()
    {
        objMarriage.SetActive(true);
        objNormal.gameObject.SetActive(false);

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
        RefreshCapacityInnerUI();
    }

    private void RefreshCapacityInnerUI()
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
                imgSexMarry.sprite = listSpSex[0];
                imgBgMarry.sprite = listBgSex[0];
            }
            else if(parent.requiredSex == Sex.Male)
            {
                imgSexMarry.sprite = listSpSex[1];
                imgBgMarry.sprite = listBgSex[1];
            }
            //
            PublicTool.ClearChildItem(tfEduMarry);
            for(int i = 0; i < parent.eduMin; i++)
            {
                GameObject objExp = GameObject.Instantiate(pfCondition, tfEduMarry);
                RequireUIItem requireExp = objExp.GetComponent<RequireUIItem>();
                requireExp.Init(ExpType.Edu);
            }
            PublicTool.ClearChildItem(tfCareerMarry);
            for (int i = 0; i < parent.CareerMin; i++)
            {
                GameObject objExp = GameObject.Instantiate(pfCondition, tfCareerMarry);
                RequireUIItem requireExp = objExp.GetComponent<RequireUIItem>();
                requireExp.Init(ExpType.Career);
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
            HideNormalCookInfo();
            return;
        }

        ShowNormalCookInfo();
    }

    private void HoverLeaveEvent(object arg0)
    {
        if ((int)arg0 != parent.cookID)
        {
            return;
        }

        HideNormalCookInfo();
    }

    private void ShowNormalCookInfo()
    {
        canvasUI.sortingOrder = 1;
        canvasGroupDesc.DOFade(1f, 0.25f);
        if (parent.eduMin > 0)
        {
            canvasGroupCondition.DOFade(1f, 0.25f);
            rtBgNormal.DOSizeDelta(new Vector2(220f, 174f), 0.25f);
            rtBgNormalCover.DOSizeDelta(new Vector2(220f, 174f), 0.25f);

        }
        else
        {
            rtBgNormal.DOSizeDelta(new Vector2(220f, 125f), 0.25f);
            rtBgNormalCover.DOSizeDelta(new Vector2(220f, 125f), 0.25f);

        }
    }

    private void InitNormalCookInfo()
    {
        canvasUI.sortingOrder = 0;
        canvasGroupDesc.alpha = 0;
        canvasGroupCondition.alpha = 1;
        if (parent.eduMin > 0)
        {
            rtBgNormal.sizeDelta = new Vector2(220f, 125f);
            rtBgNormalCover.sizeDelta = new Vector2(220f, 125f);
            canvasGroupCondition.gameObject.SetActive(true);
        }
        else
        {
            rtBgNormal.sizeDelta = new Vector2(220f, 76f);
            rtBgNormalCover.sizeDelta = new Vector2(220f, 76f);
            canvasGroupCondition.gameObject.SetActive(false);
        }
    }

    private void HideNormalCookInfo()
    {
        canvasUI.sortingOrder = 0;
        canvasGroupDesc.DOFade(0, 0.25f);
        //canvasGroupCondition.DOFade(0, 0.25f);
        if(parent.eduMin > 0)
        {
            rtBgNormal.DOSizeDelta(new Vector2(220f, 125f), 0.25f);
            rtBgNormalCover.DOSizeDelta(new Vector2(220f, 125f), 0.25f);
        }
        else
        {
            rtBgNormal.DOSizeDelta(new Vector2(220f, 76f), 0.25f);
            rtBgNormalCover.DOSizeDelta(new Vector2(220f, 76f), 0.25f);
        }
    }

    private void ConditionShine(object arg0)
    {
        List<int> list = (List<int>)arg0;

        if (list.Contains(parent.cookID))
        {
            Sequence seq = DOTween.Sequence();
            seq.Append(imgBgNormalCover.DOFade(1F, 0.5f));
            seq.Join(imgBgMarryCover.DOFade(1F, 0.5f));
            seq.Append(imgBgNormalCover.DOFade(0, 0.5f));
            seq.Join(imgBgMarryCover.DOFade(0, 0.5f));
            seq.Append(imgBgNormalCover.DOFade(1F, 0.5f));
            seq.Join(imgBgMarryCover.DOFade(1F, 0.5f));
            seq.Append(imgBgNormalCover.DOFade(0, 0.5f));
            seq.Join(imgBgMarryCover.DOFade(0, 0.5f));
        }
    }
    #endregion

}
