/*
 * @Author: your name
 * @Date: 2019-12-05 16:05:51
 * @LastEditTime : 2020-02-16 19:51:23
 * @LastEditors: Please set LastEditors
 * @Description: 游戏控制管理，负责整个游戏的逻辑控制
 * @FilePath: \CarrotFantasy\Assets\Scripts\Manager\MonoBeheaviourManager\GameController.cs
 */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour {

	private static GameController _instance;
	public static GameController Instance{
		get{
			return _instance;
		}
	}

	public Level level;
	private GameManager mGameManager;
	public int[] mMonsterIDList;  // 当前波次的产怪列表
	public int mMonsterIDIndex; // 当前波次索引
	public Stage currentStage; // 当前小关卡信息

	// 游戏的UI面板
	private NormalModelPanel normalModelPanel;
	// UIFacade中介
	protected UIFacade mUIFacade; 
	protected MapMaker mapMaker;

	private void Awake() {
#if Game
		_instance = this;
		mGameManager = GameManager.Instance;
		currentStage = mGameManager.currentStage;
		mUIFacade = mGameManager.uiManager.mUIFacade;
		normalModelPanel = mUIFacade.currentScenePanels[StringManager.NormalModelPanel] as NormalModelPanel;
		mapMaker = transform.GetComponent<MapMaker>();
		// 初始化地图
		mapMaker.InitMapMaker();
		mapMaker.LoadMap(currentStage.mBigLevelID,currentStage.mLevelID);
		//mapMaker.LoadMap(1,3); // test
#endif
	}

	/// 资源获取的有关方法
	public Sprite GetSprite(string resourcePath)
	{
		return mGameManager.GetSprite(resourcePath);
	}
	public AudioClip GetAudioClip(string resourcePath)
	{
		return mGameManager.GetAudioClip(resourcePath);
	}
	public RuntimeAnimatorController GetRuntimeAnimatorController(string resourcePath)
	{
		return mGameManager.GetRunTimeAnimatorController(resourcePath);
	}
	public GameObject GetGameObjectResource(string resourcePath)
	{
		return mGameManager.GetGameObjectResource(FactoryType.GameFactory,resourcePath);
	}
	public void PushGameObjectResource(string resourcePath,GameObject itemGo)
	{
		mGameManager.PushGameObjectToFactory(FactoryType.GameFactory,resourcePath,itemGo);
	}
	
}
