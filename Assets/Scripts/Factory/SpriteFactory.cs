using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteFactory : IBaseResourcesFactory<Sprite> {

	protected string loadPath;
	protected Dictionary<string,Sprite> resourceFactoryDict 
			= new Dictionary<string, Sprite>();

	public SpriteFactory()
	{
		loadPath = "Pictures/"; // 初始化加载目录
	}

    public Sprite GetSingleResources(string resourcePath)
    {
        Sprite itemGo = null;
		string itemPath = loadPath + resourcePath;
		if(resourceFactoryDict.ContainsKey(resourcePath))
		{
			itemGo = resourceFactoryDict[resourcePath];
		}
		else
		{
			itemGo = Resources.Load<Sprite>(itemPath);
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
