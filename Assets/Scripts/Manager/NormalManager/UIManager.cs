/*
 * @Author: your name
 * @Date: 2019-12-05 16:10:51
 * @LastEditTime : 2020-01-23 23:43:43
 * @LastEditors  : Please set LastEditors
 * @Description: 负责UI的管理
 * @FilePath: \CarrotFantasy\Assets\Scripts\Manager\NormalManager\UIManager.cs
 */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager 
{
    public UIFacade mUIFacade;
    public Dictionary<string,GameObject> currentSceneUIDict;
    private GameManager mGameManager;

    public UIManager()
    {
        mGameManager = GameManager.Instance;
        currentSceneUIDict = new Dictionary<string, GameObject>();
        mUIFacade = new UIFacade(this);
        mUIFacade.currentSceneState = new StartLoadSceneState(mUIFacade); // 初始化状态为加载状态
    }

    // 清空字典，并且将其中的对象放回对象池
    public void ClearUIDict()
    {
        foreach(var item in currentSceneUIDict)
        {
            // 因为预制体实例化后每个物体后面都带有后缀“（Clone）”，因此我们得将后缀去掉才能正确回收
            string itemName = item.Value.name;
            itemName = itemName.Substring(0,itemName.Length-7);
            PushUIPanel(itemName,item.Value);
        }
        currentSceneUIDict.Clear();
    }
    // 将UIPanel放回对象池
    public void PushUIPanel(string uiPanelName,GameObject uiPanelGo)
    {
        mGameManager.PushGameObjectToFactory(FactoryType.UIPanelFactory,uiPanelName,uiPanelGo);
    }

}
