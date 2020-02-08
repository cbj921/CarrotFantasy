using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.SceneManagement;

public class HelpPanel : BasePanel {
	private GameObject helpPageGo;
	private GameObject monsterPageGo;
	private GameObject towerPageGo;
	private SlideUnitForScrollView helpScrollView;
	private SlideUnitForScrollView towerScrollView;
	private Tween helpPanelTween;

	protected override void Awake() {
		base.Awake();
		helpPageGo = transform.Find("HelpPage").gameObject;
		monsterPageGo = transform.Find("MonsterPage").gameObject;
		towerPageGo = transform.Find("TowerPage").gameObject;
		helpScrollView = helpPageGo.transform.Find("Scroll View").GetComponent<SlideUnitForScrollView>();
		towerScrollView = towerPageGo.transform.Find("Scroll View").GetComponent<SlideUnitForScrollView>();
		helpPanelTween = transform.DOLocalMoveX(0,0.5f);
		helpPanelTween.SetAutoKill(false);
		helpPanelTween.Pause();
	}
	public override void InitPanel()
	{
		transform.SetSiblingIndex(5);
		helpScrollView.ResetScrollView();
		towerScrollView.ResetScrollView();
		ShowHelpPage();
		// 其他处理
		if(transform.localPosition == Vector3.zero)
		{
			gameObject.SetActive(false);
			helpPanelTween.PlayBackwards();
		}
		// 面板初始放在屏幕右边
		transform.localPosition = new Vector3(800,0,0);
	}
	// 显示页面的方法
	public void ShowHelpPage()
	{
		helpPageGo.SetActive(true);
		monsterPageGo.SetActive(false);
		towerPageGo.SetActive(false);
		
	}
	public void ShowMonsterPage()
	{
		helpPageGo.SetActive(false);
		monsterPageGo.SetActive(true);
		towerPageGo.SetActive(false);
	}
	public void ShowTowerPage()
	{
		helpPageGo.SetActive(false);
		monsterPageGo.SetActive(false);
		towerPageGo.SetActive(true);
	}
	public override void EnterPanel()
	{
		InitPanel();
		gameObject.SetActive(true);
		helpPanelTween.PlayForward();
	}
	public override void ExitPanel()
	{
		// 因为除了主场景，在其他场景也有help界面，所以对不同情况不同处理
		if(mUIFacade.currentSceneState.GetType() == typeof(MainSceneState))
		{
			helpPanelTween.PlayBackwards();
			mUIFacade.currentScenePanels[StringManager.MainPanel].EnterPanel();
		}
		else
		{
			// 在冒险模式场景下回到选择关卡界面
			//mUIFacade.ChangeSceneState(new MainSceneState(mUIFacade));
			helpPanelTween.PlayBackwards();
		}
		
	}
}
