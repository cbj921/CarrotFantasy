using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// 小关卡选择面板
public class GameNormalLevelPanel : BasePanel {
	private string filePath;
	private string getSpritePath;
	private int currentBigLevelID;
	private int currentlevelID;

	public Transform levelContentTrans;
	private GameObject img_LockBtnGo;
	private Transform emp_TowerTrans;
	private Image img_BGLeft;
	private Image img_BGRight;
	private Image img_Carrot; // 获得的萝卜的奖杯图片
	private Image img_AllClear; // 全部清除的图片
	private SlideUnitForScrollView levelScrollView;

	private List<GameObject> levelContentImageGoList; // 用来放生成的小关卡的UI
	private List<GameObject> towerContentImageGoList; // 用来放要求塔的UI
	private Text text_WaveNumber;  // 波数的Text组件

	public int towerTotalNum;   // 塔的总数

	// 外部进入关卡界面的方法
	public void ToThisPanel(int bigLevelID)
	{
		currentBigLevelID = bigLevelID;
		currentlevelID = 1;
		EnterPanel();
	}
	// 初始化面板方法
	public override void InitPanel()
	{
		base.InitPanel();
		gameObject.SetActive(false);
	}
	// 退出界面
	public override void ExitPanel()
	{
		base.ExitPanel();
		gameObject.SetActive(false);
	}
	// 进入界面
	public override void EnterPanel()
	{
		base.EnterPanel();
		gameObject.SetActive(true);
		getSpritePath = filePath + currentBigLevelID + "/";
		DestroyMapUI();
		UpdateMapUI(getSpritePath);
		UpdateLevelUI(getSpritePath);
	}
	// 更新界面方法
	public override void UpdatePanel()
	{
		base.UpdatePanel();
		UpdateLevelUI(getSpritePath);
	}

	protected override void Awake()
	{
		base.Awake();
		filePath = "GameOption/Normal/Level/";
		levelContentImageGoList = new List<GameObject>();
		towerContentImageGoList = new List<GameObject>();
		currentBigLevelID = 1;
		currentlevelID = 1;
		// 给各个物体赋值
		img_LockBtnGo = transform.Find("Img_LockBtn").gameObject;
		emp_TowerTrans = transform.Find("Emp_Tower");
		img_BGLeft = transform.Find("Img_BGLeft").GetComponent<Image>();
		img_BGRight = transform.Find("Img_BGRight").GetComponent<Image>();
		levelScrollView = transform.Find("Scroll View").GetComponent<SlideUnitForScrollView>();
		text_WaveNumber = transform.Find("Emp_TotalWave").Find("Text_WaveNumber").GetComponent<Text>();
	}
	// 预先加载资源
	public void LoadResource()
	{
		mUIFacade.GetSprite(filePath+"AllClear");
		mUIFacade.GetSprite(filePath+"Carrot_1");
		mUIFacade.GetSprite(filePath+"Carrot_2");
		mUIFacade.GetSprite(filePath+"Carrot_3");
		// 根据大关卡的数目来加载资源
		List<int> levelNumList = mUIFacade.GetNormalLevelTotalNum(); // 大关卡中小关卡的总数
		for(int i=1;i<=levelNumList.Count;i++)
		{
			string spritePath = filePath + i + "/";
			mUIFacade.GetSprite(spritePath + "BG_Left");
			mUIFacade.GetSprite(spritePath + "BG_Right");
			// 根据小关卡数目来加载sprite
			for(int j=1;j<=levelNumList[i-1];j++)
			{
				mUIFacade.GetSprite(spritePath + "Level_" + j);
			}
			// 根据塔的数目来加载对应sprite
			for(int j=1;j<=towerTotalNum;j++)
			{
				mUIFacade.GetSprite(filePath + "Tower/Tower_" + j);
			}
		}
	}

