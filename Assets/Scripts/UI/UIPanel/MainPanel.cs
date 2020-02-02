using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class MainPanel : BasePanel {

	private Animator carrotAnimator;
	private Transform monsterTransform;
	private Transform cloudTransform;
	// 动画数组
	public Tween[] mainPanelTween; // 0:右移动画 1：左移动画
	private Tween exitTween;   // 离开当前页面的动画

	protected override void Awake() {
		base.Awake();
		// 设置当前渲染层级
		transform.SetSiblingIndex(8);
		carrotAnimator = transform.Find("Emp_Carrot").GetComponent<Animator>();
		carrotAnimator.Play("CarrotGrow");
		monsterTransform = transform.Find("Img_Monster");
		cloudTransform = transform.Find("Img_Cloud");
		// 初始化动画
		mainPanelTween = new Tween[2];
		mainPanelTween[0] = transform.DOLocalMoveX(800,0.5f);
		mainPanelTween[0].SetAutoKill(false);
		mainPanelTween[0].Pause();
		mainPanelTween[1] = transform.DOLocalMoveX(-800,0.5f);
		mainPanelTween[1].SetAutoKill(false);
		mainPanelTween[1].Pause();
		// 播放动画
		PlayUITween();
	}

	// 进入当前界面
	public override void EnterPanel()
	{
		carrotAnimator.Play("CarrotGrow");
		if(exitTween != null)
		{
			exitTween.PlayBackwards();
		}
		cloudTransform.gameObject.SetActive(true);
	}
	// 退出当前页面
	public override void ExitPanel()
	{
		exitTween.PlayForward();
		cloudTransform.gameObject.SetActive(false);
	}
	// UI动画播放
	private void PlayUITween()
	{
		monsterTransform.DOLocalMoveY(250,1f).SetLoops(-1,LoopType.Yoyo);
		cloudTransform.DOLocalMoveX(550,5f).SetLoops(-1,LoopType.Restart);
	}
	// 进入设置界面
	public void ToSettingPanel()
	{
		exitTween = mainPanelTween[0];
		mUIFacade.currentScenePanels[StringManager.SettingPanel].EnterPanel();
	}
	// 进入帮助界面
	public void ToHelpPanel()
	{
		exitTween = mainPanelTween[1];
		mUIFacade.currentScenePanels[StringManager.HelpPanel].EnterPanel();
	}

	// 场景切换
	public void ToNormalModelScene()
	{
		mUIFacade.currentScenePanels[StringManager.GameNormalBigLevelPanel].EnterPanel();
		mUIFacade.ChangeSceneState(new GameNormalOptionSceneState(mUIFacade));
	}
	public void ToBossModelScene()
	{
		mUIFacade.currentScenePanels[StringManager.GameBossOptionPanel].EnterPanel();
		mUIFacade.ChangeSceneState(new GameBossOptionSceneState(mUIFacade));
	}
	public void ToMonsterNestScene()
	{
		mUIFacade.currentScenePanels[StringManager.MonsterNestPanel].EnterPanel();
		mUIFacade.ChangeSceneState(new MonsterNestSceneState(mUIFacade));
	}
	// 退出游戏
	public void ExitGame()
	{
		Application.Quit();
	}
}
