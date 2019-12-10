using System.Collections;
using System.Collections.Generic;
using UnityEngine;


// 动画控制器资源加载工厂
public class RuntimeAnimatorControllerFactory : IBaseResourcesFactory<RuntimeAnimatorController> {

	protected string loadPath;
	protected Dictionary<string,RuntimeAnimatorController> resourceFactoryDict 
			= new Dictionary<string, RuntimeAnimatorController>();

	public RuntimeAnimatorControllerFactory()
	{
		loadPath = "Animator/"; // 初始化加载目录
	}

    public RuntimeAnimatorController GetSingleResources(string resourcePath)
    {
        RuntimeAnimatorController itemGo = null;
		string itemPath = loadPath + resourcePath;
		if(resourceFactoryDict.ContainsKey(resourcePath))
		{
			itemGo = resourceFactoryDict[resourcePath];
		}
		else
		{
			itemGo = Resources.Load<RuntimeAnimatorController>(itemPath);
			resourceFactoryDict.Add(resourcePath,itemGo);
		}
		// 进行安全性校验
		if(itemGo == null)
		{
			Debug.Log(resourcePath + " 的资源获取失败");
			Debug.Log("加载失败路径为："+ itemPath);
		}

		return itemGo;
    }
}