	// 更新地图UI的方法
	public void UpdateMapUI(string spritePath)
	{
		img_BGRight.sprite = mUIFacade.GetSprite(spritePath + "BG_Right");
		img_BGLeft.sprite = mUIFacade.GetSprite(spritePath + "BG_Left");
		List<int> levelNumList = mUIFacade.GetNormalLevelTotalNum(); // 大关卡中小关卡的总数List
		List<Stage> levelInfoList = mUIFacade.GetUnlockedNormalLevelList(); // 获得小关卡的信息List
		for(int i=1;i<=levelNumList[currentBigLevelID-1];i++)
		{
			GameObject itemGo = CreatUIAndSetPos("Img_LevelMap",levelContentTrans);
			// 将Go添加到List中，方便后期销毁
			levelContentImageGoList.Add(itemGo);
			// 更换关卡的图片
			itemGo.GetComponent<Image>().sprite = mUIFacade.GetSprite(spritePath + "Level_" + i);
			// 先获得该大关卡前面小关卡的总数
			int beforeLevelNum=0; // 该大关卡前面小关卡的总数
			for(int j=0;j<currentBigLevelID-1;j++)
			{
				beforeLevelNum += levelNumList[j];
			}
			Stage levelStage = levelInfoList[beforeLevelNum + (i-1)]; // 得到当前小关卡的stage
			// 进行相关操作
			SetUIState(itemGo,levelStage);
		}

		// 设置滚动视图scrollView的content长度
		levelScrollView.SetContentLength(levelNumList[currentBigLevelID-1]);
		
	}
	// 通过Stage来对生成的UI做相关操作
	public void SetUIState(GameObject itemGo,Stage levelStage)
	{
		// 隐藏全清楚徽章和获得萝卜奖杯
		GameObject carrotCupGo = itemGo.transform.Find("Img_CarrotCup").gameObject;
		GameObject allClearCupGo = itemGo.transform.Find("Img_AllClearCup").gameObject;
		GameObject lockGo = itemGo.transform.Find("Img_Lock").gameObject;
		GameObject hideBGGo = itemGo.transform.Find("Img_HideBg").gameObject;
		carrotCupGo.SetActive(false);
		allClearCupGo.SetActive(false);
		// 进行条件判断
		if(levelStage.mUnlocked)
		{
			// 解锁
			if(levelStage.mAllClear)
			{
				allClearCupGo.SetActive(true);
			}
			if(levelStage.mCarrotState != 0)
			{
				// 0为初始值，表明还没通关
				Image Img_carrotCup = carrotCupGo.GetComponent<Image>();
				Img_carrotCup.sprite = mUIFacade.GetSprite(filePath + "Carrot_" + levelStage.mCarrotState);
				carrotCupGo.SetActive(true);
			}
			lockGo.SetActive(false);
			hideBGGo.SetActive(false);
		}
		else
		{
			// 未解锁
			if(levelStage.mIsRewardLevel)
			{
				// 为奖励关卡
				lockGo.SetActive(false);
				hideBGGo.SetActive(true);
				//更换养成动物的spite
				Image Img_Monster = itemGo.transform.Find("Img_HideBg").Find("Img_Monster").GetComponent<Image>();
				Img_Monster.sprite = mUIFacade.GetSprite("MonsterNest/Monster/Baby/"+currentBigLevelID);
				Img_Monster.SetNativeSize(); // 让图片保持自身比例
			}
			else
			{
				// 不为奖励关卡
				lockGo.SetActive(true);
				hideBGGo.SetActive(false);
			}
		}
	}

