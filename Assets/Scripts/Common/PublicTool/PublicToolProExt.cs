using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class PublicTool
{
    //Calculate the level of Edu according to exp
    public static int CalculateEduLevel(float expEdu)
    {
        int levelEdu = 0;

        for(int i = 0; i < GameGlobal.expEduLevelLimit.Count; i++)
        {
            if(expEdu >= GameGlobal.expEduLevelLimit[i])
            {
                levelEdu = i + 1;
            }
        }
        return levelEdu;
    }

    //Calculate the level of Career according to exp
    public static int CalculateCareerLevel(float expCareer)
    {
        int levelCareer = 0;

        for (int i = 0; i < GameGlobal.expCareerLevelLimit.Count; i++)
        {
            if (expCareer >= GameGlobal.expCareerLevelLimit[i])
            {
                levelCareer = i + 1;
            }
        }
        return levelCareer;
    }

    //Calculate Edu Rate
    public static float CalculateEduRate(float expEdu)
    {
        int levelEdu = CalculateEduLevel(expEdu);
        float rateEdu = 0;
        if(levelEdu < GameGlobal.expEduLevelLimit.Count)
        {
            float requiredExp = GameGlobal.expEduLevelLimit[levelEdu];
            float curExp = expEdu;
            if (levelEdu > 0)
            {
                requiredExp = GameGlobal.expEduLevelLimit[levelEdu] - GameGlobal.expEduLevelLimit[levelEdu - 1];
                curExp = expEdu - GameGlobal.expEduLevelLimit[levelEdu - 1];
            }
            rateEdu = curExp / requiredExp;
        }
        else
        {
            rateEdu = 1f;
        }
        return rateEdu;
    }

    //Calculate Edu Rate
    public static float CalculateCareerRate(float expCareer)
    {
        int levelCareer = CalculateCareerLevel(expCareer);
        float rateCareer = 0;
        if (levelCareer < GameGlobal.expCareerLevelLimit.Count)
        {
            float requiredExp = GameGlobal.expCareerLevelLimit[levelCareer];
            float curExp = expCareer;
            if (levelCareer > 0)
            {
                requiredExp = GameGlobal.expCareerLevelLimit[levelCareer] - GameGlobal.expCareerLevelLimit[levelCareer - 1];
                curExp = expCareer - GameGlobal.expCareerLevelLimit[levelCareer - 1];
            }
            rateCareer = curExp / requiredExp;
        }
        else
        {
            rateCareer = 1f;
        }
        return rateCareer;
    }

    public static void PlaySound(SoundType soundType)
    {
        EventCenter.Instance.EventTrigger("PlaySound", soundType);
    }

    public static void StopSound(SoundType soundType)
    {
        EventCenter.Instance.EventTrigger("StopSound", soundType);
    }

    public static void WarningTip(string strWarning,Vector3 headPos)
    {
        EffectUIInfo info = new EffectUIInfo("Warning", PublicTool.CalculateUICanvasPos(headPos, GameMgr.Instance.mapCamera), 0,strWarning);
        EventCenter.Instance.EventTrigger("EffectUI", info);
    }

    public static List<int> DrawTwo(List<int> listPool, List<int> listDelete)
    {
        List<int> listTemp = new List<int>();
        List<int> listDraw = new List<int>(listPool);
        for(int i = 0; i < listDelete.Count; i++)
        {
            listDraw.Remove(listDelete[i]);
        }
        
        for(int i = 0; i < 2; i++)
        {
            int index = Random.Range(0, listDraw.Count);
            listTemp.Add(listDraw[index]);
            listDraw.RemoveAt(index);
        }
        return listTemp;
    }

    #region Effort
    public static bool CheckWhetherEffortGot(int ID)
    {
        if (GameMgr.Instance.listEffortGot.Contains(ID))
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public static EffortExcelItem GetEffortItem(int ID)
    {
        return GameMgr.Instance.dataMgr.effortExcelData.GetExcelItem(ID);
    }

    public static float GetCurrentTimeYear()
    {
        if (GameMgr.Instance.dataMgr != null)
        {
            if (GameMgr.Instance.dataMgr.marryConditionData != null)
            {
                MarryConditionExcelItem marryConditionItem = GameMgr.Instance.dataMgr.marryConditionData.GetMarryItem(GameMgr.Instance.numMarry);
                return marryConditionItem.timeOfYear;
            }
        }
        return GameGlobal.timeOneYear;
    }
    #endregion
}
