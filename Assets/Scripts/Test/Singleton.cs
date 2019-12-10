using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class SingletonTemplete<T>:MonoBehaviour
where T : MonoBehaviour
{
	private static T _instance;
	public static T Instance
	{
		get{
			return _instance;
		}
	}
	private void Awake() {
		_instance = this as T; // 将this转换成T类型
	}

	// private static T _instance;
	// public static T Instance
	// {	
	// 	get{
	// 		if(_instance == null)
	// 		{
	// 			_instance = new T();
	// 		}
	// 		return _instance;
	// 	}
	// }
}
