using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HumanItem
{
    //Basic Data
    public int HumanID = -1;
    public Sex sex;
    public int Age = 0;
    public float expEdu = 0;//Value about Education
    public float expCareer = 0;//Value about Job
    public bool isMarried = false;//Whether this people is married
    //Record
    public int vMarryAge = -1;
    public int vRetireAge = -1;
    public int vScore = 0;

    public HumanItem(int ID)
    {
        //Initial Basic data
        this.HumanID = ID;
        this.Age = 0;
        this.isMarried = false;
        this.expEdu = 0.0001f;
        this.expCareer = 0.0001f;
        //Initial Sex data
        int ranSex = Random.Range(0, 2);
        if(ranSex == 0)
        {
            sex = Sex.Male;
        }
        else if(ranSex == 1)
        {
            sex = Sex.Female;
        }
        //Initial Score
        vScore = 0;
    }

    #region Basic Record
    /// <summary>
    /// The age grow
    /// </summary>
    public void AgeGrow()
    {
        this.Age++;
    }

    public void RecordMarried()
    {
        isMarried = true;
        vMarryAge = Age;
        GameMgr.Instance.numMarry++;
    }

    public void RecordRetired()
    {
        vRetireAge = Age;
        GameMgr.Instance.ChargeEffort();
    }
    #endregion

    #region TimeGo

    public void TimeGoGrowEdu(float eduDelta)
    {
        expEdu += eduDelta;
        if (expEdu > 100f)
        {
            expEdu = 100f;
        }
    }

    public void TimeGoGrowCareer( float careerDelta)
    {
        expCareer += careerDelta;
        if(expCareer > 100f)
        {
            expCareer = 100f;
        }
    }
    #endregion

    #region Comment

    public List<ScoreInfo> GetCommentList()
    {
        RetireScoreExcelData scoreData = GameMgr.Instance.dataMgr.retireScoreData;
        List<ScoreInfo> listTempScore = new List<ScoreInfo>();

        for (int i = (int)RetireScoreType.Edu; i < (int)RetireScoreType.End; i++)
        {
            int keyValue = 0;
            switch ((RetireScoreType)i)
            {
                case RetireScoreType.Edu:
                    keyValue = Mathf.FloorToInt(expEdu);
                    break;
                case RetireScoreType.Career:
                    keyValue = Mathf.FloorToInt(expCareer);
                    break;
                case RetireScoreType.Marriage:
                    keyValue = vMarryAge;
                    break;
                case RetireScoreType.Retire:
                    keyValue = vRetireAge;
                    break;
            }

            ScoreInfo scoreTemp = scoreData.CalculateScore((RetireScoreType)i, keyValue);
            if (scoreTemp.desc.Length > 0)
            {
                listTempScore.Add(scoreTemp);
            }
        }

        return listTempScore;
    }


    #endregion
}
