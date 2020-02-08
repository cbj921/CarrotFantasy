using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// 大关卡选择面板
public class GameNormalBigLevelPanel : BasePanel {

	public Transform bigLevelContentTrans;
	public int bigLevelPageCount; // 大关卡总数
	private SlideUnitForScrollView bigLevelSlideScrollView;
	private Transform[] bigLevelPage; //  大关卡按钮数组
	private bool hasRigisterEvent;

	protected override void Awake() {
		base.Awake();
		bigLevelSlideScrollView = transform.Find("Scroll View").GetComponent<SlideUnitForScrollView>();
		// 显示各关卡信息
		bigLevelPage = GetContentChildTrans();
		ShowBigLevelInfo();

	}
	private void OnEnable() {
		ShowBigLevelInfo();
	}
	// 得到各大关卡的Trans
	public Transform[] GetContentChildTrans()
	{
		Transform[] chlidTrans = new Transform[bigLevelPageCount];
		for(int i=0;i<bigLevelPageCount;i++)
		{
			chlidTrans[i] = bigLevelContentTrans.GetChild(i);
		}
		return chlidTrans;
	}
	// 显示大关卡信息
	public void ShowBigLevelInfo()
	{
		List<bool> unlockedNormalBigLevelList = mUIFacade.GetUnlockNormalBigLevelList();
		List<int> unlockedNormalLevelNum = mUIFacade.GetUnlockedNormalLevelNum();
		List<int> normalLevelTotalNum = mUIFacade.GetNormalLevelTotalNum();
		for(int i=0;i<bigLevelPageCount;i++)
		{
			ShowBigLevelState(unlockedNormalBigLevelList[i],unlockedNormalLevelNum[i],normalLevelTotalNum[i],i+1,bigLevelPage[i]);
		}
	}
	// 显示大关卡状态
	public void ShowBigLevelState(bool isUnlock,int unlockLevelNum,int totalNum,int bigLevelID,Transform bigLevelTrans)
	{
		Button bigLevelButton = bigLevelTrans.GetComponent<Button>();
		GameObject Img_Lock = bigLevelTrans.Find("Img_Lock").gameObject;
		GameObject Img_Page = bigLevelTrans.Find("Img_Page").gameObject;
		Text Text_Page = Img_Page.transform.Find("Text_Page").GetComponent<Text>();
		if(isUnlock) // 解锁状态
		{
			Img_Lock.SetActive(false);
			Img_Page.SetActive(true);
			Text_Page.text = unlockLevelNum + "/" + totalNum;
			bigLevelButton.interactable = true;
			// 给关卡按钮注册事件
			if(!hasRigisterEvent)
			{
				RigisterEvent(bigLevelButton,bigLevelID);
			}
		}
		else  // 锁定状态
		{
			Img_Lock.SetActive(true);
			Img_Page.SetActive(false);
			bigLevelButton.interactable = false;
		}
	}
	// 给关卡按钮注册事件
	public void RigisterEvent(Button bigLevelBtn,int bigLevelID)
	{
		bigLevelBtn.onClick.AddListener(()=>{
			// 离开大关卡
			mUIFacade.currentScenePanels[StringManager.GameNormalBigLevelPanel].ExitPanel();
			// 进入小关卡选择页面
			GameNormalLevelPanel gameNormalLevelPanel = mUIFacade.currentScenePanels[StringManager.GameNormalLevelPanel] 
														as GameNormalLevelPanel;
			gameNormalLevelPanel.ToThisPanel(bigLevelID);
			GameNormalOptionPanel gameNormalOptionPanel = mUIFacade.currentScenePanels[StringManager.GameNormalOptionPanel]
														as GameNormalOptionPanel;
			gameNormalOptionPanel.isInBigLevel = false;
			hasRigisterEvent = true;
		});
		
	}

	public override void EnterPanel()
	{
		base.EnterPanel();
		bigLevelSlideScrollView.ResetScrollView();
		gameObject.SetActive(true);
	}
	public override void ExitPanel()
	{
		base.ExitPanel();
		gameObject.SetActive(false);
	}

	// 翻页按钮方法
	public void ToNextPage()
	{
		bigLevelSlideScrollView.ToNextPage();
	}
	public void ToLastPage()
	{
		bigLevelSlideScrollView.ToLastPage();
	}
}
