using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartLoadPanel : BasePanel {

	// Use this for initialization
	protected override void Awake() 
	{
		base.Awake();
		Invoke("LoadNextScene",2f);
	} 
	
	private void LoadNextScene()
	{
		mUIFacade.ChangeSceneState(new MainSceneState(mUIFacade));
	}
}
