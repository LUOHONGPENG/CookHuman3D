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
    public CookwareBasic cookMarry1;
    public CookwareBasic cookMarry2;
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

    }

    #endregion

    #region Human
    public void InitHuman()
    {
        listHumanBasic.Clear();
        listHumanItem.Clear();

        CreateHuman();
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
    #endregion
}
