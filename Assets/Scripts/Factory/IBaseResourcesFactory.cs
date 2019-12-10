using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
 * @description: 游戏各种类型的资源工厂
 * @param {T：name} 
 */
public interface IBaseResourcesFactory<T>
{
	/**
  * @description: 获得单个资源
  * @param {string} 资源的路径 
  * @return: 相应的资源类型
  */
	T GetSingleResources(string resourcePath);
}
