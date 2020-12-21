using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 共通定数
/// </summary>
public class GlobalConst
{
    #region スライドアウト関連
    /// <summary>
    /// スライドアウトまでの時間
    /// </summary>
    public const float BG_SLIDE_DURATIOIN = 0.5f;

    /// <summary>
    /// 1フレームごとのスライド距離
    /// </summary>
    public const float BG_SLIDE_UNIT = 0.1f;
    #endregion

    #region シーン名
    /// <summary>
    /// イントロダクション画面
    /// </summary>
    public const string INTRO_SCENE = "IntroScene";

    /// <summary>
    /// ランキング画面
    /// </summary>
    public const string RANKING_SCENE = "RankingScene";
    #endregion
}
