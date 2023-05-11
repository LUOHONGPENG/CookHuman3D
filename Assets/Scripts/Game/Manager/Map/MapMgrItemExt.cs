using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class MapMgr
{
    [Header("Cookware")]
    public CookwareBasic cookStudy1;
    public CookwareBasic cookStudy2;

    [Header("Human")]
    public Transform tfHuman;
    public GameObject pfHuman;
    public List<HumanBasic> listHumanBasic = new List<HumanBasic>();
    public List<HumanItem> listHumanItem = new List<HumanItem>();

    #region Cookware

    public void InitCookware()
    {
        cookStudy1.Init(1001);
    }

    #endregion


    #region Human
    public void InitHuman()
    {
        listHumanBasic.Clear();
        listHumanItem.Clear();

        CreateHuman(new Vector3(0, 1.05f, 0.5f));
    }

    public void CreateHuman(Vector3 pos)
    {
        //Create a human item
        HumanItem humanItem = new HumanItem(listHumanItem.Count);
        listHumanItem.Add(humanItem);

        //Create a human prefab
        GameObject objHuman = GameObject.Instantiate(pfHuman, pos, Quaternion.identity, tfHuman);
        HumanBasic itemHumanBasic = objHuman.GetComponent<HumanBasic>();
        itemHumanBasic.Init(humanItem,pos);
        listHumanBasic.Add(itemHumanBasic);
    }

    #endregion
}
