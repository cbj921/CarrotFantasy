/*
 * @Author: your name
 * @Date: 2019-12-05 16:05:51
 * @LastEditTime: 2020-02-24 22:33:25
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
	public Stage currentStage; // 当前小关卡信息

	// 动画控制器资源
	public RuntimeAnimatorController[] monsterAnimator;

	// 游戏的UI面板
	private NormalModelPanel normalModelPanel;
	// UIFacade中介
	protected UIFacade mUIFacade; 
	protected MapMaker mapMaker;

	// 用于计数的成员变量
	public int killMonsterNum; // 当前波次杀怪数
	public int killMonsterTotalNum; // 总的杀怪数
	public int clearItemNum; // 销毁道具数量
	public int mMonsterIDIndex; // 用于统计当前怪物列表产生怪物的索引

	// 各属性值
	public int carrotHP = 10;
	public int coin = 500;
	public int gameSpeed;  // 当前游戏进行速度
	public bool isPause; // 是否暂停
	public Transform targetTrans; // 集火目标
	public GameObject targetSingel; // 集火信号
	public GridPoint selectPoint; // 当前选择的格子
	public bool isCreatMonster; // 是否继续产怪
	public bool gameOver; // 游戏结束标志


	// 建造者
	public MonsterBuilder monsterBuilder;

	// 建塔有关变量
	public Dictionary<int,int> towerBuildPriceDict;// 建塔价格表
	public GameObject towerListGo;	// 建塔按钮列表
	public GameObject handleTowerCanvasGo;// 处理塔升级与买卖的画布

	private void Awake() {
#if Game
		_instance = this;
		mGameManager = GameManager.Instance;
		currentStage = mGameManager.currentStage;
		// mUIFacade = mGameManager.uiManager.mUIFacade; //因为测试注销
		// normalModelPanel = mUIFacade.currentScenePanels[StringManager.NormalModelPanel] as NormalModelPanel; //因为测试注销
		mapMaker = transform.GetComponent<MapMaker>();
		// 初始化地图
		mapMaker.InitMapMaker();
		// mapMaker.LoadMap(currentStage.mBigLevelID,currentStage.mLevelID); //因为测试注销
		GetMonsterAnimator(); // 获得需要的动画控制器
		// 属性赋值
		gameSpeed = 1;
		monsterBuilder = new MonsterBuilder();
		// test
		mapMaker.LoadMap(1,1); 
		level = new Level(mapMaker.roundInfoList.Count,mapMaker.roundInfoList);
		level.HandleRound();
		// test
#endif
	}
	private void Update() {
		if(!isPause)
		{
			// 产怪逻辑
			if(killMonsterNum >= mMonsterIDList.Length)
			{
				AddRoundNum(); // 增加当前回合数
			}
			else
			{
				// 从暂停状态回来后
				if(!isCreatMonster)
				{
					CreatMonster();
				}
			}
		}
		else
		{
			// 暂停
			StopCreatMonster();
			isCreatMonster = false;
		}
	}

	// 产怪的方法
	public void CreatMonster()
	{
		isCreatMonster = true;
		InvokeRepeating("InstantiateMonster",(float)1/gameSpeed,(float)1/gameSpeed);
	}
	// 具体产怪方法
	public void InstantiateMonster()
	{
		// 产生特效
		GameObject CreatEffect = GetGameObjectResource("CreatEffect");
		CreatEffect.transform.SetParent(transform);
		CreatEffect.transform.position = mapMaker.monsterPathPos[0];
		// 产生怪物
		if(mMonsterIDIndex < mMonsterIDList.Length)
		{
			Round currentRound = level.roundArray[level.currentRound];
			monsterBuilder.mMonsterID = currentRound.roundInfo.mMonsterIDList[mMonsterIDIndex];
		}
		GameObject monsterGo = monsterBuilder.GetProductGO();
		monsterGo.transform.SetParent(transform);
		monsterGo.transform.position = mapMaker.monsterPathPos[0];
		mMonsterIDIndex++;
		if(mMonsterIDIndex>=mMonsterIDList.Length)
		{
			StopCreatMonster();
		}
	}
	// 停止产怪方法
	public void StopCreatMonster()
	{
		CancelInvoke();
	}
	// 增加当前回合数,产生下一回合怪物
	public void AddRoundNum()
	{
		mMonsterIDIndex = 0;
		killMonsterNum = 0;
		level.AddRoundNum();
		level.HandleRound();
		// TODO:更新有关回合显示的UI
	}

	// 改变玩家金币数
	public void ChangeCoin(int coinNum)
	{
		coin += coinNum;
		// TODO: 更新总金币数的UI显示
	}

	// 获得怪物的动画控制器
	public void GetMonsterAnimator()
	{
		monsterAnimator = new RuntimeAnimatorController[12];
		for(int i=0;i<monsterAnimator.Length;i++)
		{
			monsterAnimator[i] = GetRuntimeAnimatorController("AnimatorController/Monster/"+mapMaker.bigLevelID + "/"+(i+1).ToString());
		}
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
