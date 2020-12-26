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
    public const float BG_SLIDE_UNIT = 1.1f;
    #endregion

    #region メッセージ表示関連
    /// <summary>
    /// メッセージ表示初期ウエイト
    /// </summary>
    public const float MESSAGE_INIT_WAIT = 1.0f;

    /// <summary>
    /// メッセージ表示途中ウエイト
    /// </summary>
    public const float MESSAGE_WAIT = 0.7f;
    #endregion

    /// <summary>
    /// 選択肢イベントのウエイト
    /// </summary>
    public const float SELECTION_WAIT = 0.5f;

    /// <summary>
    /// 得点表示のウエイト
    /// </summary>
    public const float SCORE_WAIT = 1.0f;

    /// <summary>
    /// 選択肢イベント終了時のウエイト
    /// </summary>
    public const float SELECTION_FINISH_WAIT = 1.5f;

    public const string PLUS_MARK = "+";

    public const string MINUS_MARK = "-";

    #region シーン名
    /// <summary>
    /// イントロダクション画面
    /// </summary>
    public const string INTRO_SCENE = "IntroScene";

    /// <summary>
    /// ランキング画面
    /// </summary>
    public const string RANKING_SCENE = "RankingScene";

    /// <summary>
    /// メイン画面
    /// </summary>
    public const string MAIN_SCENE = "MainScene";
    #endregion

    public const int PLAY_COUNT = 5;

    /// <summary>
    /// 選択肢
    /// </summary>
    public enum Selection
    {
        INIT,
        OPEN,
        REJECT,
        MOREHINT
    }
}
