/*Auto Create, Don't Edit !!!*/

using UnityEngine;
using System.Collections.Generic;
using System;
using System.IO;

[Serializable]
public partial class SpecialValueExcelItem : ExcelItemBase
{
	public int ageMin_study;
	public int ageMax_study;
	public int ageStartGap;
	public int ageEndGap;
	public float yearMarryM;
	public float yearMarryF;
	public List<int> expEduLevel;
	public List<int> expCareerLevel;
}

[CreateAssetMenu(fileName = "SpecialValueExcelData", menuName = "Excel To ScriptableObject/Create SpecialValueExcelData", order = 1)]
public partial class SpecialValueExcelData : ExcelDataBase<SpecialValueExcelItem>
{
}

#if UNITY_EDITOR
public class SpecialValueAssetAssignment
{
	public static bool CreateAsset(List<Dictionary<string, string>> allItemValueRowList, string excelAssetPath)
	{
		if (allItemValueRowList == null || allItemValueRowList.Count == 0)
			return false;
		int rowCount = allItemValueRowList.Count;
		SpecialValueExcelItem[] items = new SpecialValueExcelItem[rowCount];
		for (int i = 0; i < items.Length; i++)
		{
			items[i] = new SpecialValueExcelItem();
			items[i].id = Convert.ToInt32(allItemValueRowList[i]["id"]);
			items[i].ageMin_study = Convert.ToInt32(allItemValueRowList[i]["ageMin_study"]);
			items[i].ageMax_study = Convert.ToInt32(allItemValueRowList[i]["ageMax_study"]);
			items[i].ageStartGap = Convert.ToInt32(allItemValueRowList[i]["ageStartGap"]);
			items[i].ageEndGap = Convert.ToInt32(allItemValueRowList[i]["ageEndGap"]);
			items[i].yearMarryM = Convert.ToSingle(allItemValueRowList[i]["yearMarryM"]);
			items[i].yearMarryF = Convert.ToSingle(allItemValueRowList[i]["yearMarryF"]);
			items[i].expEduLevel = new List<int>(Array.ConvertAll((allItemValueRowList[i]["expEduLevel"]).Split(';'), int.Parse));
			items[i].expCareerLevel = new List<int>(Array.ConvertAll((allItemValueRowList[i]["expCareerLevel"]).Split(';'), int.Parse));
		}
		SpecialValueExcelData excelDataAsset = ScriptableObject.CreateInstance<SpecialValueExcelData>();
		excelDataAsset.items = items;
		if (!Directory.Exists(excelAssetPath))
			Directory.CreateDirectory(excelAssetPath);
		string pullPath = excelAssetPath + "/" + typeof(SpecialValueExcelData).Name + ".asset";
		UnityEditor.AssetDatabase.DeleteAsset(pullPath);
		UnityEditor.AssetDatabase.CreateAsset(excelDataAsset, pullPath);
		UnityEditor.AssetDatabase.Refresh();
		return true;
	}
}
#endif


