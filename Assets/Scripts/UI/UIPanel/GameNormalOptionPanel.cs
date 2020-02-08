using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 正常模式中背景选择面板
public class GameNormalOptionPanel : BasePanel {

	public bool isInBigLevel = true;

	public void ReturnToLastPanel()
	{
		if(isInBigLevel)
		{
			// 返回主界面
			mUIFacade.ChangeSceneState(new MainSceneState(mUIFacade));
		}
		else
		{
			// 返回大关卡界面
			mUIFacade.currentScenePanels[StringManager.GameNormalLevelPanel].ExitPanel();
			mUIFacade.currentScenePanels[StringManager.GameNormalBigLevelPanel].EnterPanel();
		}
		isInBigLevel = true;
	}

	public void ToHelpPanel()
	{
		mUIFacade.currentScenePanels[StringManager.HelpPanel].EnterPanel();
	}


}
