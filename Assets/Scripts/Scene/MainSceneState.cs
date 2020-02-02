using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainSceneState : BaseSceneState
{
    public MainSceneState(UIFacade uiFacade) : base(uiFacade)
    {
    }
    public override void EnterScene()
    {
        // 将 主界面中有的都添加到字典中
        mUIFacade.AddPanelToDict(StringManager.MainPanel);
        mUIFacade.AddPanelToDict(StringManager.SettingPanel);
        mUIFacade.AddPanelToDict(StringManager.HelpPanel);
        mUIFacade.AddPanelToDict(StringManager.GameLoadPanel);
        base.EnterScene();
    }
    public override void ExitScene()
    {
        base.ExitScene();
        if(mUIFacade.currentSceneState.GetType() == typeof(GameNormalOptionSceneState))
        {
            SceneManager.LoadScene(2);
        }
        else if(mUIFacade.currentSceneState.GetType() == typeof(GameBossOptionSceneState))
        {
            SceneManager.LoadScene(4);
        }
        else if(mUIFacade.currentSceneState.GetType() == typeof(MonsterNestSceneState))
        {
            SceneManager.LoadScene(6);
        }
    }
}
