using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.IO;

#if Tool
// 编辑器拓展
[CustomEditor(typeof(MapMaker))]
public class MapTool : Editor {
	private MapMaker mapMaker;
	// 关卡文件列表
	private List<FileInfo> fileList = new List<FileInfo>();
	private string[] fileNameList;
	// 当前编辑关卡的索引
	private int selectIndex = -1;

	public override void OnInspectorGUI()
	{
		base.OnInspectorGUI();
		if(Application.isPlaying)
		{
			// 游戏运行时才去绘制面板
			EditorGUILayout.BeginHorizontal();
			//在begin和end之间是对应的布局
			// 获得文件名
			fileNameList = GetNames();
			// 获得MapMaker单例
			mapMaker = MapMaker.Instance;
			// 下拉文件列表
			int currentIndex = EditorGUILayout.Popup(selectIndex,fileNameList);
			if(currentIndex != selectIndex)
			{
				// 当前选择对象是否改变
				selectIndex = currentIndex;
				// 实例化地图的方法
				mapMaker.InitMap();
				// 加载当前选择的level文件 
				LevelInfo levelFile = mapMaker.LoadLevelInfoByJson(fileNameList[selectIndex]);
				mapMaker.LoadLevelData(levelFile);

			}
			if(GUILayout.Button("读取关卡列表"))
			{
				LoadLevelFiles(); 
			}
			EditorGUILayout.EndHorizontal();

			EditorGUILayout.BeginHorizontal();
			if(GUILayout.Button("恢复地图编辑器默认状态"))
			{
				mapMaker.RecoverTowerPoint();
			}
			if(GUILayout.Button("清除怪物路点"))
			{
				mapMaker.ClearMonsterPoint();
			}
			EditorGUILayout.EndHorizontal();

			EditorGUILayout.BeginHorizontal();
			if(GUILayout.Button("更新当前地图背景和道路"))
			{
				mapMaker.SetMapBgAndRoad();
				mapMaker.RecoverTowerPoint();
			}
			EditorGUILayout.EndHorizontal();

			if(GUILayout.Button("保存当前关卡数据"))
			{
				mapMaker.SaveFileByJson();
			}
		}
	}

	// 加载关卡数据文件
	private void LoadLevelFiles()
	{
		// 每次加载文件的时候都要清空文件列表
		ClearList();
		fileList = GetLevelFiles();
	}
	// 清除文件列表
	private void ClearList()
	{
		fileList.Clear();
		selectIndex = -1;
	}
	// 具体读取关卡列表
	private List<FileInfo> GetLevelFiles()
	{
		string jsonFilePath = Application.dataPath + "/Resources/Json/Level/";
		string[] files = Directory.GetFiles(jsonFilePath,"*.json"); // 读取后缀为Json的文件
		List<FileInfo> fileList = new List<FileInfo>();
		for(int i=0;i<files.Length;i++)
		{
			FileInfo fileInfo = new FileInfo(files[i]);
			fileList.Add(fileInfo);
		}
		return fileList;
	}
	// 提取关卡文件的名字
	private string[] GetNames()
	{
		List<string> fileName = new List<string>();
		foreach(FileInfo item in fileList)
		{
			string name = item.Name;
			fileName.Add(name);
		}
		return fileName.ToArray();
	}
}	
#endif