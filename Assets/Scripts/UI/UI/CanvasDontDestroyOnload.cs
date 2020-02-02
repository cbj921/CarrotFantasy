using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 用来确保 Canvas 在切换场景的时候不销毁
public class CanvasDontDestroyOnload : MonoBehaviour {

	// Use this for initialization
	void Start () {
		DontDestroyOnLoad(this.gameObject);	
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
