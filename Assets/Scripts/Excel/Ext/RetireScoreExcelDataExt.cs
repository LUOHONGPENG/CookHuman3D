using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class RetireScoreExcelData
{

    public Dictionary<RetireScoreType, List<RetireScoreExcelItem>> dicRetireScoreType = new Dictionary<RetireScoreType, List<RetireScoreExcelItem>>();

    public void Init()
    {
        dicRetireScoreType.Clear();

        for (int i = 0;i< items.Length; i++)
        {
            RetireScoreExcelItem scoreItem = items[i];
            if (dicRetireScoreType.ContainsKey(scoreItem.scoreType))
            {
                List<RetireScoreExcelItem> listScore = dicRetireScoreType[scoreItem.scoreType];
                listScore.Add(scoreItem);
            }
            else
            {
                List<RetireScoreExcelItem> listScore = new List<RetireScoreExcelItem>();
                listScore.Add(scoreItem);
                dicRetireScoreType.Add(scoreItem.scoreType, listScore);
            }
        }
    }

    public ScoreInfo CalculateScore(RetireScoreType scoreType,int keyValue)
    {
        if (dicRetireScoreType.ContainsKey(scoreType))
        {
            List<RetireScoreExcelItem> listScore = dicRetireScoreType[scoreType];
            //Calculate the index of target item
            int targetIndex = -1;
            for (int i = 0; i < listScore.Count; i++)
            {
                if(i < listScore.Count - 1)
                {
                    if (keyValue >= listScore[i].keyValue && keyValue < listScore[i + 1].keyValue)
                    {
                        targetIndex = i;
                    }
                    else if(keyValue < listScore[i].keyValue)
                    {
                        break;
                    }
                }
                else
                {
                    if (keyValue >= listScore[i].keyValue)
                    {
                        targetIndex = i;
                    }
                }
            }

            if (targetIndex >= 0)
            {
                RetireScoreExcelItem targetItem = listScore[targetIndex];

                int targetScore = targetItem.init + targetItem.slope * keyValue;
                string targetDesc = targetItem.desc;

                return new ScoreInfo(targetDesc, targetScore);
            }
        }
        return new ScoreInfo("",0);
    }

}

public struct ScoreInfo
{
    public string desc;
    public int score;

    public ScoreInfo(string desc,int score)
    {
        this.desc = desc;
        this.score = score;
    }
}
