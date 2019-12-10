using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 实现对象池的核心，理解对象池原理
public class ObjectPool : MonoBehaviour {

	public GameObject monster;
	private Stack<GameObject> monsterPool;
	private Stack<GameObject> activeMonsterList;

	private void Start() {
		monsterPool = new Stack<GameObject>();
		activeMonsterList = new Stack<GameObject>();
	}
	private void Update() {
		if(Input.GetMouseButtonDown(0)) // 0是左键，1是右键
		{
			GameObject monsterGo = GetMonster();
			monsterGo.transform.position = Vector3.one;
			activeMonsterList.Push(monsterGo);
		}
		else if(Input.GetMouseButtonDown(1))
		{
			if(activeMonsterList.Count>0)
			{
				PushMonster(activeMonsterList.Pop());
			}
		}	
	}

	private GameObject GetMonster()
	{
		GameObject newMonster = null;
		if(monsterPool.Count<=0)
		{
			newMonster = Instantiate(monster);
		}
		else
		{
			newMonster = monsterPool.Pop();
			newMonster.SetActive(true);
		}
		return newMonster;
	}
	private void PushMonster(GameObject backMonster)
	{
		backMonster.SetActive(false);
		monsterPool.Push(backMonster);
	}
}
