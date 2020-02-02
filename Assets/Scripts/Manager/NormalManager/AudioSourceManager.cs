/*
 * @Author: your name
 * @Date: 2019-12-05 16:07:11
 * @LastEditTime : 2020-02-02 11:24:44
 * @LastEditors  : Please set LastEditors
 * @Description: 该脚本是负责控制音乐的播放和停止
 * @FilePath: \CarrotFantasy\Assets\Scripts\Manager\NormalManager\AudioSourceManager.cs
 */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioSourceManager 
{
    public AudioSource[] audioSources; // 0:控制播放BGM，1：控制播放音效
    private bool isPlayBGMusic = true; // 默认背景音乐为 开
    private bool isPlayEffectMusic = true;  // 默认音效为 开
    // 构造函数
    public AudioSourceManager()
    {
        audioSources = GameManager.Instance.GetComponents<AudioSource>(); // 获得 GameManager 上的两个 AudioSource 组件
    }
    // 切换背景音频时调用
    public void PlayBGM(AudioClip audioClip)
    {
        if(!audioSources[0].isPlaying ||audioSources[0].clip != audioClip)
        {
            audioSources[0].clip = audioClip;
            audioSources[0].Play();
        }
    }
    // 播放音效
    public void PlayEffect(AudioClip audioClip)
    {
        if(isPlayEffectMusic)
        {
            // 判断是否能播放音效
            audioSources[1].PlayOneShot(audioClip);
        }
    }
    // 关闭BGM
    public void CloseBGM()
    {
        audioSources[0].Stop();
    }
    // 开启BGM
    public void OpenBGM()
    {
        audioSources[0].Play();
    }

    public void CloseOrOpenBGM()
    {
        isPlayBGMusic = !isPlayBGMusic;
        if(isPlayBGMusic)
        {
            OpenBGM();
        }
        else
        {
            CloseBGM();
        }
    }
    public void CloseOrOpenEffect()
    {
        isPlayEffectMusic = !isPlayEffectMusic;
    }
}
