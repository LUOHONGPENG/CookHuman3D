/*Auto Create, Don't Edit !!!*/

using UnityEngine;
using System.Collections.Generic;
using System;
using System.IO;

[Serializable]
public partial class EffortExcelItem : ExcelItemBase
{
	public string name;
	public string desc;
	public float value0;
	public float value1;
	public List<int> affect;
	public string remark;
}

[CreateAssetMenu(fileName = "EffortExcelData", menuName = "Excel To ScriptableObject/Create EffortExcelData", order = 1)]
public partial class EffortExcelData : ExcelDataBase<EffortExcelItem>
{
}

#if UNITY_EDITOR
public class EffortAssetAssignment
{
	public static bool CreateAsset(List<Dictionary<string, string>> allItemValueRowList, string excelAssetPath)
	{
		if (allItemValueRowList == null || allItemValueRowList.Count == 0)
			return false;
		int rowCount = allItemValueRowList.Count;
		EffortExcelItem[] items = new EffortExcelItem[rowCount];
		for (int i = 0; i < items.Length; i++)
		{
			items[i] = new EffortExcelItem();
			items[i].id = Convert.ToInt32(allItemValueRowList[i]["id"]);
			items[i].name = allItemValueRowList[i]["name"];
			items[i].desc = allItemValueRowList[i]["desc"];
			items[i].value0 = Convert.ToSingle(allItemValueRowList[i]["value0"]);
			items[i].value1 = Convert.ToSingle(allItemValueRowList[i]["value1"]);
			items[i].affect = new List<int>(Array.ConvertAll((allItemValueRowList[i]["affect"]).Split(';'), int.Parse));
			items[i].remark = allItemValueRowList[i]["remark"];
		}
		EffortExcelData excelDataAsset = ScriptableObject.CreateInstance<EffortExcelData>();
		excelDataAsset.items = items;
		if (!Directory.Exists(excelAssetPath))
			Directory.CreateDirectory(excelAssetPath);
		string pullPath = excelAssetPath + "/" + typeof(EffortExcelData).Name + ".asset";
		UnityEditor.AssetDatabase.DeleteAsset(pullPath);
		UnityEditor.AssetDatabase.CreateAsset(excelDataAsset, pullPath);
		UnityEditor.AssetDatabase.Refresh();
		return true;
	}
}
#endif


