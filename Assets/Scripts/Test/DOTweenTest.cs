using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class DOTweenTest : MonoBehaviour
{


    private Image maskImage;
    private Tween maskTween;
    // Use this for initialization
    void Start()
    {
        maskImage = GetComponent<Image>();
        // 感觉跟cocos中的action差不多
        // 1.DOTween的静态方法
        //DOTween.To(()=>maskImage.color,toColor=>maskImage.color=toColor,new Color(0,0,0,0),2f);

        // 2.DOTween的直接作用于transform上
        //Tween tween = maskImage.transform.DOLocalMoveX(500,1f);
        //tween.PlayForward();

        //3.动画的循环使用
        // maskTween = DOTween.To(() => maskImage.color, toColor => maskImage.color = toColor, new Color(0, 0, 0, 0), 2f);
        // maskTween.SetAutoKill(false);
        // maskTween.Pause();

        // 4.动画的回调
        // Tween maskTween =  maskImage.transform.DOLocalMoveX(500,1f);
        // maskTween.OnComplete(CompleteMethod);

        // 5.缓动函数
        maskTween =  maskImage.transform.DOLocalMoveX(500,1f);
        maskTween.OnComplete(CompleteMethod);
        maskTween.SetEase(Ease.InOutBounce);
        maskTween.Pause();
        maskTween.SetAutoKill(false);
        maskTween.SetLoops(-1,LoopType.Yoyo);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            maskTween.PlayForward();
        }
        else if(Input.GetMouseButtonDown(1)){
            maskTween.PlayBackwards();
        }
    }

    private void CompleteMethod(){
        DOTween.To(() => maskImage.color, toColor => maskImage.color = toColor, new Color(0, 0, 0, 0), 2f);
    }
}
