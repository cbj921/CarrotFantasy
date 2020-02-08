using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stage  
{
	public int[] mTowerIDList; // 当前关卡可用的塔类型
	public int mTowerIDListLength; // 上面数组的长度
	public bool mAllClear;  // 道具是否全清
	public int mCarrotState; // 萝卜的状态
	public int mLevelID; // 关卡ID
	public int mBigLevelID; // 大关卡ID
	public bool mUnlocked; // 是否解锁
	public bool mIsRewardLevel; // 是否为奖励关卡
	public int mTotalRound; // 全部波数

	public Stage(int totalRound,int towerIDListLength,int[] towerIDList,int carrotState,
					int bigLevelID,int levelID,bool allClear,bool unlocked,bool isRewardLevel)
	{
		mTotalRound = totalRound;
		mTowerIDListLength = towerIDListLength;
		mTowerIDList = towerIDList;
		mCarrotState = carrotState;
		mBigLevelID = bigLevelID;
		mLevelID = levelID;
		mAllClear = allClear;
		mUnlocked = unlocked;
		mIsRewardLevel = isRewardLevel;
	}
}
