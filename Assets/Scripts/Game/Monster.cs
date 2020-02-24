using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Monster : MonoBehaviour {

	// 属性值
	public int monsterID; 
	public int HP;
	public int currentHP; // 当前血量
	public float moveSpeed; // 当前速度
	public float initMoveSpeed; // 初始速度
	public int prize; // 奖励

	// 引用
	public Animator animator;
	public Slider slider;
	private GameObject TshitGo; // 便便预制体
	private GameController gameController;
	// 资源
	private RuntimeAnimatorController runtimeAnimator;
	private AudioClip deadAudioClip; 

	// 计数属性或开关
	private int roadPointIndex = 1;
	private bool isReachCarrot = false; // 是否到达终点标志位
	private bool isSlowSpeed = false; // 是否减速标志位
	private float slowSpeedTimer; // 减速计时器
	private float slowSpeedNeedTime; // 减速具体时间
	private int monsterPathPointNum; // 怪物路点的数量

	private void Awake() {
		slider.gameObject.SetActive(false); // 初始血条不可见，只有被攻击才可见
		gameController = GameController.Instance;
		monsterPathPointNum = MapMaker.Instance.monsterPathPos.Count;
	}
	private void Update() {
		if(GameController.Instance.isPause)
		{
			return;
		}
		if(!isReachCarrot)
		{
			// 怪物移动
			Vector2 endPoint = MapMaker.Instance.monsterPathPos[roadPointIndex];
			// 通过Distance函数来解决Lerp比例的问题
			float distance = Vector2.Distance(transform.position,endPoint);
			float proportion = 1/distance * Time.deltaTime * moveSpeed * gameController.gameSpeed;
			transform.position = Vector2.Lerp(transform.position,endPoint,proportion); // 第三个参数为运动总路程的比例
			if(distance <= 0.01f)
			{
				roadPointIndex++;
				if(roadPointIndex >= monsterPathPointNum)
				{
					isReachCarrot = true;
				}
			}
		}
		else // 到达终点
		{
			DestroyMonster();
			// TODO: 萝卜扣血
		}
	}

	// 销毁怪物方法
	public void DestroyMonster()
	{
		if(!isReachCarrot)// 被玩家杀死
		{
			//生成金币以及数目
			GameObject coinGo = gameController.GetGameObjectResource("CoinCanvas");
			coinGo.transform.Find("Emp_Coin").GetComponent<CoinMove>().prize = prize;
			coinGo.transform.position = slider.transform.position;
			coinGo.transform.SetParent(gameController.transform);
			//增加玩家金币数量函数
			gameController.ChangeCoin(prize);
			//TODO: 调用随机生成怪物窝函数
		}
		else // 到达萝卜点
		{

		}
		// 产生销毁特效
		GameObject destroyEffect = gameController.GetGameObjectResource("DestoryEffect");
		destroyEffect.transform.SetParent(gameController.transform);
		destroyEffect.transform.position = transform.position;
		//Debug.Log(destroyEffect);
		gameController.killMonsterNum++;
		gameController.killMonsterTotalNum++;
		initState();
		gameController.PushGameObjectResource("MonsterPrefab",gameObject); // 放回对象池
	}

	// 初始化状态
	public void initState()
	{
		monsterID = 0;
		currentHP = 0;
		moveSpeed = 0;
		prize = 0;
		slider.value = 1;
		roadPointIndex = 1;
		isReachCarrot = false;
		isSlowSpeed = false;
		deadAudioClip = null;
		slider.gameObject.SetActive(false);
		slowSpeedTimer = 0;
		slowSpeedNeedTime = 0;
		CancelDebuff();
	}

	// 承受伤害的方法
	private void TakeDamage(int attackVal)
	{
		slider.gameObject.SetActive(true);
		currentHP -= attackVal;
		if(currentHP <= 0)
		{
			// TODO: 播放死亡音效
			DestroyMonster();
			return;
		}
		// 更新血条
		slider.value = (float)currentHP / HP;
	}

	// 消除Debuff的方法
	private void CancelDebuff()
	{

	}


	// 获得特异性属性的方法
	public void GetMonsterProperity()
	{
		runtimeAnimator = GameController.Instance.monsterAnimator[monsterID];
		animator.runtimeAnimatorController = runtimeAnimator;
	}
}