	// 更新静态UI的方法（波数Text和可用塔UI的更新）
	public void UpdateLevelUI(string spritePath)
	{
		// 销毁建塔UI的list中的内容
		DestroyTowerUI();
		List<int> levelNumList = mUIFacade.GetNormalLevelTotalNum(); // 大关卡中小关卡的总数List
		List<Stage> levelInfoList = mUIFacade.GetUnlockedNormalLevelList(); // 获得小关卡的信息List
		// 先获得该大关卡前面小关卡的总数
		int beforeLevelNum=0; // 该大关卡前面小关卡的总数
		for(int j=0;j<currentBigLevelID-1;j++)
		{
			beforeLevelNum += levelNumList[j];
		}
		Stage levelStage = levelInfoList[beforeLevelNum + currentlevelID-1];
		if(levelStage.mUnlocked)
		{
			img_LockBtnGo.SetActive(false);
		}
		else
		{
			img_LockBtnGo.SetActive(true);
		}
		text_WaveNumber.text = levelStage.mTotalRound.ToString(); // 更新波数text
		// 更新建塔的UI
		for(int i=0;i<levelStage.mTowerIDListLength;i++)
		{
			GameObject towerGo = CreatUIAndSetPos("Img_Tower",emp_TowerTrans);
			towerContentImageGoList.Add(towerGo); // 方便后续销毁
			Image img_towerGo = towerGo.GetComponent<Image>();
			img_towerGo.sprite = mUIFacade.GetSprite(filePath + "Tower/Tower_" + levelStage.mTowerIDList[i]);
		}

	}

//////////////////////////////
	// 实例化UI并且设置父对象
	public GameObject CreatUIAndSetPos(string uiName,Transform parentTrans)
	{
		GameObject itemGo = mUIFacade.GetGameObjectResource(FactoryType.UIFactory,uiName);
		itemGo.transform.SetParent(parentTrans);
		itemGo.transform.localPosition = Vector3.zero;
		itemGo.transform.localScale = Vector3.one;
		return itemGo;
	}

	// 销毁地图的关卡UI
	public void DestroyMapUI()
	{
		if(levelContentImageGoList.Count>0)
		{
			for(int i=0;i<levelContentImageGoList.Count;i++)
			{
				mUIFacade.PushGameObjectToFactory(FactoryType.UIFactory,"Img_LevelMap",levelContentImageGoList[i]);
			}
		}
		levelScrollView.ResetScrollView();
		levelContentImageGoList.Clear();
	}
	// 销毁建塔List中的UI
	public void DestroyTowerUI()
	{
		if(towerContentImageGoList.Count>0)
		{
			for(int i=0;i<towerContentImageGoList.Count;i++)
			{
				towerContentImageGoList[i].GetComponent<Image>().sprite = null;
				mUIFacade.PushGameObjectToFactory(FactoryType.UIFactory,"Img_Tower",towerContentImageGoList[i]);
			}
		}
		towerContentImageGoList.Clear();
	}

	// 选择小关卡进入游戏界面
	public void EnterGamePanel()
	{
		List<int> levelNumList = mUIFacade.GetNormalLevelTotalNum(); // 大关卡中小关卡的总数List
		List<Stage> levelInfoList = mUIFacade.GetUnlockedNormalLevelList(); // 获得小关卡的信息List
		// 先获得该大关卡前面小关卡的总数
		int beforeLevelNum=0; // 该大关卡前面小关卡的总数
		for(int j=0;j<currentBigLevelID-1;j++)
		{
			beforeLevelNum += levelNumList[j];
		}
		// 将当前小关卡的stage信息传递给GameManager中对应的变量
		mUIFacade.PassStageToGameManager(levelInfoList[beforeLevelNum + currentlevelID - 1]);
		// 切换场景
		mUIFacade.currentScenePanels[StringManager.GameLoadPanel].EnterPanel();
		mUIFacade.ChangeSceneState(new NormalModelSceneState(mUIFacade));
	}

	// 通过消息发送机制来调用随着scrollview滑动更改的levelID
	public void ToNextLevel()
	{
		List<int> levelNumList = mUIFacade.GetNormalLevelTotalNum(); // 大关卡中小关卡的总数List
		currentlevelID++;
		if(currentlevelID >levelNumList[currentBigLevelID-1])
		{
			currentlevelID = levelNumList[currentBigLevelID-1];
		}
		UpdateLevelUI(getSpritePath);
	}
	public void ToLastLevel()
	{
		currentlevelID--;
		if(currentlevelID <1)
		{
			currentlevelID = 1;
		}
		UpdateLevelUI(getSpritePath);
	}
}
