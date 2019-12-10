using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
 * @description: 是一个枚举类型的变量，用来指出有哪些工厂类型 
 */
public enum FactoryType {
	UIPanelFactory,
	UIFactory,
	GameFactory // 该类是指会用在游戏中的类别，比如音频，sprite等等
}
