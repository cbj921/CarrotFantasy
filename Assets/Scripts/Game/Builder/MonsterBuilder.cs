using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterBuilder : IBuilder<Monster>
{
	public int mMonsterID;
	private GameObject monsterGo;

	public GameObject GetProductGO()
    {
        GameObject monsterGo = GameController.Instance.GetGameObjectResource("MonsterPrefab");
		Monster monsterScript = GetProduciClass(monsterGo);
		GetData(monsterScript);
		GetOtherResources(monsterScript);
		return monsterGo;
    }
    public void GetData(Monster productClassGo)
    {
        productClassGo.monsterID = mMonsterID;
		productClassGo.HP = mMonsterID * 100;
		productClassGo.initMoveSpeed = mMonsterID;
		productClassGo.prize = mMonsterID * 50;
		productClassGo.currentHP = productClassGo.HP;
		productClassGo.moveSpeed = productClassGo.initMoveSpeed;
    }

    public void GetOtherResources(Monster productClass)
    {
		productClass.GetMonsterProperity();
    }

    public Monster GetProduciClass(GameObject gameObject)
    {
		return gameObject.GetComponent<Monster>();
    }
}
