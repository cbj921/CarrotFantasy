using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameLoadPanel : BasePanel {

	public override void EnterPanel()
	{
		gameObject.SetActive(true);
		transform.SetSiblingIndex(8);
	}
	public override void InitPanel()
	{
		gameObject.SetActive(false);
	}
}
