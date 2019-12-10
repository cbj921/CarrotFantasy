using System.Collections;
using System.Collections.Generic;
using UnityEngine;


// 面向接口编程测试
public class IOP : MonoBehaviour {

	// Use this for initialization
	void Start () {
		IHero a = new Ler(5,3);
		a.SkillQ();
		a.SkillW();
		//通过IHero生成的对象，无法调用原对象中的成员，只能调用属于接口的成员函数。
		// a.xx;是错误的
	}

}

public interface IHero
{
	void SkillQ();
	void SkillW();
}
public class Ler : IHero
{
	public Ler(int x,int y)
	{
		xx = x;
		yy = y;
	}
	public int xx;
	public int yy;
    public void SkillQ()
    {
        Debug.Log("Q");
    }

    public void SkillW()
    {
        Debug.Log("W");
    }
}