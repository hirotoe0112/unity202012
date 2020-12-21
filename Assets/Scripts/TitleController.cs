using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System;

public class TitleController : MonoBehaviour
{
    /// <summary>
    /// 背景パネル
    /// </summary>
    [SerializeField]
    private GameObject pnlWrap;

    /// <summary>
    /// ボタン押下
    /// </summary>
    private bool isPushButton;

    /// <summary>
    /// スタートボタン押下時
    /// </summary>
    public void GameStart()
    {
        if (!isPushButton)
        {
            isPushButton = true;

            //パネルをスライドアウトさせた後、イントロダクション画面を表示
            GlobalProc globalProc = new GlobalProc();
            GlobalProc.AfterSlideOut afterSlideOut = () => { SceneManager.LoadScene(GlobalConst.INTRO_SCENE); };
            StartCoroutine(globalProc.SlideOutPanel(pnlWrap, afterSlideOut));
        }
    }

    /// <summary>
    /// ランキングボタン押下時
    /// </summary>
    public void DisplayRanking()
    {
        if (!isPushButton)
        {
            isPushButton = true;

            //パネルをスライドアウトさせた後、イントロダクション画面を表示
            GlobalProc globalProc = new GlobalProc();
            GlobalProc.AfterSlideOut afterSlideOut = () => { SceneManager.LoadScene(GlobalConst.RANKING_SCENE); };
            StartCoroutine(globalProc.SlideOutPanel(pnlWrap, afterSlideOut));
        }
    }
}
