using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LitJson;
using System.IO; // 用来读写文件的IO库

public class JsonTest : MonoBehaviour {

	private AppInfo appInfo;

	private void Start() {
		appInfo =  new AppInfo{
			appNum = 3,
			phoneState = 1,
			appNameList = new List<string>() {
				"come on",
				"everyBody",
				"hello"
			}
		};
		//SaveByJson();
		AppInfo test = LoadByJson();
		Debug.Log(test.ToString());
	}

	// 存储Json信息
	private void SaveByJson()
	{
		string filePath = Application.dataPath + "/Resources" + "/Json"+"/JsonTest.json";
		// 利用JsonMapper将信息类转换成json格式的字符串
		string saveJsonStr = JsonMapper.ToJson(appInfo);
		// 创建文件流，将json字符串写入对应文件中
		StreamWriter sw = new StreamWriter(filePath); // 新建文件流类的参数为写入的路径
		sw.Write(saveJsonStr);
		sw.Close();
		Debug.Log("写入Json成功");
	}

	// 读取json的信息文件
	private AppInfo LoadByJson()
	{
		string filePath = Application.dataPath + "/Resources" + "/Json"+"/JsonTest.json";
		AppInfo ap = new AppInfo();
		if(File.Exists(filePath))
		{
			StreamReader sr = new StreamReader(filePath);
			string jsonStr = sr.ReadToEnd();
			sr.Close();
			ap = JsonMapper.ToObject<AppInfo>(jsonStr);
		}
		if(ap == null)
		{
			Debug.Log("读取Json失败");
			Debug.Log("失败路径："+filePath);
		}
		return ap;
	}
}
