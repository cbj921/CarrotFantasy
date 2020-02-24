using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 特效脚本
public class Effect : MonoBehaviour {

	public float effectAnimationTime;
	public string resourcePath;

	private void OnEnable() {
		Invoke("DestroyEffect",effectAnimationTime);
	}

	private void DestroyEffect()
	{
		GameController.Instance.PushGameObjectResource(resourcePath,gameObject);
	}
}
