using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EndUIMgr : MonoBehaviour
{
    public GameObject objPopup;

    public Text codeTotalScore;
    public Button btnRestart;
    public Transform tfMeal;
    public GameObject pfMeal;

    public void Init()
    {
        btnRestart.onClick.RemoveAllListeners();
        btnRestart.onClick.AddListener(delegate () {
            HidePopup();
            //SceneManager.LoadScene("Main");
        });
    }

    private void OnEnable()
    {
        EventCenter.Instance.AddEventListener("EndGame", ShowEndPopup);
    }

    private void OnDestroy()
    {
        EventCenter.Instance.RemoveEventListener("EndGame", ShowEndPopup);
    }

    public void ShowEndPopup(object arg0)
    {
        List<HumanItem> listHuman = GameMgr.Instance.mapMgr.listHumanItem;
        int totalScore = 0;
        for (int i = 0; i < listHuman.Count; i++)
        {
            /*            GameObject objMeal = GameObject.Instantiate(pfMeal, tfMeal);
                        EndUIMeal itemMeal = objMeal.GetComponent<EndUIMeal>();
                        itemMeal.Init(listScore[i]);*/

            totalScore += listHuman[i].vScore;
        }

        codeTotalScore.text = totalScore.ToString();
        objPopup.SetActive(true);
        GameMgr.Instance.isPageOn = true;
    }
    public void HidePopup()
    {
        objPopup.SetActive(false);
        GameMgr.Instance.isPageOn = false;
    }
}
