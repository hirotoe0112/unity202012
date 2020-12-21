using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 共通関数
/// </summary>
public class GlobalProc
{
    /// <summary>
    /// スライドアウト後の処理用デリゲード
    /// </summary>
    public delegate void AfterSlideOut();

    /// <summary>
    /// スライドアウト
    /// </summary>
    /// <returns></returns>
    public IEnumerator SlideOutPanel(GameObject pnlWrap, AfterSlideOut afterSlideOut)
    {
        //開始時間
        float startTime = Time.time;

        //指定の秒数経過するまで移動
        while ((Time.time - startTime) < GlobalConst.BG_SLIDE_DURATIOIN)
        {
            pnlWrap.GetComponent<RectTransform>().transform.Translate(GlobalConst.BG_SLIDE_UNIT, 0, 0);
            yield return 0;
        }

        //スライドアウト後の処理
        afterSlideOut();
    }

}
