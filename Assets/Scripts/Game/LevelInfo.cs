﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelInfo  {

	public int bigLevelID;
	public int levelID;

	// 所有格子的状态列表
	public List<GridPoint.GridState> gridPoints;

	// 怪物路点列表
	public List<GridPoint.GridIndex> monsterPathPoints;

	// 波次信息
	public List<Round.RoundInfo> roundInfos;
}
