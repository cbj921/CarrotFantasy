using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

// UI中介，上层与管理者进行交互，下层与UI面板进行交互
public class UIFacade 
{
    // 上层管理者
    private UIManager mUIManager;
    private GameManager mGameManager;
    private AudioSourceManager mAudioSourceManager;
    private PlayerManager mPlayManager;
    // 下层UI面板
    // 当前场景下的UI面板中对应脚本
    public Dictionary<string,IBasePanel> currentScenePanels = new Dictionary<string, IBasePanel>();
    // 其他成员变量
    private GameObject mask;
    private Image maskImage;
    private Transform canvasTransform;
    // 场景状态
    public IBaseSceneState lastSceneState;
    public IBaseSceneState currentSceneState;

    // 构造函数
    public UIFacade(UIManager uiManager)
    {
        mUIManager = uiManager;
        mGameManager = GameManager.Instance;
        mAudioSourceManager = mGameManager.audioSourceManager;
        mPlayManager = mGameManager.playerManager;
        InitMask(); // 初始化遮罩
    }
    // 初始化遮罩
    public void InitMask()
    {
        canvasTransform = GameObject.Find("Canvas").transform;
        mask = CreatUIAndSetUIPos("Img_Mask");
        maskImage = mask.GetComponent<Image>();
    }
    // 实例化当前场景所有UIPanel，并将对应脚本存入 UIFacade 的字典中
    public void InitAllPanelDict()
    {
        foreach(var item in mUIManager.currentSceneUIDict)
        {
            item.Value.transform.SetParent(canvasTransform);
            item.Value.transform.localPosition = Vector3.zero;
            item.Value.transform.localScale = Vector3.one;
            IBasePanel basePanel = item.Value.GetComponent<IBasePanel>();
            if(basePanel == null)
            {
                Debug.Log("没有获取到对应的 IBasePanel 脚本");
            }
            basePanel.InitPanel();
            currentScenePanels.Add(item.Key,basePanel);
        }
    }
    // 实例化UI
    public GameObject CreatUIAndSetUIPos(string uiName)
    {
        GameObject itemGo = GetGameObjectResource(FactoryType.UIFactory,uiName);
        // 将父节点设置在Canvas下，否则显示不出来
        itemGo.transform.SetParent(canvasTransform);
        itemGo.transform.localPosition = Vector3.zero;
        itemGo.transform.localScale = Vector3.one;
        return itemGo;
    }
    // 生成Panel到UI字典中
    public void AddPanelToDict(string uiPanelName)
    {
        mUIManager.currentSceneUIDict.Add(uiPanelName,GetGameObjectResource(FactoryType.UIPanelFactory,uiPanelName));
    }
    // 清空字典
    public void ClearUIDict()
    {
        currentScenePanels.Clear();
        mUIManager.ClearUIDict();
    }
    // 改变场景状态
    public void ChangeSceneState(IBaseSceneState sceneState)
    {
        lastSceneState = currentSceneState;
        // 显示遮罩，进行场景的过渡
        ShowMask();
        currentSceneState = sceneState;
    }
    // 显示遮罩
    public void ShowMask()
    {
        mask.transform.SetSiblingIndex(10); // 设置渲染层级，确保mask处于最后渲染，这样才有效果
        Tween t = DOTween.To(()=>maskImage.color,toColor=>maskImage.color=toColor,new Color(0,0,0,1),2f);
        t.OnComplete(ExitCurrentScene);
    }
    // 离开当前场景
    private void ExitCurrentScene()
    {
        lastSceneState.ExitScene();
        currentSceneState.EnterScene();
        HideMask();
    }
    // 隐藏遮罩
    public void HideMask()
    {
        mask.transform.SetSiblingIndex(10); // 设置渲染层级，确保mask处于最后渲染，这样才有效果
        DOTween.To(()=>maskImage.color,toColor=>maskImage.color=toColor,new Color(0,0,0,0),2f);
    }

    // 给下层UI面板调用的获取资源函数，降低代码耦合性
    // 则下层只需调用 UIFacade 这个中介的函数即可，无需知道有上层 GameManager 
    public Sprite GetSprite(string resourcePath)
    {
        return mGameManager.GetSprite(resourcePath);
    }
    public AudioClip GetAudioClip(string resourcePath)
    {
        return mGameManager.GetAudioClip(resourcePath);
    }
    public RuntimeAnimatorController GetRuntimeAnimatorController(string resourcePath)
    {
        return mGameManager.GetRunTimeAnimatorController(resourcePath);
    }
    // 获取游戏物体
    public GameObject GetGameObjectResource(FactoryType factoryType,string resourcePath)
    {
        return mGameManager.GetGameObjectResource(factoryType,resourcePath);
    }
    // 将游戏物体放回工厂对象池
	public void PushGameObjectToFactory(FactoryType factoryType,string resourcePath,GameObject itemGo)
	{
		mGameManager.PushGameObjectToFactory(factoryType,resourcePath,itemGo);
	}
    // 提供给下层UI开关音乐和音效的API
    public void CloseOrOpenBGM()
    {
        mAudioSourceManager.CloseOrOpenBGM();
    }
    public void CloseOrOpenEffect()
    {
        mAudioSourceManager.CloseOrOpenEffect();
    }

    // 设置面板中数据显示中方法的封装
    // 按UI中文本的顺序放回统计数据
    public int[] GetStatistics()
    {
        int[] statistics = new int[7];
        statistics[0] = mPlayManager.normalLevelMapNum;
        statistics[1] = mPlayManager.hideLevelMapNum;
        statistics[2] = mPlayManager.bossLevelMapNum;
        statistics[3] = mPlayManager.moneyNum;
        statistics[4] = mPlayManager.killBossNum;
        statistics[5] = mPlayManager.killMonsterNum;
        statistics[6] = mPlayManager.destroyPropsNum;
        return statistics;
    }
}
