using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;


public class ScrollViewTest : MonoBehaviour,IBeginDragHandler,IDragHandler,IEndDragHandler {

	private ScrollRect scrollRect;
	private RectTransform contentRectTrans;

	// Use this for initialization
	void Start () {
		scrollRect = GetComponent<ScrollRect>();
		// UI中的RectTransform的探究
		contentRectTrans = scrollRect.content;

		// 获得世界坐标
		Debug.Log(contentRectTrans.position);
		// 获得局部坐标
		Debug.Log(contentRectTrans.localPosition); 
		// 获得UI宽度
		//Debug.Log(contentRectTrans.rect.xMax);
		Debug.Log(contentRectTrans.rect.width);
		// 获得高度
		Debug.Log(contentRectTrans.rect.height);
		// 获得当前UI的宽高
		Debug.Log(contentRectTrans.sizeDelta);
		Debug.Log(contentRectTrans.sizeDelta.x);
		Debug.Log(contentRectTrans.sizeDelta.y);

		// 注册监听事件
		scrollRect.onValueChanged.AddListener(PrintValue);
	}

	private void PrintValue(Vector2 vector2)
	{
		Debug.Log(vector2);
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void OnEndDrag(PointerEventData eventData)
    {
       	Debug.Log("结束滑动");
    }

    void IDragHandler.OnDrag(PointerEventData eventData)
    {
        Debug.Log("滑动中");
    }

    void IBeginDragHandler.OnBeginDrag(PointerEventData eventData)
    {
        Debug.Log("开始滑动");
    }
}
