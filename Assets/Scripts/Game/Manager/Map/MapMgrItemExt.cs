using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class MapMgr
{
    [Header("Cookware")]
    public CookwareBasic cookStudy;
    public CookwareBasic cookWork1;
    public CookwareBasic cookWork2;
    public CookwareBasic cookWork3;
    public CookwareBasic cookMarryF;
    public CookwareBasic cookMarryM;
    public CookwareBasic cookRetire;

    [Header("Human")]
    public Transform tfHuman;
    public GameObject pfHuman;
    public List<HumanBasic> listHumanBasic = new List<HumanBasic>();
    public Dictionary<int, HumanBasic> dicHumanPos = new Dictionary<int, HumanBasic>();
    public List<HumanItem> listHumanItem = new List<HumanItem>();

    #region Cookware

    public void InitCookware()
    {
        cookStudy.Init(1001);
        cookWork1.Init(2001);
        cookWork2.Init(2002);
        cookWork3.Init(2003);
        cookMarryF.Init(3001);
        cookMarryM.Init(3002);
        cookRetire.Init(4001);
    }

    #endregion

    #region Human

    public void StartHuman()
    {
        listHumanBasic.Clear();
        listHumanItem.Clear();
        StartCoroutine(IE_StartHuman());
    }

    public IEnumerator IE_StartHuman()
    {
        CreateHuman();
        yield return new WaitForSeconds(GameGlobal.timeOneYear * 2);
        CreateHuman();
    }

    public void CreateHuman()
    {
        if(listHumanBasic.Count >= GameGlobal.listPosHumanOrigin.Count)
        {
            Debug.Log("HumanIsFull");
            return;
        }

        //Create a human item
        HumanItem humanItem = new HumanItem(listHumanItem.Count);
        listHumanItem.Add(humanItem);

        //Decide PosID
        int posID = 0;
        for(int i = 0; i < GameGlobal.listPosHumanOrigin.Count; i++)
        {
            if (!dicHumanPos.ContainsKey(i))
            {
                posID = i;
                break;
            }
        }

        //Create a human prefab
        GameObject objHuman = GameObject.Instantiate(pfHuman, GameGlobal.listPosHumanOrigin[posID], Quaternion.identity, tfHuman);
        HumanBasic itemHumanBasic = objHuman.GetComponent<HumanBasic>();
        itemHumanBasic.Init(humanItem, posID);
        listHumanBasic.Add(itemHumanBasic);
        dicHumanPos.Add(posID, itemHumanBasic);
    }

    private void CreateBaby(object arg0)
    {
        CreateHuman();
    }
    #endregion

    #region Special-Retirement

    public void TimeGoCheckAllHuman()
    {
        for(int i = listHumanBasic.Count - 1; i >= 0; i--)
        {
            HumanBasic human = listHumanBasic[i];
            //Dead
            if (human.isDead)
            {
                DestroyHuman(human);
                RefreshHumanScore();
                continue;
            }
            //Time Go
            human.TimeGo();
            //Retire
            if (human.Age >= 80)
            {
                if (!human.isRetired)
                {
                    human.humanItem.RecordRetired();
                    human.isRetired = true;
                }
            }
            if (human.isRetired && !human.isDead)
            {
                EventCenter.Instance.EventTrigger("RetirePage", human);
            }
        }
    }

    public void DestroyHuman(HumanBasic human)
    {
        //Destory
        human.UnBindCookware();
        listHumanBasic.Remove(human);
        dicHumanPos.Remove(human.posOriginID);
        Destroy(human.gameObject);

        if (listHumanBasic.Count <= 0)
        {
            EventCenter.Instance.EventTrigger("EndGame", null) ;
        }
    }

    public void RefreshHumanScore()
    {
        int vScore = 0;
        for(int i = 0; i < listHumanItem.Count; i++)
        {
            vScore += listHumanItem[i].vScore;
        }

        EventCenter.Instance.EventTrigger("RefreshScore",vScore);
    }
    #endregion
}
