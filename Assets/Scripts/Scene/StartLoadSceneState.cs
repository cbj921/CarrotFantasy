using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartLoadSceneState : BaseSceneState
{
    public StartLoadSceneState(UIFacade uiFacade) : base(uiFacade)
    {
    }

    public override void EnterScene()
    {
        mUIFacade.AddPanelToDict(StringManager.StartLoadPanel); 
        base.EnterScene();
    }
    public override void ExitScene()
    {
        base.ExitScene();
        SceneManager.LoadScene(1);
    }
}
