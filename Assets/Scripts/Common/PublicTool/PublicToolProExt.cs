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
            float requiredExp = GameGlobal.expCareerLevelLimit[levelEdu];
            float curExp = expEdu;
            if (levelEdu > 0)
            {
                curExp = expEdu - GameGlobal.expCareerLevelLimit[levelEdu - 1];
            }
            rateEdu = curExp / requiredExp;
        }
        else
        {
            rateEdu = 1f;
        }
        return rateEdu;
    }
}
