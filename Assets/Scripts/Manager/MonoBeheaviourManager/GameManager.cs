/*
 * @Author: your name
 * @Date: 2019-12-05 16:03:23
 * @LastEditTime: 2020-02-23 22:14:09
 * @LastEditors: Please set LastEditors
 * @Description: 该脚本是游戏负责全局控制的脚本
 * @FilePath: \CarrotFantasy\Assets\Scripts\Manager\MonoBeheaviourManager\GameManager.cs
 */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

	public PlayerManager playerManager;
	public AudioSourceManager audioSourceManager;
	public FactoryManager factoryManager;
	public UIManager uiManager;

	private static GameManager _instance;
	public static GameManager Instance{
		get{
			return _instance;
		}
	}

	public Stage currentStage; // 当前选择小关卡的stage

	private void Awake() {
		DontDestroyOnLoad(this.gameObject); // 该api表示该节点不会随着初始化而销毁，我们要让GameManager贯穿整个游戏
		_instance = this;   // 单例模式
		// 以下实例化必须按顺序来,以防止出错
		playerManager = new PlayerManager();
		factoryManager = new FactoryManager();
		audioSourceManager = new AudioSourceManager();
		// uiManager = new UIManager(); //因为测试注销
		// uiManager.mUIFacade.currentSceneState.EnterScene(); //因为测试注销
	}

	// 因为在工厂的脚本中，没有继承MonoBehaviour,所以无法使用Instantiate来实例化
	// 因此在该单例中写，以便其他脚本调用
	public GameObject CreatItem(GameObject item)
	{
		GameObject itemGo = Instantiate(item);
		return itemGo;
	}

	// 获取 Sprite 资源
	public Sprite GetSprite(string resourcePath)
	{
		return factoryManager.spriteFactory.GetSingleResources(resourcePath);
	}
	// 获取 audioClip 资源
	public AudioClip GetAudioClip(string resourcePath)
	{
		return factoryManager.audioClipFactory.GetSingleResources(resourcePath);
	}
	// 获取 runtimeAnimatorController 资源
	public RuntimeAnimatorController GetRunTimeAnimatorController(string resourcePath)
	{
		return factoryManager.runtimeAnimatorController.GetSingleResources(resourcePath);
	}
	// 获取游戏物体
	public GameObject GetGameObjectResource(FactoryType factoryType,string resourcePath)
	{
		return factoryManager.factoryDict[factoryType].GetItem(resourcePath);
	}
	// 将游戏物体放回工厂对象池
	public void PushGameObjectToFactory(FactoryType factoryType,string resourcePath,GameObject itemGo)
	{
		factoryManager.factoryDict[factoryType].PushItem(resourcePath,itemGo);
	}

}
