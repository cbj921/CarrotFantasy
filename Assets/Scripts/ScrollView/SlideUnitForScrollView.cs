using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using DG.Tweening;

public class SlideUnitForScrollView : MonoBehaviour,IBeginDragHandler,IEndDragHandler {

	private float contentLength; // 获取到content的除第一个单元外的长度
    private float beginMousePositionX;
    private float endMousePositionX;
    private float lastProportion; // 上一个位置比例
    private ScrollRect scrollRect;
    private float cellLength;
    private float spacingX;
    private float leftOffset; // 左偏移量
    private GridLayoutGroup contentGridLayoutGroup; // content中的gridLayoutGroup组件
    private RectTransform content; // 获得content组件
    private float limitLength; // 移动到下一个单元格的阈值
    private float everyItemLength; // 移动除第一个单元格外的每一个的距离
    private float everyItemProportion; // 滑动每一个单元格的比例
    private int totalItemNum; // 所有单元格的数量
    private int currentItemIndex; // 当前单元格索引 

    public float offsetLimit; // 用来改变拖动触发的距离，即灵敏度，默认为0
    
	
    
    private void Awake() {
        init();
    }
	private void init()
    {
        scrollRect = GetComponent<ScrollRect>();
        content = scrollRect.content;
        contentGridLayoutGroup = content.GetComponent<GridLayoutGroup>();
        spacingX = contentGridLayoutGroup.spacing.x;
        cellLength = contentGridLayoutGroup.cellSize.x;
        leftOffset = contentGridLayoutGroup.padding.left;
        totalItemNum = content.childCount; // 获取content下面子物体数量,即单元格数量

        contentLength = content.rect.xMax ; // 算出content的不包括viewport宽度的长度,用于算移动比例
        limitLength = cellLength/2 + leftOffset;// 计算出开始滑到下一个单元的阈值
        everyItemLength = cellLength + spacingX;
        everyItemProportion = everyItemLength / contentLength; //算出每一个单元的比例，即移动一格的比例
        currentItemIndex = 1; // 设置当前单元索引为1
        lastProportion = 0;
        scrollRect.horizontalNormalizedPosition = 0; // 设置初始比例为 0 
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        float offsetX = 0;
        endMousePositionX = Input.mousePosition.x;
        offsetX = beginMousePositionX - endMousePositionX;
        // Debug.Log("celllength:"+cellLength);
        // Debug.Log("offsetX:"+offsetX);
        // Debug.Log(currentItemIndex);
        if((Mathf.Abs(offsetX)+offsetLimit)>=limitLength)
        {
            if(offsetX>0)
            {
                // 右滑
                if(currentItemIndex >= totalItemNum) return;
                lastProportion += everyItemProportion;
                if(lastProportion > 1) lastProportion = 1; // 其实这里没必要进行检查，因为不可能出现超过1的情况
                currentItemIndex++;
        
            }
            else
            {
                // 左滑
                if(currentItemIndex <= 0) return;
                lastProportion -= everyItemProportion;
                if(lastProportion < 0) lastProportion = 0;
                currentItemIndex--; 
            }
            //scrollRect.horizontalNormalizedPosition = lastProportion;
            // DOTween的这个TO函数，第一个参数是要改变的值，第二个参数为插值的lambda表达式，第三个为要达到的最终值，第四个为时间
            DOTween.To(()=>scrollRect.horizontalNormalizedPosition,lerpValue=>scrollRect.horizontalNormalizedPosition = lerpValue,
                        lastProportion,0.5f).SetEase(Ease.OutQuint);
        }
        else
        {
            // 当滑动的距离没有达到时,返回当前index单元
            //scrollRect.horizontalNormalizedPosition = lastProportion;
            DOTween.To(()=>scrollRect.horizontalNormalizedPosition,lerpValue=>scrollRect.horizontalNormalizedPosition = lerpValue,
                        lastProportion,0.5f).SetEase(Ease.OutQuint);
        }
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        beginMousePositionX = Input.mousePosition.x;
    }
}
