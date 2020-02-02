/*
 * @Author: your name
 * @Date: 2019-12-05 16:09:08
 * @LastEditTime: 2020-01-21 15:46:01
 * @LastEditors: Please set LastEditors
 * @Description: 工厂管理，负责管理各种类型的工厂
 * @FilePath: \CarrotFantasy\Assets\Scripts\Manager\NormalManager\FactoryManager.cs
 */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FactoryManager 
{
    public Dictionary<FactoryType,IBaseFactory> factoryDict = new Dictionary<FactoryType, IBaseFactory>();
    public AudioClipFactory audioClipFactory;
    public SpriteFactory spriteFactory;
    public RuntimeAnimatorControllerFactory runtimeAnimatorController;

    public FactoryManager()
    {
        factoryDict.Add(FactoryType.UIPanelFactory,new UIPanelFactory());
        factoryDict.Add(FactoryType.UIFactory,new UIFactory());
        factoryDict.Add(FactoryType.GameFactory,new GameFactory());
        audioClipFactory = new AudioClipFactory();
        spriteFactory = new SpriteFactory();
        runtimeAnimatorController = new RuntimeAnimatorControllerFactory();
    }

}
