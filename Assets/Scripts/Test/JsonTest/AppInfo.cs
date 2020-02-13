using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// json测试文件中的 App类
public class AppInfo  {
	public int appNum;
	public int phoneState;
	public List<string> appNameList;

	public override string ToString()
	{
		string info;
		info = "appNum:"+appNum+"phoneState:" + phoneState;
		return info;
	}
}
