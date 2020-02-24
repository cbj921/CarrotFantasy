using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class CoinMove : MonoBehaviour {

	private Text coinText;
	private Image coinImage;
	private Sprite[] coinSprite; // 0:少量金币 1：大量金币

	[HideInInspector]
	public int prize;

	private void Awake() {
		coinText = transform.Find("Text_Coin").GetComponent<Text>();
		coinImage = transform.Find("Img_Coin").GetComponent<Image>();
		coinSprite[0] = GameController.Instance.GetSprite("NormalMordel/Game/Coin");
		coinSprite[1] = GameController.Instance.GetSprite("NormalMordel/Game/ManyCoin");
	}

	private void OnEnable() {
		ShowCoin();
	}

	private void ShowCoin()
	{
		coinText.text = prize.ToString();
		if(prize <= 300)
		{
			coinImage.sprite = coinSprite[0];
		}
		else 
		{
			coinImage.sprite = coinSprite[1];
		}
		// 金币的移动
		transform.DOLocalMoveY(60,0.5f);
		DOTween.To(()=>coinImage.color,toColor=>coinImage.color = toColor,new Color(1,1,1,0),0.5f);
		Tween tween =  DOTween.To(()=>coinText.color,toColor=>coinText.color = toColor,new Color(1,1,1,0),0.5f);
		tween.OnComplete(DestroyCoin);
	}

	// 销毁金币UI
	private void DestroyCoin()
	{
		transform.localPosition = Vector3.zero;
		coinImage.color = coinText.color = new Color(1,1,1,1);
		GameController.Instance.PushGameObjectResource("CoinCanvas",transform.parent.gameObject);
	}
}
