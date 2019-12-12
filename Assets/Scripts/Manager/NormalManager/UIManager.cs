/*
 * @Author: your name
 * @Date: 2019-12-05 16:10:51
 * @LastEditTime: 2019-12-12 22:45:24
 * @LastEditors: Please set LastEditors
 * @Description: 负责UI的管理
 * @FilePath: \CarrotFantasy\Assets\Scripts\Manager\NormalManager\UIManager.cs
 */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager 
{
    public UIFacade mUIFacade;
    public Dictionary<string,GameObject> currentSceneUIDict = new Dictionary<string, GameObject>();
    private GameManager mGameManager;

    public UIManager()
    {
        mGameManager = GameManager.Instance;
        mUIFacade = new UIFacade();
    }
}
