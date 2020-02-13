using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridPoint : MonoBehaviour {

	private SpriteRenderer spriteRenderer;
	private Sprite gridSprite; // 格子图片
	private Sprite monsterPathSprite; // 怪物路点图片

	public GameObject[] itemPrefabs; // 道具数组
	public GameObject currentItem; // 当前格子持有道具

	// 格子状态
	public struct GridState{
		public bool canBulid;
		public bool isMonster;
		public bool hasItem;
		public int itemID;
	}
	// 格子的索引
	public struct GridIndex{
		public int xIndex;
		public int yIndex;
	}
	public GridState gridState;
	public GridIndex gridIndex;


	private void Awake() {
		spriteRenderer = GetComponent<SpriteRenderer>();
		// 加载sprite资源
		string spritePath = "Pictures/NormalMordel/Game/";
		gridSprite = Resources.Load<Sprite>(spritePath+"Grid");
		monsterPathSprite = Resources.Load<Sprite>(spritePath+"1/Monster/1-1");
		// 加载Prefabs资源
		itemPrefabs = new GameObject[10];
		string prefabsPath = "Prefabs/Game/"+MapMaker.Instance.bigLevelID+"/";
		for(int i=0;i<itemPrefabs.Length;i++)
		{
			itemPrefabs[i] = Resources.Load<GameObject>(prefabsPath +"Item/"+"item_"+i);
			if(!itemPrefabs[i])
			{
				Debug.Log("加载失败，失败路径："+prefabsPath + "item_"+i);
			}
		}
		// 初始化gridState
		InitGrid();
	}

	// 初始化gridState
	public void InitGrid()
	{
		gridState.canBulid = true;
		gridState.isMonster = false;
		gridState.hasItem = false;
		spriteRenderer.enabled = true;
		gridState.itemID = 0;
	}

	// 鼠标事件监听
	private void OnMouseDown() {
		// 根据不同标志位，进行不同的操作
		if(MapMaker.Instance.actionFlag == MapMaker.ActionFlag.MonsterPath)
		{
			gridState.canBulid = false;
			gridState.isMonster = !gridState.isMonster; // 该状态下每次点击取反，即可以取消
			gridState.hasItem = false;
			spriteRenderer.enabled = true;
			if(gridState.isMonster)
			{
				// 是怪物路点的处理
				MapMaker.Instance.monsterPathGrids.Add(gridIndex);
				spriteRenderer.sprite = monsterPathSprite;
			}
			else
			{
				// 不是怪物路点的处理
				MapMaker.Instance.monsterPathGrids.Remove(gridIndex);
				spriteRenderer.sprite = gridSprite;
				gridState.canBulid = true;
			}

		}
		else if(MapMaker.Instance.actionFlag == MapMaker.ActionFlag.Item)
		{
			// 道具ID索引超出时
			if(gridState.itemID == itemPrefabs.Length)
			{
				gridState.itemID = -1;
				gridState.hasItem = false;
				Destroy(currentItem);
			}
			else if(currentItem == null)
			{
				// 产生当前itemID的道具
				currentItem = CreatItem();
			}
			else
			{
				Destroy(currentItem);
				// 产生当前itemID的道具
				currentItem = CreatItem();
			}
			// 点击生成道具模式
			gridState.itemID++;
			gridState.hasItem = true;
		}
		else if(MapMaker.Instance.actionFlag == MapMaker.ActionFlag.NotClick)
		{
			// 不可点击区域的情况
			if(!gridState.isMonster)
			{
				// 不为怪物路点时才可以操作
				gridState.canBulid = !gridState.canBulid;
				if(gridState.canBulid)
				{
					spriteRenderer.enabled = true;
				}
				else
				{
					spriteRenderer.enabled = false;
				}
			}
		}
	}

	// 生成当前itemID的道具
	public GameObject CreatItem()
	{
		Vector2 creatPos = transform.position;
		if(gridState.itemID<=2)
		{
			creatPos += new Vector2(MapMaker.Instance.gridWidth/2,MapMaker.Instance.gridHeight/2); 
		}
		else if(gridState.itemID <= 4)
		{
			creatPos += new Vector2(MapMaker.Instance.gridWidth/2,0); 
		}
		GameObject itemGo = Instantiate(itemPrefabs[gridState.itemID],creatPos,Quaternion.identity);
		return itemGo;
	}

	// 更新格子状态
	public void UpdataGrid()
	{
		if(gridState.canBulid)
		{
			spriteRenderer.enabled = true;
			if(gridState.hasItem)
			{
				CreatItem();
			}
		}
		else 
		{
			if(gridState.isMonster)
			{
				spriteRenderer.enabled = true;
				spriteRenderer.sprite = monsterPathSprite;
			}
			else
			{
				spriteRenderer.enabled = false;
			}
		}
	}
}
