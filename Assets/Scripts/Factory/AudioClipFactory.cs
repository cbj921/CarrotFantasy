using System.Collections;
using System.Collections.Generic;
using UnityEngine;


// 音乐资源加载工厂
public class AudioClipFactory : IBaseResourcesFactory<AudioClip>
{
	protected string loadPath;
	protected Dictionary<string,AudioClip> resourceFactoryDict 
			= new Dictionary<string, AudioClip>();

	public AudioClipFactory()
	{
		loadPath = "AudioClips/"; // 初始化加载目录
	}

    public AudioClip GetSingleResources(string resourcePath)
    {
        AudioClip itemGo = null;
		string itemPath = loadPath + resourcePath;
		if(resourceFactoryDict.ContainsKey(resourcePath))
		{
			itemGo = resourceFactoryDict[resourcePath];
		}
		else
		{
			itemGo = Resources.Load<AudioClip>(itemPath);
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
