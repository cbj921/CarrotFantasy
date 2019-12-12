using System.Collections;
using System.Collections.Generic;
using UnityEngine;


// 中介者设计模式 测试脚本
public class DPMediator : MonoBehaviour {

	private void Start() {
		FriendA A = new FriendA();
		FriendB B = new FriendB();
		A.MoneyCount = 50;
		B.MoneyCount = 50;
		Mediator mediator = new Mediator(A,B);

		A.changeMoneyCount(30,mediator);
		Debug.Log("A此时的金钱数："+A.MoneyCount);
		Debug.Log("B此时的金钱数："+B.MoneyCount);
		B.changeMoneyCount(10,mediator);
		Debug.Log("A此时的金钱数："+A.MoneyCount);
		Debug.Log("B此时的金钱数："+B.MoneyCount);

	}
}

// 抽象牌友类
public abstract class AbstractCardFridend
{
	public int MoneyCount{get;set;}
	protected AbstractCardFridend()
	{
		MoneyCount = 0;
	}

	public abstract void changeMoneyCount(int count,AbstractMediator mediator);
}

// 牌友A
public class FriendA : AbstractCardFridend
{
	// 依赖于抽象中介者对象
    public override void changeMoneyCount(int count, AbstractMediator mediator)
    {
        mediator.AWin(count);
    }
}
// 牌友B
public class FriendB : AbstractCardFridend
{
    public override void changeMoneyCount(int count, AbstractMediator mediator)
    {
        mediator.BWin(count);
    }
}
// 抽象中介者类
public abstract class AbstractMediator
{
	protected AbstractCardFridend A;
	protected AbstractCardFridend B;

	protected AbstractMediator(AbstractCardFridend a,AbstractCardFridend b)
	{
		A = a;
		B = b;
	}
	public abstract void AWin(int count);
	public abstract void BWin(int count);
}
// 具体中介者类
public class Mediator : AbstractMediator
{
    public Mediator(AbstractCardFridend a, AbstractCardFridend b) : base(a, b)
    {
    }

    public override void AWin(int count)
    {
        A.MoneyCount += count;
		B.MoneyCount -= count;
    }

    public override void BWin(int count)
    {
        A.MoneyCount -= count;
		B.MoneyCount += count;
    }
}
