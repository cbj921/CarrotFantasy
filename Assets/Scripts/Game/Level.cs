using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 关卡产生怪物类
public class Level  {
	public int totalRound;
	public Round[] roundArray;
	public int currentRound;

	public Level(int roundNum,List<Round.RoundInfo> roundInfos)
	{
		totalRound = roundNum;
		roundArray = new Round[totalRound];
		// 给Round数组赋值
		for(int i=0;i<totalRound;i++)
		{
			roundArray[i] = new Round(roundInfos[i].mMonsterIDList,i,this);
		}
		// 设置任务链
		for(int i=0;i<totalRound;i++)
		{
			if(i == totalRound-1)
			{
				break;
			}
			roundArray[i].SetNextRound(roundArray[i+1]);
		}
	}

	// 处理每个Round的方法
	public void HandleRound()
	{
		if(currentRound >= totalRound)
		{
			// TODO: win
		}
		else if(currentRound == totalRound -1) 
		{
			// 最后一波怪的UI显示和音乐播放
		}
		else 
		{
			roundArray[currentRound].Handle(currentRound);
		}
	}

	// 处理最后一波怪的方法
	public void HandleLastRound()
	{
		roundArray[totalRound-1].Handle(totalRound-1);
	}

	// 增加当前波次索引
	public void AddRoundNum()
	{
		currentRound++;
	}
}
