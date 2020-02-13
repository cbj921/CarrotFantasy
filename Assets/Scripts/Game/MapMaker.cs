using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LitJson;
using System.IO;

public class MapMaker : MonoBehaviour {

	public bool isDrawLine = true; // 是否画线，调试是使用
	public GameObject gridGo; // 格子的物体

	// 地图的属性
	private float mapWidth;
	private float mapHeight;

	// 格子的属性
	[HideInInspector]
	public float gridWidth;
	[HideInInspector]
	public float gridHeight;

	// 当前关卡索引
	public int bigLevelID;
	public int levelID;

	// 存储所有格子的二维数组
	[HideInInspector]
	public GridPoint[,] gridPoints;

	// 怪物路径点(存储的为格子的索引)
	[HideInInspector]
	public List<GridPoint.GridIndex> monsterPathGrids = new List<GridPoint.GridIndex>(); 
	// 怪物的路近点(存储的是格子的位置，vector3)
	[HideInInspector]
	public List<Vector2> monsterPathPos = new List<Vector2>();

	// 每一波次产生的怪物ID列表
	public List<Round.RoundInfo> roundInfoList ;

	// 背景和道路的SpriteRender
	[HideInInspector]
	public SpriteRenderer bgSR;
	[HideInInspector]
	public SpriteRenderer roadSR;

	// 切分的行数和列数
	private int row = 8; // 行数
	private int column = 12; // 列数

	// 对格点不同操作的枚举变量
	// 不同操作的标志位,枚举类型
	public enum ActionFlag
	{
		MonsterPath,// 怪物路点
		Item,    // 道具点
		NotClick, // 不可点击点
	}
	public ActionFlag actionFlag = ActionFlag.MonsterPath;

	// 单例
	private static MapMaker _instance;
	public static MapMaker Instance{
		get{
			return _instance;
		}
	}

	private void Awake() {
		_instance = this;
		InitMapMaker();
	}

	// 初始化地图制造者相关变量
	public void InitMapMaker()
	{
		CalcGirdSize(); // 计算相关参数
		gridPoints = new GridPoint[row,column];
		for(int i=0;i<row;i++)
		{
			for(int j=0;j<column;j++)
			{
				GameObject itemGo = Instantiate(gridGo,transform.position,transform.rotation);
				itemGo.transform.localPosition =new Vector2(-mapWidth/2+j*gridWidth+gridWidth/2,-mapHeight/2+i*gridHeight+gridHeight/2);
				itemGo.transform.SetParent(transform);
				gridPoints[i,j] = itemGo.GetComponent<GridPoint>();
				gridPoints[i,j].gridIndex.xIndex = i;
				gridPoints[i,j].gridIndex.yIndex = j;
			}
		}
		bgSR = transform.Find("BG").GetComponent<SpriteRenderer>();
		roadSR = transform.Find("Road").GetComponent<SpriteRenderer>();
	}

	// 计算地图格子的宽高
	private void CalcGirdSize()
	{
		Vector2 leftDown = new Vector2(0,0);
		Vector2 rightUp = new Vector2(1,1);

		Vector2 pos_1 = Camera.main.ViewportToWorldPoint(leftDown);
		Vector2 pos_2 = Camera.main.ViewportToWorldPoint(rightUp);
		// 得到地图的宽高
		mapWidth = pos_2.x - pos_1.x;
		mapHeight = pos_2.y - pos_1.y;
		// 计算格子的宽高
		gridWidth = mapWidth / column;
		gridHeight = mapHeight / row;
	}

	// 画格子用于辅助设计
	private void OnDrawGizmos() 
	{
		// 该方法是Unity的内置方法，即不用再运行模式下也会执行的内容
		if(isDrawLine)
		{
			CalcGirdSize();
			Gizmos.color = Color.yellow; // 定义画的线的颜色
			// 画行
			for(int i=0;i<row;i++)
			{
				Vector2 startPos = new Vector2(-mapWidth/2,-mapHeight/2+i*gridHeight);
				Vector2 endPos = new Vector2(mapWidth/2,-mapHeight/2+i*gridHeight);
				Gizmos.DrawLine(startPos,endPos);
			}
			// 画列
			for(int i=0;i<column;i++)
			{
				Vector2 startPos = new Vector2(-mapWidth/2+i*gridWidth,-mapHeight/2);
				Vector2 endPos = new Vector2(-mapWidth/2+i*gridWidth,mapHeight/2);
				Gizmos.DrawLine(startPos,endPos);
			}
		}
	}

	// 有关地图编辑的方法

