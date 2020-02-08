using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameNormalOptionSceneState : BaseSceneState
{
    public GameNormalOptionSceneState(UIFacade uiFacade) : base(uiFacade)
    {
    }
    public override void EnterScene()
    {
        // 添加场景中需要的面板到字典中
        mUIFacade.AddPanelToDict(StringManager.HelpPanel);
        mUIFacade.AddPanelToDict(StringManager.GameLoadPanel);
        mUIFacade.AddPanelToDict(StringManager.GameNormalOptionPanel);
        mUIFacade.AddPanelToDict(StringManager.GameNormalBigLevelPanel);
        mUIFacade.AddPanelToDict(StringManager.GameNormalLevelPanel);
        base.EnterScene();
    }
    public override void ExitScene()
    {
        base.ExitScene();
    }
}
