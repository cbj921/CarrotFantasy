using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;

public class SetPanel : BasePanel {

	private GameObject optionPageGo;
    private GameObject statisticsPageGo;
    private GameObject producePageGo;
    private GameObject resetPanelGo;
    private Tween SetPanelTween;
    private bool isPlayBGM = true;
    private bool isPlayEffect = true;
    public Sprite[] btnSprites; //  0：音效开 1：音效关 2:BGM开 3：BGM关
    public Text[] statisticsText; // 统计数据的文本
    private Image Img_Btn_EffectAudio;
    private Image Img_Btn_BgmAudio;

    protected override void Awake()
    {
        base.Awake();
        // 动画初始化
        SetPanelTween = transform.DOLocalMoveX(0,0.5f);
        SetPanelTween.SetAutoKill(false);
        SetPanelTween.Pause();
        // 获取物体对象
        optionPageGo = transform.Find("OptionPage").gameObject;
        statisticsPageGo = transform.Find("StatisticsPage").gameObject;
        producePageGo = transform.Find("ProducePage").gameObject;
        resetPanelGo = transform.Find("Panel_Reset").gameObject;
        Img_Btn_BgmAudio = optionPageGo.transform.Find("Btn_BgmAudio").GetComponent<Image>();
        Img_Btn_EffectAudio = optionPageGo.transform.Find("Btn_EffectAudio").GetComponent<Image>();
        //InitPanel();
    }

    // 初始化面板
    public override void InitPanel()
    {
        // 因为该面板初始放在屏幕的左边
        transform.localPosition = new Vector3(-800,0,0);
        // 设置渲染顺序
        transform.SetSiblingIndex(2);
        ShowOptionPage();
    }

    // 显示页面
    public void ShowOptionPage()
    {
        optionPageGo.SetActive(true);
        statisticsPageGo.SetActive(false);
        producePageGo.SetActive(false);
        resetPanelGo.SetActive(false);
    }
    public void ShowStatisticsPage()
    {
        ShowStatistics();
        optionPageGo.SetActive(false);
        statisticsPageGo.SetActive(true);
        producePageGo.SetActive(false);
        resetPanelGo.SetActive(false);
    }
    public void ShowProducePage()
    {
        optionPageGo.SetActive(false);
        statisticsPageGo.SetActive(false);
        producePageGo.SetActive(true);
        resetPanelGo.SetActive(false);
    }

    // 进入面板
    public override void EnterPanel()
    {
        InitPanel(); 
        gameObject.SetActive(true);
        SetPanelTween.PlayForward();
    }
    // 退出面板
    public override void ExitPanel()
    {
        SetPanelTween.PlayBackwards();
        mUIFacade.currentScenePanels[StringManager.MainPanel].EnterPanel();
    }
    
    // 音乐处理
    public void CloseOrOpenBGM()
    {
        isPlayBGM = !isPlayBGM;
        mUIFacade.CloseOrOpenBGM();
        if(isPlayBGM)
        {

            Img_Btn_BgmAudio.sprite = btnSprites[2];
        }
        else
        {
            Img_Btn_BgmAudio.sprite = btnSprites[3];
        }
    }
    // 音效
    public void CloseOrOpenEffect()
    {
        isPlayEffect = !isPlayEffect;
        mUIFacade.CloseOrOpenEffect();
        if(isPlayEffect)
        {
            Img_Btn_EffectAudio.sprite = btnSprites[0];
        }
        else
        {
            Img_Btn_EffectAudio.sprite = btnSprites[1];
        }
    }
    // 统计数据获得
    public void ShowStatistics()
    {
        int[] statistics = mUIFacade.GetStatistics();
        for(int i=0;i<statisticsText.Length;i++)
        {
            statisticsText[i].text = statistics[i].ToString();
        }
    }
    // 重置游戏
    public void RestGame()
    {

    }
    public void ShowResetPanel()
    {
        resetPanelGo.SetActive(true);
    }
    public void CloseResetPanel()
    {
        resetPanelGo.SetActive(false);
    }
}