	// 清除怪物路点
	public void ClearMonsterPoint()
	{
		for(int i=0;i<monsterPathGrids.Count;i++)
		{
			int xIndex = monsterPathGrids[i].xIndex;
			int yIndex = monsterPathGrids[i].yIndex;
			gridPoints[xIndex,yIndex].InitGrid();
		}
		monsterPathGrids.Clear();
	}

	// 恢复地图默认状态
	public void RecoverTowerPoint()
	{
		ClearMonsterPoint();
		for(int i=0;i<row;i++)
		{
			for(int j=0;j<column;j++)
			{
				gridPoints[i,j].InitGrid();
			}
		}
	}

	// 初始化地图方法
	public void InitMap()
	{
		RecoverTowerPoint();
		roundInfoList.Clear();
		bigLevelID = 0;
		levelID = 0;
		bgSR.sprite = null;
		roadSR.sprite = null;
	}

	// 生成LevelInfo对象来保存文件
	public LevelInfo CreatLevelInfoGo()
	{
		LevelInfo levelInfo = new LevelInfo();
		levelInfo.bigLevelID = bigLevelID;
		levelInfo.levelID = levelID;
		// 所有格子信息
		levelInfo.gridPoints = new List<GridPoint.GridState>();
		for(int i=0;i<row;i++)
		{
			for(int j=0;j<column;j++)
			{
				levelInfo.gridPoints.Add(gridPoints[i,j].gridState);
			}
		}
		// 怪物路点信息
		levelInfo.monsterPathPoints = new List<GridPoint.GridIndex>();
		for(int i=0;i<monsterPathGrids.Count;i++)
		{
			levelInfo.monsterPathPoints.Add(monsterPathGrids[i]);
		}
		// 波次数信息
		levelInfo.roundInfos = new List<Round.RoundInfo>();
		for(int i=0;i<roundInfoList.Count;i++)
		{
			levelInfo.roundInfos.Add(roundInfoList[i]);
		}
		Debug.Log("保存成功");
		return levelInfo;
	}

	// 以Json形式保存信息至对应的路径
	public void SaveFileByJson()
	{
		LevelInfo levelInfoGo = CreatLevelInfoGo();
		string filePath = Application.dataPath + "/Resources/Json/Level/" + "level_" +bigLevelID + "_" + levelID + ".json";
		string saveJsonStr = JsonMapper.ToJson(levelInfoGo);
		StreamWriter sw = new StreamWriter(filePath);
		sw.Write(saveJsonStr);
		sw.Close();
	}
	// 加载Json文件转化为LevelInfo数据
	public LevelInfo LoadLevelInfoByJson(string fileName)
	{
		LevelInfo levelInfo = new LevelInfo();
		string filePath = Application.dataPath + "/Resources/Json/Level/" + fileName; 
		if(File.Exists(filePath))
		{
			StreamReader sr = new StreamReader(filePath);
			string loadJsonStr = sr.ReadToEnd();
			sr.Close();
			levelInfo = JsonMapper.ToObject<LevelInfo>(loadJsonStr); 
			return levelInfo;
		}
		else 
		{
			Debug.Log("文件加载失败: "+filePath);
		}
		return levelInfo;
	}

	// 将获得的LevelInfo数据赋值给MapMaker里面的各成员变量
	public void LoadLevelData(LevelInfo levelInfo)
	{
		bigLevelID = levelInfo.bigLevelID;
		levelID = levelInfo.levelID;
		// 二维数组赋值
		for(int i=0;i<row;i++)
		{
			for(int j=0;j<column;j++)
			{
				// LevelInfo中的二维数组实际上为一维数组，我们需要转换成二维，json只能存储一维数组
				// 此时的levelInfo.gridPoints实际为一个一维数组
				// 经过修改，将LevelInfo类中的gridPoints改为了List
				gridPoints[i,j].gridState = levelInfo.gridPoints[i*column+j];
				// 更新格子的状态
				gridPoints[i,j].UpdataGrid();
			}
		}
		// 怪物路点赋值
		monsterPathGrids.Clear();
		for(int i=0;i<levelInfo.monsterPathPoints.Count;i++)
		{
			monsterPathGrids.Add(levelInfo.monsterPathPoints[i]);
		}
		// 怪物波次数赋值
		roundInfoList = new List<Round.RoundInfo>();
		for(int i=0;i<levelInfo.roundInfos.Count;i++)
		{
			roundInfoList.Add(levelInfo.roundInfos[i]);
		}
		// 加载BG和Road图片资源
		bgSR.sprite = Resources.Load<Sprite>("Pictures/NormalMordel/Game/"+bigLevelID+"/BG"+(levelID/3));// 前三关用BG0，后两关用BG1
		roadSR.sprite = Resources.Load<Sprite>("Pictures/NormalMordel/Game/"+bigLevelID+"/Road"+levelID);

	}
}
