using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Round {

	[System.Serializable]
	public struct RoundInfo{
		public int[] mMonsterIDList;
	}

	public RoundInfo roundInfo;
	// 责任链模式
	protected Round mNextRound;
	protected int mRoundID;
	protected Level mLevel;

	public Round(int[] monsterIDList,int roundID,Level level)
	{
		mLevel = level;
		mRoundID = roundID;
		roundInfo.mMonsterIDList = monsterIDList;
	}
	// 设置下一个处理对象(责任链)
	public void SetNextRound(Round nextRound)
	{
		mNextRound = nextRound;
	}
	// 处理方法
	public void Handle(int roundID)
	{
		if(mRoundID < roundID)
		{
			mNextRound.Handle(roundID);
		}
		else 
		{
			// 将怪物信息数组赋值给GameController中
			GameController.Instance.mMonsterIDList = roundInfo.mMonsterIDList;
			// 产生怪物
			GameController.Instance.CreatMonster();
		}
	}

}
