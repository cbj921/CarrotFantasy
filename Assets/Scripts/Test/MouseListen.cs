using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

// 鼠标点击事件测试脚本
public class MouseListen : MonoBehaviour {
	// IPointerDownHandler接口只对UI有效，即Canvas下的物体有效
    // public void OnPointerDown(PointerEventData eventData)
    // {
	// 	Debug.Log("UI Click");
    // }

	// 用于游戏世界2D物体检测的API
	// 只能检测鼠标左键，右键不检测
	private void OnMouseDown() {
		Debug.Log("GameObject Click");
	}
}
