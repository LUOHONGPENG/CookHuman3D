/*Auto Create, Don't Edit !!!*/

using UnityEngine;
using System.Collections.Generic;
using System;
using System.IO;

[Serializable]
public partial class CookwareExcelItem : ExcelItemBase
{
	public string name;
	public string desc;
	public CookwareType cookwareType;
	public int capacity;
	public float growRate;
	public int ageMin_real;
	public int ageMax_real;
	public int eduMin;
	public float posxCapa;
	public float posyCapa;
}

[CreateAssetMenu(fileName = "CookwareExcelData", menuName = "Excel To ScriptableObject/Create CookwareExcelData", order = 1)]
public partial class CookwareExcelData : ExcelDataBase<CookwareExcelItem>
{
}

#if UNITY_EDITOR
public class CookwareAssetAssignment
{
	public static bool CreateAsset(List<Dictionary<string, string>> allItemValueRowList, string excelAssetPath)
	{
		if (allItemValueRowList == null || allItemValueRowList.Count == 0)
			return false;
		int rowCount = allItemValueRowList.Count;
		CookwareExcelItem[] items = new CookwareExcelItem[rowCount];
		for (int i = 0; i < items.Length; i++)
		{
			items[i] = new CookwareExcelItem();
			items[i].id = Convert.ToInt32(allItemValueRowList[i]["id"]);
			items[i].name = allItemValueRowList[i]["name"];
			items[i].desc = allItemValueRowList[i]["desc"];
			items[i].cookwareType = (CookwareType) Enum.Parse(typeof(CookwareType), allItemValueRowList[i]["cookwareType"], true);
			items[i].capacity = Convert.ToInt32(allItemValueRowList[i]["capacity"]);
			items[i].growRate = Convert.ToSingle(allItemValueRowList[i]["growRate"]);
			items[i].ageMin_real = Convert.ToInt32(allItemValueRowList[i]["ageMin_real"]);
			items[i].ageMax_real = Convert.ToInt32(allItemValueRowList[i]["ageMax_real"]);
			items[i].eduMin = Convert.ToInt32(allItemValueRowList[i]["eduMin"]);
			items[i].posxCapa = Convert.ToSingle(allItemValueRowList[i]["posxCapa"]);
			items[i].posyCapa = Convert.ToSingle(allItemValueRowList[i]["posyCapa"]);
		}
		CookwareExcelData excelDataAsset = ScriptableObject.CreateInstance<CookwareExcelData>();
		excelDataAsset.items = items;
		if (!Directory.Exists(excelAssetPath))
			Directory.CreateDirectory(excelAssetPath);
		string pullPath = excelAssetPath + "/" + typeof(CookwareExcelData).Name + ".asset";
		UnityEditor.AssetDatabase.DeleteAsset(pullPath);
		UnityEditor.AssetDatabase.CreateAsset(excelDataAsset, pullPath);
		UnityEditor.AssetDatabase.Refresh();
		return true;
	}
}
#endif


