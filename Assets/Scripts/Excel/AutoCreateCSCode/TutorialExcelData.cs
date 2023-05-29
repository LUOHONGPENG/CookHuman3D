/*Auto Create, Don't Edit !!!*/

using UnityEngine;
using System.Collections.Generic;
using System;
using System.IO;

[Serializable]
public partial class TutorialExcelItem : ExcelItemBase
{
	public string strTip;
	public float posX;
	public float posY;
	public float sizeX;
	public float sizeY;
}

[CreateAssetMenu(fileName = "TutorialExcelData", menuName = "Excel To ScriptableObject/Create TutorialExcelData", order = 1)]
public partial class TutorialExcelData : ExcelDataBase<TutorialExcelItem>
{
}

#if UNITY_EDITOR
public class TutorialAssetAssignment
{
	public static bool CreateAsset(List<Dictionary<string, string>> allItemValueRowList, string excelAssetPath)
	{
		if (allItemValueRowList == null || allItemValueRowList.Count == 0)
			return false;
		int rowCount = allItemValueRowList.Count;
		TutorialExcelItem[] items = new TutorialExcelItem[rowCount];
		for (int i = 0; i < items.Length; i++)
		{
			items[i] = new TutorialExcelItem();
			items[i].id = Convert.ToInt32(allItemValueRowList[i]["id"]);
			items[i].strTip = allItemValueRowList[i]["strTip"];
			items[i].posX = Convert.ToSingle(allItemValueRowList[i]["posX"]);
			items[i].posY = Convert.ToSingle(allItemValueRowList[i]["posY"]);
			items[i].sizeX = Convert.ToSingle(allItemValueRowList[i]["sizeX"]);
			items[i].sizeY = Convert.ToSingle(allItemValueRowList[i]["sizeY"]);
		}
		TutorialExcelData excelDataAsset = ScriptableObject.CreateInstance<TutorialExcelData>();
		excelDataAsset.items = items;
		if (!Directory.Exists(excelAssetPath))
			Directory.CreateDirectory(excelAssetPath);
		string pullPath = excelAssetPath + "/" + typeof(TutorialExcelData).Name + ".asset";
		UnityEditor.AssetDatabase.DeleteAsset(pullPath);
		UnityEditor.AssetDatabase.CreateAsset(excelDataAsset, pullPath);
		UnityEditor.AssetDatabase.Refresh();
		return true;
	}
}
#endif


