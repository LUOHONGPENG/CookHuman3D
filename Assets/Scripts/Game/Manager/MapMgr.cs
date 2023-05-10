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
    public List<HumanBasic> listHumanObject = new List<HumanBasic>();
    public List<HumanItem> listHumanItem = new List<HumanItem>();

    public void InitHuman()
    {
        listHumanObject.Clear();
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
        HumanBasic itemHumanObject = objHuman.GetComponent<HumanBasic>();
        itemHumanObject.Init(humanItem);
        listHumanObject.Add(itemHumanObject);
    }
}
