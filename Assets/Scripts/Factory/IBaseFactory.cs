using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/**
 * @description: 游戏工厂接口
 */
public interface IBaseFactory
{
  	/**
  * @description: 获取相应的物体
  * @param {string} 物体的名字 
  * @return: 返回相应的物体
  */
	GameObject GetItem(string itemName);
	/**
  * @description: 将相应的物体放入工厂中
  * @param {string} 物体名称
  * @param {GameObject} 物体
  * @return: null 
  */
	void PushItem(string itemName,GameObject item);
}
