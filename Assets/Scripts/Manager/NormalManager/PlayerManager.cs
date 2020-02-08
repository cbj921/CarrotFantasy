/*
 * @Author: your name
 * @Date: 2019-12-05 16:10:42
 * @LastEditTime : 2020-02-08 16:22:05
 * @LastEditors  : Please set LastEditors
 * @Description: 负责玩家各种数据的管理
 * @FilePath: \CarrotFantasy\Assets\Scripts\Manager\NormalManager\PlayerManager.cs
 */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager
{
    // 总的游戏数据
    public int normalLevelMapNum; // 解锁正常关卡数
    public int hideLevelMapNum; // 解锁隐藏关卡数
    public int bossLevelMapNum; // 解锁boss关卡数
    public int moneyNum; // 钱的总数
    public int killBossNum;
    public int killMonsterNum;
    public int destroyPropsNum; // 摧毁的道具总数

    // 怪物窝数据
    public int cookies;
    public int milk;
    public int diamonds;
    public int nest;

    // 关卡数据
    public List<bool> unlockedNormalBigLevelList ; // 大关卡的解锁信息
    public List<Stage> unlockedNormalLevelList ; // 各小关卡的信息
    public List<int> unlockedNormalLevelNum ; // 解锁的小关卡的数量
    public List<int> normalLevelTotalNum ;   // 每一个大关卡中小关卡的总数

    // 测试使用
    public PlayerManager()
    {
        unlockedNormalBigLevelList = new List<bool>() {true,true,false}; // 大关卡的解锁信息
        unlockedNormalLevelNum = new List<int>() {5,5,5}; // 解锁的小关卡的数量
        normalLevelTotalNum  = new List<int>() {5,5,5};   // 每一个大关卡中小关卡的总数
        unlockedNormalLevelList = new List<Stage>() {   new Stage(10,3,new int[]{1,2,3},0,1,1,false,true,false), 
                                                        new Stage(10,3,new int[]{1,2,7},0,1,2,false,true,false), 
                                                        new Stage(10,3,new int[]{1,2,4},0,1,3,false,true,false),
                                                        new Stage(10,3,new int[]{1,2,5},0,1,4,false,true,false),
                                                        new Stage(10,3,new int[]{1,2,6},0,1,5,false,true,true),
                                                        new Stage(10,3,new int[]{2,3,6},0,2,1,false,true,false),
                                                        new Stage(10,3,new int[]{1,2,6},0,2,2,false,true,false),
                                                        new Stage(10,3,new int[]{1,2,6},0,2,3,false,true,false),
                                                        new Stage(10,3,new int[]{1,2,6},0,2,4,false,true,false),
                                                        new Stage(10,3,new int[]{1,2,6},0,2,5,false,true,true),
                                                        new Stage(10,3,new int[]{1,2,6},0,3,1,false,true,false),
                                                        new Stage(10,3,new int[]{1,2,6},0,3,2,false,true,false),
                                                        new Stage(10,3,new int[]{1,2,6},0,3,3,false,true,false),
                                                        new Stage(10,3,new int[]{1,2,6},0,3,4,false,true,false),
                                                        new Stage(10,3,new int[]{2,5,6},0,3,5,false,true,true),};
    }

}
