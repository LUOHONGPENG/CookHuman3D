/*Auto Create, Don't Edit !!!*/

using UnityEngine;
using System.Collections.Generic;
using System;
using System.IO;

[Serializable]
public partial class MarryConditionExcelItem : ExcelItemBase
{
	public int numMarriage;
	public float timeOfYear;
	public int ageMinM;
	public int ageMaxM;
	public int ageMinF;
	public int ageMaxF;
	public int wEduM0;
	public int wEduM1;
	public int wEduM2;
	public int wEduM3;
	public int wEduM4;
	public int wEduM5;
	public float expectEduM;
	public int wEduF0;
	public int wEduF1;
	public int wEduF2;
	public int wEduF3;
	public int wEduF4;
	public int wEduF5;
	public float expectEduF;
	public int wCareerM0;
	public int wCareerM1;
	public int wCareerM2;
	public int wCareerM3;
	public int wCareerM4;
	public int wCareerM5;
	public float expectCareerM;
	public int wCareerF0;
	public int wCareerF1;
	public int wCareerF2;
	public int wCareerF3;
	public int wCareerF4;
	public int wCareerF5;
	public float expectCareerF;
}

[CreateAssetMenu(fileName = "MarryConditionExcelData", menuName = "Excel To ScriptableObject/Create MarryConditionExcelData", order = 1)]
public partial class MarryConditionExcelData : ExcelDataBase<MarryConditionExcelItem>
{
}

#if UNITY_EDITOR
public class MarryConditionAssetAssignment
{
	public static bool CreateAsset(List<Dictionary<string, string>> allItemValueRowList, string excelAssetPath)
	{
		if (allItemValueRowList == null || allItemValueRowList.Count == 0)
			return false;
		int rowCount = allItemValueRowList.Count;
		MarryConditionExcelItem[] items = new MarryConditionExcelItem[rowCount];
		for (int i = 0; i < items.Length; i++)
		{
			items[i] = new MarryConditionExcelItem();
			items[i].id = Convert.ToInt32(allItemValueRowList[i]["id"]);
			items[i].numMarriage = Convert.ToInt32(allItemValueRowList[i]["numMarriage"]);
			items[i].timeOfYear = Convert.ToSingle(allItemValueRowList[i]["timeOfYear"]);
			items[i].ageMinM = Convert.ToInt32(allItemValueRowList[i]["ageMinM"]);
			items[i].ageMaxM = Convert.ToInt32(allItemValueRowList[i]["ageMaxM"]);
			items[i].ageMinF = Convert.ToInt32(allItemValueRowList[i]["ageMinF"]);
			items[i].ageMaxF = Convert.ToInt32(allItemValueRowList[i]["ageMaxF"]);
			items[i].wEduM0 = Convert.ToInt32(allItemValueRowList[i]["wEduM0"]);
			items[i].wEduM1 = Convert.ToInt32(allItemValueRowList[i]["wEduM1"]);
			items[i].wEduM2 = Convert.ToInt32(allItemValueRowList[i]["wEduM2"]);
			items[i].wEduM3 = Convert.ToInt32(allItemValueRowList[i]["wEduM3"]);
			items[i].wEduM4 = Convert.ToInt32(allItemValueRowList[i]["wEduM4"]);
			items[i].wEduM5 = Convert.ToInt32(allItemValueRowList[i]["wEduM5"]);
			items[i].expectEduM = Convert.ToSingle(allItemValueRowList[i]["expectEduM"]);
			items[i].wEduF0 = Convert.ToInt32(allItemValueRowList[i]["wEduF0"]);
			items[i].wEduF1 = Convert.ToInt32(allItemValueRowList[i]["wEduF1"]);
			items[i].wEduF2 = Convert.ToInt32(allItemValueRowList[i]["wEduF2"]);
			items[i].wEduF3 = Convert.ToInt32(allItemValueRowList[i]["wEduF3"]);
			items[i].wEduF4 = Convert.ToInt32(allItemValueRowList[i]["wEduF4"]);
			items[i].wEduF5 = Convert.ToInt32(allItemValueRowList[i]["wEduF5"]);
			items[i].expectEduF = Convert.ToSingle(allItemValueRowList[i]["expectEduF"]);
			items[i].wCareerM0 = Convert.ToInt32(allItemValueRowList[i]["wCareerM0"]);
			items[i].wCareerM1 = Convert.ToInt32(allItemValueRowList[i]["wCareerM1"]);
			items[i].wCareerM2 = Convert.ToInt32(allItemValueRowList[i]["wCareerM2"]);
			items[i].wCareerM3 = Convert.ToInt32(allItemValueRowList[i]["wCareerM3"]);
			items[i].wCareerM4 = Convert.ToInt32(allItemValueRowList[i]["wCareerM4"]);
			items[i].wCareerM5 = Convert.ToInt32(allItemValueRowList[i]["wCareerM5"]);
			items[i].expectCareerM = Convert.ToSingle(allItemValueRowList[i]["expectCareerM"]);
			items[i].wCareerF0 = Convert.ToInt32(allItemValueRowList[i]["wCareerF0"]);
			items[i].wCareerF1 = Convert.ToInt32(allItemValueRowList[i]["wCareerF1"]);
			items[i].wCareerF2 = Convert.ToInt32(allItemValueRowList[i]["wCareerF2"]);
			items[i].wCareerF3 = Convert.ToInt32(allItemValueRowList[i]["wCareerF3"]);
			items[i].wCareerF4 = Convert.ToInt32(allItemValueRowList[i]["wCareerF4"]);
			items[i].wCareerF5 = Convert.ToInt32(allItemValueRowList[i]["wCareerF5"]);
			items[i].expectCareerF = Convert.ToSingle(allItemValueRowList[i]["expectCareerF"]);
		}
		MarryConditionExcelData excelDataAsset = ScriptableObject.CreateInstance<MarryConditionExcelData>();
		excelDataAsset.items = items;
		if (!Directory.Exists(excelAssetPath))
			Directory.CreateDirectory(excelAssetPath);
		string pullPath = excelAssetPath + "/" + typeof(MarryConditionExcelData).Name + ".asset";
		UnityEditor.AssetDatabase.DeleteAsset(pullPath);
		UnityEditor.AssetDatabase.CreateAsset(excelDataAsset, pullPath);
		UnityEditor.AssetDatabase.Refresh();
		return true;
	}
}
#endif


