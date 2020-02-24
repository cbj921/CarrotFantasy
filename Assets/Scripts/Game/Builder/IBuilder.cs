using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IBuilder<T>  {

	// 得到相应物体身上的脚本
	T GetProduciClass(GameObject gameObject);

	// 使用工厂去获得具体的游戏对象
	GameObject GetProductGO();

	// 获取信息
	void GetData(T productClassGo);

	// 获得对应物品其他资源和信息
	void GetOtherResources(T productClassGo);
}
