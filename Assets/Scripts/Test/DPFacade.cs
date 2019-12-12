using System.Collections;
using System.Collections.Generic;
using UnityEngine;


// 该脚本为  外观模式   的测试脚本
public class DPFacade : MonoBehaviour {

	// Use this for initialization
	void Start () {
		Boss boss = new Boss();
		boss.orderManager();	
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}

// 上层客户端
public class Boss
{
	private Manager manager = new Manager();
	public void orderManager()
	{
		manager.orderStaffWork();
	}
}

// 外观者
public class Manager
{
	private Staff1 staff1 = new Staff1();
	private Staff2 staff2 = new Staff2();

	public void orderStaffWork()
	{
		staff1.work();
		staff2.work();
	}
}

// 下层子系统
public class Staff1
{
	public void work()
	{
		Debug.Log("Staff1 is working.");
	}
}
public class Staff2
{
	public void work()
	{
		Debug.Log("Staff2 is working.");
	}
}


