using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class GameGlobal
{
    //Fixed Value
    public static float timeOneYear = 1.5f;

    //Excel Special Value
    public static int ageMinStudy = 0;
    public static int ageMaxStudy = 0;

    public static List<int> expEduLevelLimit = new List<int>();
    public static List<int> expCareerLevelLimit = new List<int>();

    public static List<Vector3> listPosHumanOrigin = new List<Vector3>
    {new Vector3(-2.1f,1.85f,0.7f),new Vector3(-1.9f,1.85f,0.7f),new Vector3(-1.7f,1.85f,0.7f),
    new Vector3(-2.1f,1.42f,0.7f),new Vector3(-1.9f,1.42f,0.7f),new Vector3(-1.7f,1.42f,0.7f) };

    public static List<Vector3> listPosHumanCookware = new List<Vector3>
    {new Vector3(-0.1f,0.15f,0),new Vector3(0.1f,0.15f,0),new Vector3(0,0.15f,0.1f) };
}
