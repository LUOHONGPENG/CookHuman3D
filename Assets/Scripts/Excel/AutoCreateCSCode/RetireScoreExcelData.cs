/*Auto Create, Don't Edit !!!*/

using UnityEngine;
using System.Collections.Generic;
using System;
using System.IO;

[Serializable]
public partial class RetireScoreExcelItem : ExcelItemBase
{
	public RetireScoreType scoreType;
	public int keyValue;
	public string desc;
	public int init;
	public int slope;
	public int keyPointScore;
	public string remark;
}

[CreateAssetMenu(fileName = "RetireScoreExcelData", menuName = "Excel To ScriptableObject/Create RetireScoreExcelData", order = 1)]
public partial class RetireScoreExcelData : ExcelDataBase<RetireScoreExcelItem>
{
}

#if UNITY_EDITOR
public class RetireScoreAssetAssignment
{
	public static bool CreateAsset(List<Dictionary<string, string>> allItemValueRowList, string excelAssetPath)
	{
		if (allItemValueRowList == null || allItemValueRowList.Count == 0)
			return false;
		int rowCount = allItemValueRowList.Count;
		RetireScoreExcelItem[] items = new RetireScoreExcelItem[rowCount];
		for (int i = 0; i < items.Length; i++)
		{
			items[i] = new RetireScoreExcelItem();
			items[i].id = Convert.ToInt32(allItemValueRowList[i]["id"]);
			items[i].scoreType = (RetireScoreType) Enum.Parse(typeof(RetireScoreType), allItemValueRowList[i]["scoreType"], true);
			items[i].keyValue = Convert.ToInt32(allItemValueRowList[i]["keyValue"]);
			items[i].desc = allItemValueRowList[i]["desc"];
			items[i].init = Convert.ToInt32(allItemValueRowList[i]["init"]);
			items[i].slope = Convert.ToInt32(allItemValueRowList[i]["slope"]);
			items[i].keyPointScore = Convert.ToInt32(allItemValueRowList[i]["keyPointScore"]);
			items[i].remark = allItemValueRowList[i]["remark"];
		}
		RetireScoreExcelData excelDataAsset = ScriptableObject.CreateInstance<RetireScoreExcelData>();
		excelDataAsset.items = items;
		if (!Directory.Exists(excelAssetPath))
			Directory.CreateDirectory(excelAssetPath);
		string pullPath = excelAssetPath + "/" + typeof(RetireScoreExcelData).Name + ".asset";
		UnityEditor.AssetDatabase.DeleteAsset(pullPath);
		UnityEditor.AssetDatabase.CreateAsset(excelDataAsset, pullPath);
		UnityEditor.AssetDatabase.Refresh();
		return true;
	}
}
#endif


