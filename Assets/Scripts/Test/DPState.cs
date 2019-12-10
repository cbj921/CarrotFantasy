using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DPState : MonoBehaviour {

	// Use this for initialization
	void Start () {
		Context a = new Context();
		a.SetState(new Eat(a));
		a.Handle();
	}
	
}

public interface IState
{
	void Handle();
}

public class Context
{
	private IState mState; // 当前状态

	public void SetState(IState state)
	{
		mState = state;
	}
	public void Handle()
	{
		mState.Handle(); // 当前状态下需要执行的方法
	}
}

public class Eat:IState
{
	private Context mContext; // 当前状态持有者
	public Eat(Context context)
	{
		mContext = context;
	}
	public void Handle()
	{
		Debug.Log("Eat");
	}
}
public class Work:IState
{
	private Context mContext; // 当前状态持有者
	public Work(Context context)
	{
		mContext = context;
	}
	public void Handle()
	{
		Debug.Log("Work");
	}
}
public class Sleep:IState
{
	private Context mContext; // 当前状态持有者
	public Sleep(Context context)
	{
		mContext = context;
	}
	public void Handle()
	{
		Debug.Log("Sleep");
	}
}
