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

    public void StartCookware()
    {
        cookStudy.StartGame();
        cookWork1.StartGame();
        cookWork2.StartGame();
        cookWork3.StartGame();
        cookMarryF.StartGame();
        cookMarryM.StartGame();
        cookRetire.StartGame();
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
        CreateHuman(false);
        yield return new WaitForSeconds(PublicTool.GetCurrentTimeYear() * 2);
        CreateHuman(false);
    }

    public void CreateHuman(bool setSex,Sex sex = Sex.Male)
    {
        if(listHumanBasic.Count >= GameGlobal.listPosHumanOrigin.Count)
        {
            Debug.Log("HumanIsFull");
            return;
        }

        //Sex
        Sex targetSex= Sex.Male;
        if (setSex)
        {
            targetSex = sex;
        }
        else
        {
            //Initial Sex data
            int ranSex = Random.Range(0, 2);
            if (ranSex == 0)
            {
                targetSex = Sex.Male;
            }
            else if (ranSex == 1)
            {
                targetSex = Sex.Female;
            }
        }
        HumanItem humanItem = new HumanItem(listHumanItem.Count, targetSex);
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
        bool isTwin = false;
        if (PublicTool.CheckWhetherEffortGot(1003))
        {
            if (listHumanBasic.Count <= 2)
            {
                int ran = Random.Range(0, 100);
                float expectation = 0;
                Debug.Log("MagicBellyCheck " + ran);
                switch (listHumanBasic.Count)
                {
                    case 1:
                        expectation = PublicTool.GetEffortItem(1003).value0;
                        break;
                    case 2:
                        expectation = PublicTool.GetEffortItem(1003).value1;
                        break;
                }
                if(ran < expectation)
                {
                    isTwin = true;
                }
            }
        }

        if (isTwin)
        {
            CreateHuman(true,Sex.Male);
            CreateHuman(true,Sex.Female);

            //MagicBelly!
            PublicTool.PlaySound(SoundType.MagicBelly);

        }
        else
        {
            CreateHuman(false);
            PublicTool.PlaySound(SoundType.Marriage);
        }
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
                EventCenter.Instance.EventTrigger("RefreshScore", null);
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
    #endregion
}
