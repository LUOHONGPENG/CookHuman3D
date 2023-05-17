using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class PublicTool
{
    public static void ClearChildItem(UnityEngine.Transform tf)
    {
        foreach (UnityEngine.Transform item in tf)
        {
            UnityEngine.Object.Destroy(item.gameObject);
        }
    }


    /// <summary>
    /// Useful function for Calculate angle. For example, from Target to (0,1)
    /// </summary>
    /// <param name="from"></param>
    /// <param name="to"></param>
    /// <returns></returns>
    public static float CalculateAngle(Vector2 from, Vector2 to)
    {
        float angle;
        Vector3 cross = Vector3.Cross(from, to);
        angle = Vector2.Angle(from, to);
        return cross.z > 0 ? angle : -angle;
    }

    public static Vector3 CalculateUICanvasPos(Vector3 objPos,Camera tarCamera)
    {
        Vector2 screenPos = RectTransformUtility.WorldToScreenPoint(tarCamera, objPos);
        screenPos = new Vector2(screenPos.x * 1920f / Screen.width, screenPos.y * 1080f / Screen.height);
        Vector2 targetPos = new Vector2(screenPos.x - 1920f / 2, screenPos.y - 1080f / 2);
        return new Vector3(targetPos.x, targetPos.y, 0);
    }
}
