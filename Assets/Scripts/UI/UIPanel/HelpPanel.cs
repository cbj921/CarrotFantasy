using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

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
		// 面板初始放在屏幕右边
		transform.localPosition = new Vector3(800,0,0);
		transform.SetSiblingIndex(5);
		helpScrollView.ResetScrollView();
		towerScrollView.ResetScrollView();
		ShowHelpPage();
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
		helpPanelTween.PlayBackwards();
		mUIFacade.currentScenePanels[StringManager.MainPanel].EnterPanel();
	}
}
