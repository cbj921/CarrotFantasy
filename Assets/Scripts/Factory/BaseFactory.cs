using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
 * @description: 游戏物体工厂的基类 
 */
public class BaseFactory : IBaseFactory
{
	// 用一个字典来保存相应工厂的物体资源(UI,UIPanel,Game) PS:注意放的是资源
	protected Dictionary<string,GameObject> resourceFactoryDict 
		= new Dictionary<string, GameObject>();

	// 对象池,用字典来保存各种类型的对象池，前面的string表明的是选中哪个类型的资源栈
	protected Dictionary<string,Stack<GameObject>> objectPoolDict
		= new Dictionary<string,Stack<GameObject>>();

	// 加载路径
	protected string loadPath;
	public BaseFactory()
	{
		// 在构造函数中将加载路径设置成prefabs文件夹下
		loadPath = "Prefabs/";
	}

    public GameObject GetItem(string itemName)
    {
        GameObject itemGo = null;
		if(objectPoolDict.ContainsKey(itemName))
		{
			// 检查对象池中是否还有元素
			if(objectPoolDict[itemName].Count == 0)
			{
				GameObject go = GetResource(itemName);
				itemGo = GameManager.Instance.CreatItem(go);
			}
			else
			{
				itemGo = objectPoolDict[itemName].Pop();
				itemGo.SetActive(true);
			}
		}
		else
		{
			// 如果对象池字典中没有对应的对象池，那我们创建它
			objectPoolDict.Add(itemName,new Stack<GameObject>());
			// 获得资源，并不是一个实例，我们需要创建为实例
			GameObject go = GetResource(itemName); 
			// 调用gameManager中的 实例化 方法
			itemGo = GameManager.Instance.CreatItem(go);
		}
		// 安全性校验
		if(itemGo == null)
		{
			Debug.Log(itemName + " 获取失败");
		}
		return itemGo;
    }

	// 物体放入对象池中
    public void PushItem(string itemName, GameObject item)
    {
        item.SetActive(false);
		item.transform.SetParent(GameManager.Instance.transform); // 将失活的物体放在GameManager节点下
		// 进行安全性判断
		if(objectPoolDict.ContainsKey(itemName))
		{
			objectPoolDict[itemName].Push(item);
		}
		else
		{
			Debug.Log("当前对象池字典中不存在 "+itemName+" 的对象池");
		}
    }

	// 取资源的方法
	private GameObject GetResource(string itemName)
	{
		GameObject itemGo = null;
		string itemLoadPath = loadPath + itemName;
		if(resourceFactoryDict.ContainsKey(itemName))
		{
			itemGo = resourceFactoryDict[itemName];
		}
		else
		{
			itemGo = Resources.Load<GameObject>(itemLoadPath);
			resourceFactoryDict.Add(itemName,itemGo);
		}
		// 安全性判断
		if(itemGo == null)
		{
			// 经过上面程序itemGo == null，只有可能是路径出错
			Debug.Log(itemName + " 资源加载失败");
			Debug.Log("失败路径为："+ itemLoadPath);
		}
		return itemGo;
	}
}
