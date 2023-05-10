using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class MapMgr : MonoBehaviour
{
    [Header("Cookware")]
    public CookwareBasic cookStudy1;
    public CookwareBasic cookStudy2;

    public void Init()
    {
        cookStudy1.Init(1001);

        InitHuman();
    }
}

public partial class MapMgr
{
    public Transform tfHuman;
    public GameObject pfHuman;
    public List<HumanBasic> listHuman = new List<HumanBasic>();
    public List<HumanItem> listHumanItem = new List<HumanItem>();

    public void InitHuman()
    {
        listHuman.Clear();
        listHumanItem.Clear();
    }

    public void CreateHuman()
    {
        //Create a human item
        HumanItem humanItem = new HumanItem(listHumanItem.Count);
        listHumanItem.Add(humanItem);

        //Create a human prefab
        
    }
}
