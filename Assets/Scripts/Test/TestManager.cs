
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestManager : SingletonTemplete<TestManager> {

	// Use this for initialization
	void Start () {
		Debug.Log(Instance);
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
