using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class GameGlobal
{
    //Fixed Value
    public static float timeOneYear = 1.55f;

    //Excel Special Value
    public static int ageMinStudy = 0;
    public static int ageMaxStudy = 0;
    public static int ageStartGap = 16;
    public static int ageEndGap = 60;

    public static float yearMarryM = 0.5f;
    public static float yearMarryF = 2.5f;


    public static List<int> expEduLevelLimit = new List<int>();
    public static List<int> expCareerLevelLimit = new List<int>();

    public static List<Vector3> listPosHumanOrigin = new List<Vector3>
    {new Vector3(-2.55f,1.74f,0.86f),new Vector3(-2.35f,1.74f,0.86f),new Vector3(-2.15f,1.74f,0.86f),
    new Vector3(-2.55f,1.39f,0.86f),new Vector3(-2.35f,1.39f,0.86f),new Vector3(-2.15f,1.39f,0.86f) };

    public static List<Vector3> listPosHumanCookware = new List<Vector3>
    {new Vector3(-0.1f,0.15f,0),new Vector3(0.1f,0.15f,0),new Vector3(0,0.15f,0.1f) };

    public static bool isStart = false;
}
